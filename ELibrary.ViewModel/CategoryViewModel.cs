using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.ViewModel;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public int NoOfBooks { get; set; }
    public string ImageUrl { get; set; }
    public bool IsFeatured { get; set; } = false;
}

public class EditCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public IFormFile ImageFile { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; } = false;
    public string? PreviousImageUrl { get; set; }
}

public class AddCategoryViewModel
{
    [Required]
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public IFormFile ImageFile { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; } = false;
}
