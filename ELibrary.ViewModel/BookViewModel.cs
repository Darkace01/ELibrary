using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ELibrary.ViewModel;

public class BookViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int? CategoryId { get; set; }
    public CategoryViewModel Category { get; set; }
    public string Tags { get; set; }
    public string ImageUrl { get; set; }
    public string PdfUrl { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    public bool UserOwnBook { get; set; }
}

public class AddBookViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }
    public List<string> Tags { get; set; }
    public string? TagString { get; set; }
    public string? PdfUrl { get; set; }
    public IFormFile ImageFile { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile PdfFile { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Description { get; set; }
}

public class EditBookViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }
    public List<string> Tags { get; set; }
    public string? TagString { get; set; }
    public IFormFile PdfFile { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Description { get; set; }
    public string PreviousImageUrl { get; set; }
    public string PreviousPdfUrl { get; set; }
}
