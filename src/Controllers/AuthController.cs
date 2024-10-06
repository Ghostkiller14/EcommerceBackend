

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


     [ApiController]
     [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {

          try{
            var result = await _authService.RegisterUserService(userRegisterDto);

                if (!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

            return ApiResponse.Created(result,"User Created Successfully :-)");

          }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto){

          try{

            var token = await _authService.LoginService(userLoginDto);

            return Created("Loged in :-)", new { Token = token });

          }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }
      }

    }

