using System.Text.Json.Serialization;

public class User{
  public Guid UserId {get;set;}
  public required string UserName {get;set;}
  public required string Password {get;set;}
  public required string Email {get;set;}
  public int Age {get;set;}
  public string? Address {get;set;}
  public bool IsAdmin = false;
  public bool IsBanned = false;

// refrence for the Order
// Each user can have many order :)

[JsonIgnore]
  public List<Order> orders = new List<Order>();
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}
