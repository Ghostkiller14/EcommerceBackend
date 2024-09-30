
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


public class UserServices{

   private readonly AppDbContext _appDbContext;
   private readonly IMapper _mapper;

      public UserServices(AppDbContext appDbContext , IMapper mapper){
          _mapper = mapper;
          _appDbContext = appDbContext;
      }

  public async Task<User> CreateUserServiceAsync(CreateUserDto createUser){



      // Map Create User To user
        var user = _mapper.Map<User>(createUser);

        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();

        return user;

  }


  public async Task<List<UserDto>> GetUserAsync(){

    var users =  await _appDbContext.Users.ToListAsync();

    var requiredUserData = users.Select(user => new UserDto{

      UserId = user.UserId,
      UserName = user.UserName,
      Email = user.Email,
      Address = user.Address,
      Age = user.Age
    }).ToList();

    return requiredUserData;


  }



  public async Task<UserDto> FindUserByIdServiceAsync(Guid Id){

    var findUser = await _appDbContext.Users.FindAsync(Id);

    if(findUser == null){
      return null;
    }

    //Map user to userDto


      var userData = _mapper.Map<UserDto>(findUser);
    // var userData = new UserDto{
    //   UserId = findUser.UserId,
    //   UserName = findUser.UserName,
    //   Email = findUser.Email,
    //   Age = findUser.Age,
    //   Address = findUser.Address
    // };

    return userData;


  }


  public async Task<bool> DeleteUserByIdServiceAsync(Guid Id){

        var findUser = await _appDbContext.Users.FindAsync(Id);


        if(findUser == null){
          return false;

        }

        _appDbContext.Remove(findUser);
        await _appDbContext.SaveChangesAsync();

        return true;

  }


  public async Task<UserDto> UpdateUserServiceAsync(Guid Id , UpdateUserDto updateUser){

       var findUser = await _appDbContext.Users.FindAsync(Id);

       if(findUser == null){
        return null;
       }


        findUser.UserName = updateUser.UserName ?? findUser.UserName;
        findUser.Password = updateUser.Password ?? findUser.Password;
        findUser.Email = updateUser.Email ?? findUser.Email;
        findUser.Age = updateUser.Age  != 0 ? updateUser.Age : findUser.Age;

         _appDbContext.Users.Update(findUser);

         await _appDbContext.SaveChangesAsync();


         var userData = _mapper.Map<UserDto>(findUser);

         return userData;

  }


 }
