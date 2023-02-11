using Microsoft.AspNetCore.Identity;

namespace CeoHelper.Data.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public int Tokens { get; set; } = 5000;
    public bool IsDeactivated { get; set; }
    public DateTime? DeactivationDate { get; set; }

    public string Ip { get; set; }
}

public class AppRole : IdentityRole<long>
{
}
