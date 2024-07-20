using ASPCMVC08;
using ASPCMVC08.Attributes;
using ASPCMVC08.Dto;
using ASPCMVC08.Exceptions;
using ASPCMVC08.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCMVC08.Controllers;

[Route("Admin")]
[IsValidForEnter]
[AuthHelper]
public class AdminController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly AppDbContext? dbContext;

    private const string defaultCntr = "Home";
    private const string adminCntr = "Admin";
    private const string defaultAct = "Index";

    public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        this.dbContext = dbContext;
    }

    private async Task<List<UserRolesViewModel>> userRoleAsync(string selfAdmin)
    {

        var users = await userManager.Users.ToListAsync();
        var userRoles = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            if (selfAdmin != user.UserName)
            {

                var roles = await userManager.GetRolesAsync(user);

                var userRolesViewModel = new UserRolesViewModel
                {
                    UserName = user.UserName,
                    Roles = roles
                };

                userRoles.Add(userRolesViewModel);

            }
        }


        return userRoles;
    }

    [ManagerCtrl]
    [Authorize(Roles = "Administrator")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
                return View();

            var user = await userManager.GetUserAsync(User);
            ViewBag.IsAuthorized = true;
            ViewBag.selfUsername = user.UserName;
            ViewBag.selfRoles = await userManager.GetRolesAsync(user);
            var data = await userRoleAsync(user.UserName);
            if (data.Count == 0) throw new Exception();
            return View("Index", data);
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
        }
        return View();
    }

    [HttpGet("Register")]
    [CheckerAuth]
    [ManagerCtrl]
    public IActionResult RegisterForm()
    {
        return View("Register");
    }

    [HttpPost("Register")]
    [CheckerAuth]
    public async Task<IActionResult> Register(SignUpDto dto, string cntrl = defaultCntr, string act = defaultAct)
    {
        try
        {
            ViewBag.Username = dto.Username;
            ViewBag.Password = dto.Password;

            if (!ModelState.IsValid)
                throw new ExceptionHandler(ModelState);
   

            var user = new User()
            {
                UserName = dto.Username,
                PasswordHash = dto.Password,
                NormalizedUserName = dto.Username
            };

            if (await userManager.FindByNameAsync(user.UserName.ToLower()) != null)
                throw new Exception("Пользователь уже существует");

            await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, "User");

            if (user.UserName.Contains("Admin"))
                await userManager.AddToRoleAsync(user, "Administrator");

            return RedirectToAction(act, cntrl);
        }
        catch (ExceptionHandler ve)
        {
            ViewBag.Errors = ve.ErrorsHans;
            return View(ViewBag.View ?? "Register");
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return View(ViewBag.View ?? "Register");
        }
    }


    [HttpGet("SignIn")]
    [CheckerAuth]
    [ManagerCtrl]
    public IActionResult LoginForm()
    {
        return View("Login");
    }

    [HttpPost("SignIn")]
    [CheckerAuth]
    public async Task<IActionResult> Login(SignUpDto dto, string cntrl = defaultCntr, string act = defaultAct)
    {
        try
        {

            if (!ModelState.IsValid)
                throw new ExceptionHandler(ModelState);

            var user = await userManager.FindByNameAsync(dto.Username);

            if (user is null || user.PasswordHash != dto.Password)
                throw new Exception("Ошибка при входе");

            await this.signInManager.SignInAsync(user, true);
            return RedirectToAction(act, cntrl);
        }
        catch (ExceptionHandler ve)
        {
            ViewBag.Errors = ve.ErrorsHans;
            return View("Login");
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return View("Login");
        }
    }


    [HttpGet("SignOut")]
    [Authorize]
    [ManagerCtrl]
    public IActionResult SignOutForm()
    {
        return View("SignOut");
    }

    [HttpPost("SignOut")]
    [Authorize]
    public async Task<IActionResult> SignOut(string cntrl = defaultCntr, string act = defaultAct)
    {
        try
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(act, cntrl);
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return View();
        }
    }



    [HttpGet("CreateUser")]
    [Authorize(Roles = "Administrator")]
    [ManagerCtrl]
    public IActionResult CreateUserForm()
    {
        return View("CreateUser");
    }

    [HttpPost("CreateUser")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateUser(SignUpDto dto, string cntrl = adminCntr, string act = defaultAct)
    {
        ViewBag.View = "CreateUser";
        return await Register(dto, cntrl, act);
    }



    [HttpGet("DeleteUser")]
    [Authorize(Roles = "Administrator")]
    [ManagerCtrl]
    public async Task<IActionResult> DeleteUserForm()
    {
        var nameOfCurrent = this.User.Identity?.Name;

        var userNames = await userManager
            .Users
            .Select(u => u.UserName)
            .Where(n => !n.Equals(nameOfCurrent))
            .ToListAsync();
        return View("DeleteUser", userNames);
    }

    [HttpPost("DeleteUser")]
    [Authorize(Roles = "Administrator,User")]
    public async Task<IActionResult> DeleteUser([FromForm] string username, string cntrl = adminCntr, string act = defaultAct)
    {
        try
        {
            var user = await userManager.GetUserAsync(User);
            var _user = user;
            if (user is null)
                throw new("Пользователь не существует");

            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains("Administrator") && username != user.UserName)
                throw new Exception("Ошибка при удалении пользователя");

            user = await userManager.FindByNameAsync(username);
            if (user is null)
                throw new("Пользователь не существует");

            if (_user.UserName == username)
                await signInManager.SignOutAsync();


            await userManager.DeleteAsync(user);

            return RedirectToAction(act, cntrl);
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return View();
        }
    }



    [HttpGet("ChangePassword")]
    [Authorize]
    [ManagerCtrl]
    public IActionResult ChangePasswordForm()
    {
        return View("ChangePassword");
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto, string cntrl = defaultCntr, string act = defaultAct)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new ExceptionHandler(ModelState);

            if (User.Identity is null)
                throw new UnauthorizedAccessException();

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user is null)
                throw new UnauthorizedAccessException();

            if (!user.PasswordHash.Equals(dto.Password))
                throw new Exception("Пароль неверный");

            user.PasswordHash = dto.NewPassword;
            await userManager.UpdateAsync(user);

            return RedirectToAction(act, cntrl);
        }
        catch (ExceptionHandler ve)
        {
            ViewBag.Errors = ve.ErrorsHans;
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
        }
        return View();
    }


    [HttpGet("Assign")]
    [Authorize(Roles = "Administrator")]
    [ManagerCtrl]
    public async Task<IActionResult> AssignForm()
    {
        try
        {
            var users = (await userManager
                .Users
                .Where(u => !u.NormalizedUserName.Equals(User.Identity.Name))
                .ToListAsync())
                .Select(u => new Role(u, this.userManager.GetRolesAsync(u).Result))
                .ToList();

            var roles = await roleManager
                .Roles
                .Where(r => r.Name != "User")
                .ToListAsync();

            return View("Assign", new RoleModel(users, roles));
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return View("Assign", new RoleModel());
        }
    }

    [HttpPost("Assign")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignRole(SetupRoleDto dto)
    {
        try
        {
            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user is null)
                throw new("Пользователь не существует");

            var userRoles = await userManager.GetRolesAsync(user);

            var role = await roleManager.FindByIdAsync(dto.RoleId);

            if (role is null || role.Name == "User")
                throw new Exception("Ошибка при назвачении роли");

            IdentityResult res = await (!userRoles.Contains(role.Name) ?
                userManager.AddToRoleAsync(user, role.NormalizedName) :
                userManager.RemoveFromRoleAsync(user, role.NormalizedName));

            if (!res.Succeeded)
                throw new Exception("Ошибка при назвачении роли");

            return Ok();
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return BadRequest();
        }
    }



    [HttpGet("CreateRole")]
    [Authorize(Roles = "Administrator")]
    [ManagerCtrl]
    public IActionResult CreateRoleForm()
    {
        return View("CreateRole");
    }

    [HttpPost("CreateRole")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateRole(CreateRoleDto dto, string cntrl = adminCntr, string act = defaultAct)
    {
        try
        {
            if (!ModelState.IsValid)
                throw new ExceptionHandler(ModelState);

            var role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == dto.Name);

            if (role is not null)
                throw new Exception("Роль уже существует");

            role = new IdentityRole()
            {
                Name = dto.Name,
                NormalizedName = dto.Name
            };

            await roleManager.CreateAsync(role);
            return RedirectToAction(act, cntrl);
        }
        catch (ExceptionHandler ve)
        {
            ViewBag.Errors = ve.ErrorsHans;
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<String> { e.Message };
        }

        return View();
    }


    [HttpGet("DeleteRole")]
    [Authorize(Roles = "Administrator")]
    [ManagerCtrl]
    public async Task<IActionResult> DeleteRoleForm()
    {
        try
        {
            var roles = await roleManager
                .Roles
                .Select(r => r.Name)
                .Where(r => r != "Administrator" && r != "User")
                .ToListAsync();
            return View("DeleteRole", roles);
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
        }

        return View("DeleteRole", new string[0]);
    }

    [HttpPost("DeleteRole")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteRole(DeleteRoleDto dto, string cntrl = adminCntr, string act = defaultAct)
    {
        try
        {
            var role = await roleManager.FindByNameAsync(dto.RoleName);

            if (role is null)
                throw new Exception("Неизвестная роль");

            var users = await userManager.Users.ToListAsync();
            foreach (var u in users)
            {
                await userManager.RemoveFromRoleAsync(u, role?.NormalizedName ?? "");
            }

            await roleManager.DeleteAsync(role);
            return RedirectToAction(act, cntrl);
        }
        catch (Exception e)
        {
            ViewBag.Errors = new List<string> { e.Message };
            return await this.DeleteRoleForm();
        }
    }



    [HttpGet("Error")]
    [ManagerCtrl]
    [Route("{*path}")]
    public IActionResult Error(string message)
    {
        ViewBag.Message = message ?? "404. Некорректный url";
        return View();
    }



    private async Task<List<string>> GetRoles(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        return (await userManager.GetRolesAsync(user)).ToList();
    }

}
