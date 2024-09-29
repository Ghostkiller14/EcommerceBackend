using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

public class UserServices{
  private readonly AppDbContext _appDbContext;
  private readonly IMapper _mapper;

  public UserServices(AppDbContext appDbContext, IMapper mapper){
    _appDbContext = appDbContext;
    _mapper = mapper;
  }

  public async Task<User> CreateUserServiceAsync(CreateUserDto createUser){

    var user = _mapper.Map<User>(createUser);

    await _appDbContext.Users.AddAsync(user);
    await _appDbContext.SaveChangesAsync();

    return user;
  }
//   public List<UserDto> GetUsersService(){


//     var users = _user.Select(users => new UserDto{

//       UserId = users.UserId,
//       UserName = users.UserName,
//       Email = users.Email,
//       Age = users.Age

//     }).ToList();


//     return users;

//   }

//   public UserDto GetUserByIdService(Guid Id){


//     var userFound = _user.FirstOrDefault(user => user.Id == Id);
//     if(userFound == null){
//       return null;
//     }


//     var user = new UserDto{
//       Id = userFound.Id,
//       Name = userFound.Name,
//       UserName = userFound.UserName,
//       Email = userFound.Email,
//       Age = userFound.Age


//     };


//     return user;









//   }
//   public bool DeleteUserService(Guid Id){

//     var user = _user.FirstOrDefault(user => user.Id == Id);

//     if(user == null){
//       return false;
//     }

//     _user.Remove(user);

//     return true;
//   }

}
