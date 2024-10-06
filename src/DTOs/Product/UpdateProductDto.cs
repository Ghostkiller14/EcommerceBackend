public class UpdateProductDto{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public string? ImageIDs { get; set; }
    //public Guid? CategoryId { get; set; } // Uncomment when Category class is ready
    public int? ReturnByDaysAfterOrder { get; set; }
    public bool? IsOnSale { get; set; }
}
