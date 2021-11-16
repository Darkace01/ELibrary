using System.ComponentModel.DataAnnotations;

namespace ELibrary.ViewModel;

public class ApplicationUserViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    ///     User full name
    /// </summary>
    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    /// <summary>
    ///     User Matric No
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [Display(Name = "Matric Number")]
    public string MatricNo { get; set; }
}
