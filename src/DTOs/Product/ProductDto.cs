public class ProductDto{
  public required string Name {get;set;}
  public Double Price {get;set;}
  public int Quantity {get;set;}
  public string? Description {get;set;}
  public string? ImageIDs {get;set;}
  public Guid CategoryId {get;set;}
  public int? ReturnByDaysAfterOrder {get;set;} =2;
  public bool IsOnSale = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}