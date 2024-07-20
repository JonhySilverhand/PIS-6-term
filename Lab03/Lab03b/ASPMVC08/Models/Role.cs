namespace ASPCMVC08.Models;

public class Role : User
{
    public IEnumerable<string> Roles { get; set; }

    public Role() : base()
    {
        Roles = new List<string>();
    }

    public Role(User user) : base(user)
    {
        Roles = new List<string>();
    }

    public Role(User user, IEnumerable<string> roles) : base(user)
    {
        Roles = roles;
    }
}
