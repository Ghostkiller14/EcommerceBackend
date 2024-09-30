public class User{
  public Guid UserId {get;set;}
  public required string UserName {get;set;}
  public required string Password {get;set;}
  public required string Email {get;set;}
  public int Age {get;set;}
  public string? Address {get;set;}
  public bool IsAdmin = false;
  public bool IsBanned = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}