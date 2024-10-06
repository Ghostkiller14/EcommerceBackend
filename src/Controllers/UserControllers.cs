using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("/api/v1/users")]

public class UserControllers: ControllerBase {
  private readonly IUserServices _userServices;

  public UserControllers(IUserServices userServices){
    _userServices = userServices;
  }

  // I don't think we need to create user anymore because we are using the Regester Authntication but it might be used for the admin if he want to creat a user for somone, right now i think there is no need but later we might use it.

  //   [HttpPost]
  //   public async Task<IActionResult> CreateUser([FromBody]CreateUserDto createdUser){
  //   if (!ModelState.IsValid){
  //     return ApiResponse.BadRequest("Invalid User Data");
  //   }
  //   try{
  //     var user =  await _userServices.CreateUserServiceAsync(createdUser);

  //     return ApiResponse.Created(user, "User Created Successfully!");
  //   }catch(ApplicationException ex){
  //     return ApiResponse.ServerError("Server error: " + ex.Message);
  //   }catch(Exception ex){
  //     return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
  //   }
  // }



        [Authorize(Roles = "User")]
        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            return Ok("user data is returned");
        }


  [Authorize(Roles = "Admin")]
  [HttpGet]
  public async Task<IActionResult> GetUsers(){
    try{
      var users =  await _userServices.GetUserAsync();

      return ApiResponse.Success(users, "Users are returned Successfully");
    }catch(Exception ex){
      return ApiResponse.ServerError("Server Error: " + ex.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> FindUserById(Guid id){
    try{
      var user = await _userServices.FindUserByIdServiceAsync(id);
      if (user == null){
        return ApiResponse.NotFound($"User with this id {id} does not exist");
      }
      return ApiResponse.Success(user, "User is returned succesfully");
    }catch(Exception ex){
      return ApiResponse.ServerError("Server Error: " + ex.Message);
    }
  }

  [Authorize(Roles = "Admin")]

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUserById(Guid id){
    try{
      var user = await _userServices.DeleteUserByIdServiceAsync(id);

      if (user == false){
        return ApiResponse.NotFound("the User with the Id you've given dosen't exist");
      }
      return ApiResponse.Success(user, "User has been deleted successfully");
    }catch(Exception ex){
      return ApiResponse.BadRequest("Server Error: " + ex.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUserById(Guid id, UpdateUserDto updateUser){
    if (!ModelState.IsValid){
      return ApiResponse.BadRequest("Invalid Update Data");
    }

    try{
      var userData = await _userServices.UpdateUserServiceAsync(id, updateUser);
      return ApiResponse.Success(userData, "User has been Updated!");
    }catch(Exception ex){
      return ApiResponse.BadRequest("Server Error: " + ex.Message);
    }
  }
}
