public class CreateProductDto{
  public required string Name { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public string? Description { get; set; }
  public string? ImageIDs { get; set; } // = "Default Image Path when available";
  public Guid CategoryId { get; set; }
  public int? ReturnByDaysAfterOrder { get; set; }
  public bool IsOnSale { get; set; } = false;
}
