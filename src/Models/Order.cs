
public class Order{

  public Guid OrderId {get;set;}

  public Guid ProductId {get;set;}

  public Decimal Price {get;set;}

  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;


//Each Order has a one User
  public User? User {get;set;} // the refrence User Table

  public Guid UserId{get;set;} // Forign Key

  }
