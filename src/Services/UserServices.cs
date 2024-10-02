using AutoMapper;
using Microsoft.EntityFrameworkCore;

public interface IUserServices
{
  public Task<User> CreateUserServiceAsync(CreateUserDto createUser);
  public Task<List<UserDto>> GetUserAsync();
  public Task<UserDto> FindUserByIdServiceAsync(Guid Id);
  public Task<bool> DeleteUserByIdServiceAsync(Guid Id);
  public Task<UserDto> UpdateUserServiceAsync(Guid Id, UpdateUserDto updateUser);
}

public class UserServices : IUserServices
{
  private readonly AppDbContext _appDbContext;
  private readonly IMapper _mapper;

  public UserServices(AppDbContext appDbContext, IMapper mapper)
  {
    _mapper = mapper;
    _appDbContext = appDbContext;
  }

  public async Task<User> CreateUserServiceAsync(CreateUserDto createUser)
  {
    try
    {
      var user = _mapper.Map<User>(createUser);

      await _appDbContext.Users.AddAsync(user);
      await _appDbContext.SaveChangesAsync();

      return user;
    }
    catch (DbUpdateException dbEx)
    {
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  public async Task<List<UserDto>> GetUserAsync()
  {
    try
    {
      var users = await _appDbContext.Users.ToListAsync();
      var requiredUserData = _mapper.Map<List<UserDto>>(users);

      return requiredUserData;
    }
    catch (DbUpdateException dbEx)
    {
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
  
  public async Task<UserDto> FindUserByIdServiceAsync(Guid Id)
  {
    try
    {
      var findUser = await _appDbContext.Users.FindAsync(Id);

      if (findUser == null)
      {
        return null;
      }

      var userData = _mapper.Map<UserDto>(findUser);

      return userData;
    }
    catch (DbUpdateException dbEx)
    {
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  public async Task<bool> DeleteUserByIdServiceAsync(Guid Id)
  {
    try
    {
      var findUser = await _appDbContext.Users.FindAsync(Id);

      if (findUser == null)
      {
        return false;

      }

      _appDbContext.Remove(findUser);
      await _appDbContext.SaveChangesAsync();

      return true;
    }
    catch (DbUpdateException dbEx)
    {
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  public async Task<UserDto> UpdateUserServiceAsync(Guid Id, UpdateUserDto updateUser)
  {
    try
    {
      var findUser = await _appDbContext.Users.FindAsync(Id);

      if (findUser == null)
      {
        return null;
      }
      
      _mapper.Map(updateUser, findUser);

      _appDbContext.Users.Update(findUser);
      await _appDbContext.SaveChangesAsync();

      var userData = _mapper.Map<UserDto>(findUser);
      return userData;
    }
    catch (DbUpdateException dbEx)
    {
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
}
