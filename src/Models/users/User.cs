

public class User{


  public Guid UserId {get;set;}

  public string? UserName {get;set;}

  public string? Password {get;set;}
  public string? Email {get;set;}
  public int Age {get;set;}

  public string? Address {get;set;}

  public bool IsAdmin = false;
  public bool IsBanned = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;



}
