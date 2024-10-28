
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
 public class AuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext appDbContext, IConfiguration configuration,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _mapper = mapper;

            // Console.WriteLine($"JWT Key Length: {_configuration["Jwt:Key"].Length}");
            // Console.WriteLine($"JWT Issuer: {_configuration["Jwt:Issuer"]}");
            // Console.WriteLine($"JWT Audience: {_configuration["Jwt:Audience"]}");
        }

        // Register the user

        public async Task<User> RegisterUserService(UserRegisterDto  userRegisterDto)
        {
          try{

            userRegisterDto.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            var userRegester = _mapper.Map<User>(userRegisterDto);

            await _appDbContext.Users.AddAsync(userRegester);
            await _appDbContext.SaveChangesAsync();

            return userRegester;

          } catch (DbUpdateException dbEx){
          Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
           throw new ApplicationException("An error has occurred while saving the data to the database");
        }catch (Exception ex)
           {
          Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
          throw new ApplicationException("An unexpected error has occurred");

    }
 }

        public async Task<string> LoginService(UserLoginDto userLoginDto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return "Email/Password is incorrect";
            }

            var userLogin = _mapper.Map<User>(user);



            var token = GenerateJwtToken(userLogin);

            return token;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

          var jwtKey = Environment.GetEnvironmentVariable("JWT__KEY") ?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
         var jwtIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER") ?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE") ?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[]
                {

                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") // User's role, determining access level.
            }),
                Expires = DateTime.UtcNow.AddMinutes(5), // Set the token to expire in 1 minute from creation.
                // Expires = DateTime.UtcNow.AddDays(2), // Set the token to expire in 2 hours from creation.
                // Expires = DateTime.UtcNow.AddHours(2),

                // The signing credentials contain the security key and the algorithm used for signature validation.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),


                // optional
                Issuer = jwtIssuer,// "iss" (issuer) claim: The issuer of the token.
                Audience =  jwtAudience// "aud" (audience) claim: Intended recipient of the token.
            };

            // Create the token based on the descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token to a JWT string.
            return tokenHandler.WriteToken(token);
        }


    }
