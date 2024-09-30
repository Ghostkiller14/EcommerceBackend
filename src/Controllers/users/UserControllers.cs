using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/users")]

public class UserControllers: ControllerBase {

  private readonly UserServices _userServices;

  public UserControllers( UserServices userServices){
    _userServices = userServices;
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody]CreateUserDto createdUser){
    if (!ModelState.IsValid){
      // Log the errors or handle them as needed
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
      Console.WriteLine("Validation errors:");

      errors.ForEach(error => Console.WriteLine(error));

      // Return a custom response with validation errors
      return BadRequest(new { Message = "Validation failed", Errors = errors });
    }

    var user =  await _userServices.CreateUserServiceAsync(createdUser);


    var response = new { Message = "User created successfully", User = user };
    return Created($"/api/users/{user.UserId}",response);

}


  [HttpGet]
  public async Task<IActionResult> GetUsers(){

    var users =  await _userServices.GetUserAsync();

    var response = new { StatusCode = 200, Message = "Users are returned successfully", Users = users };
    return Ok(response);
  }


  [HttpGet("{Id}")]
  public async Task<IActionResult> FindUserById(Guid Id){

    var user =   await _userServices.FindUserByIdServiceAsync(Id);

    return Ok(user);
  }



    [HttpDelete("{Id}")]

    public async Task<IActionResult> DeleteUserById(Guid Id){

      var user = await _userServices.DeleteUserByIdServiceAsync(Id);

      if(user == false){
        return BadRequest("The Id you trying to find is Not Exist");
      }

      var response = new {message = "UserDeleted successfully" , User = user};

      return Ok(response);

    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateUserById(Guid Id, UpdateUserDto updateUser){

      var userData = await  _userServices.UpdateUserServiceAsync(Id,updateUser);

      return Ok(userData);

    }
  }
