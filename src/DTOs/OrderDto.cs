public class OrderDto{

  public Guid OrderId {get;set;}

  public Guid ProductId {get;set;}

  public Decimal Price {get;set;}

  public Guid UserId {get;set;}
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;


}
