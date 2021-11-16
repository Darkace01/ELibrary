using System.ComponentModel.DataAnnotations;

namespace ELibrary.ViewModel;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public int NoOfBooks { get; set; }
    public bool IsFeatured { get; set; } = false;
}

public class EditCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public bool IsFeatured { get; set; } = false;
}

public class AddCategoryViewModel
{
    [Required]
    public string Name { get; set; }
    public bool DefaultCategory { get; set; }
    public bool IsFeatured { get; set; } = false;
}
