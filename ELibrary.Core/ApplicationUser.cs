using Microsoft.AspNetCore.Identity;

namespace ELibrary.Core;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Users fullname
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Users matric no
    /// </summary>
    public string? MatricNo { get; set; }
}
