using Microsoft.AspNetCore.Identity;

namespace CeoHelper.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public int Tokens { get; set; } = 100;
}

public class AppRole : IdentityRole<Guid>
{
}
