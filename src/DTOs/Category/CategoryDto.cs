public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Product> Products { get; set; }


}
