public class Product{
  public Guid ProductId {get;set;}
  public required string Name {get;set;}
  public Double Price {get;set;}
  public int Quantity {get;set;}
  public string? Description {get;set;}
  public string? ImageIDs {get;set;} //= "Path of a default image when available";
  //public Guid CategoryId {get;set;} uncomment when Category class is ready
  public int? ReturnByDaysAfterOrder {get;set;} =2;
  public bool IsOnSale = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}