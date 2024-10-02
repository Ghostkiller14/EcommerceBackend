public class CreateProductDto{
  public required string Name { get; set; }
  public double Price { get; set; }
  public int Quantity { get; set; }
  public string? Description { get; set; }
  public string? ImageIDs { get; set; } // = "Default Image Path when available";
  //public Guid CategoryId { get; set; } // Uncomment when Category class is ready
  public int? ReturnByDaysAfterOrder { get; set; }
  public bool IsOnSale { get; set; } = false;
}