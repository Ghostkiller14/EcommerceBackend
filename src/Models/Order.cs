


using System.Text.Json.Serialization;



public class Order{

  public Guid OrderId {get;set;}

  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

  public User? User {get;set;} // the refrence User Table

  public Guid UserId{get;set;} // Forign Key


  [JsonIgnore]
      public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

  }
