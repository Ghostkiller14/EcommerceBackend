
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


       if (!ModelState.IsValid)
      {
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


  // [HttpGet]
  // public IActionResult GetUsers(){

  //   var users = _userServices.

  //   if (users == null){
  //     return NotFound("The List of users is Empty");
  //   }

  //   return Ok(users);
  // }


  // [HttpGet("{Id}")]
  // public IActionResult GetUserByID(Guid Id){

  //   var user = _userServices.GetUserByIdService(Id);

  //   if(user == null){

  //     return NotFound($"The user with {Id} is not Exist ");
  //   }
  //   return Ok(user);
  // }

  // [HttpDelete("{Id}")]

  // public IActionResult DeleteUserById(Guid Id){

  //   var user = _userServices.DeleteUserService(Id);

  //   if(!user){
  //           return NotFound("Id you are tring to find is not Exist");
  //   }

  //   return Ok(user);
  // }




}
