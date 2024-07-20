using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPCMVC08.Models;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected AppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "48aff748-71e1-4673-8c23-f1cba54db4ad",
            Name = "Administrator",
            NormalizedName = "Administrator".ToUpper()
        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c982a2f4-bb19-4e70-87a3-c881d70077d5",
            Name = "User",
            NormalizedName = "User".ToUpper()
        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "a3dc3734-6b09-4090-aaba-ba6e120d99c1",
            Name = "Employee",
            NormalizedName = "Employee".ToUpper()
        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "da739459-dfb0-42d3-95ef-9a65013c2851",
            Name = "Master",
            NormalizedName = "Master".ToUpper()
        });

        modelBuilder.Entity<User>().HasData(
                  new User
                  {
                      Id = "5fbd1caf-bb65-44bc-a676-0c4905bdd921",
                      UserName = "Administrator",
                      NormalizedUserName = "Administrator",
                      PasswordHash = "qwerty1234"
                  }
              );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
           new IdentityUserRole<string>
           {
               RoleId = "48aff748-71e1-4673-8c23-f1cba54db4ad",
               UserId = "5fbd1caf-bb65-44bc-a676-0c4905bdd921"
           });

        base.OnModelCreating(modelBuilder);
    }
}