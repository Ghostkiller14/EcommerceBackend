using System.ComponentModel.DataAnnotations;
public class CreateCategoryDto{
    [Required(ErrorMessage = "Category name is missing")]
    [StringLength(100, ErrorMessage = "Category name has to be 3 to 100 characters in Length", MinimumLength = 3)]
    public string Name {get; set;}
    public string? Slug {get; set;}
}