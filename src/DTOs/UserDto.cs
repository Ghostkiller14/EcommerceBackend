public class UserDto{
  public Guid UserId {get;set;}
  public required string UserName {get;set;}
  public required string Email {get;set;}

  public string Address {get;set;}

  public int Age {get;set;}
}
