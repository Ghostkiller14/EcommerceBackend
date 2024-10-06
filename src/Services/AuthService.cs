
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

        public async Task<User> RegisterUserService(UserRegisterDto userRegisterDto)
        {

          try{

            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            return user;

          } catch (DbUpdateException dbEx){
          Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
           throw new ApplicationException("An error has occurred while saving the data to the database");
        }catch (Exception ex)
           {
          Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
          throw new ApplicationException("An unexpected error has occurred");
          
    }
 }

        // login the user
        public async Task<string> LoginService(UserLoginDto userLoginDto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return "Email/Password is incorrect";
            }

            var token = GenerateJwtToken(user);

            return token;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Handler responsible for creating and validating JWTs. It handles the token's lifecycle.

            // Convert the secret key from the configuration into a byte array.
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration.");

            var key = Encoding.ASCII.GetBytes(jwtKey);


            // settings for token: Describe the token using SecurityTokenDescriptor.
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[]
                {
                // optional claims
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Often used to store a user ID, which is critical for identifying the user within your system.
                new Claim(ClaimTypes.Name, user.UserName), // User's name.
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") // User's role, determining access level.
            }),
                Expires = DateTime.UtcNow.AddMinutes(5), // Set the token to expire in 1 minute from creation.
                // Expires = DateTime.UtcNow.AddDays(2), // Set the token to expire in 2 hours from creation.
                // Expires = DateTime.UtcNow.AddHours(2),

                // The signing credentials contain the security key and the algorithm used for signature validation.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                // optional
                Issuer = _configuration["Jwt:Issuer"], // "iss" (issuer) claim: The issuer of the token.
                Audience = _configuration["Jwt:Audience"] // "aud" (audience) claim: Intended recipient of the token.
            };

            // Create the token based on the descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token to a JWT string.
            return tokenHandler.WriteToken(token);
        }


    }
