using AutoMapper;
using Microsoft.EntityFrameworkCore;

public interface IRatingServices
{
  public Task<Rating> CreateRatingServiceAsync(CreateRatingDto createRating);
  public Task<List<RatingDto>> GetRatingAsync();
  public Task<RatingDto> FindRatingByIdServiceAsync(Guid Id);
  public Task<bool> DeleteRatingByIdServiceAsync(Guid Id);
  public Task<RatingDto> UpdateRatingServiceAsync(Guid Id, UpdateRatingDto updateRate);
}

public class RatingServices : IRatingServices
{
  private readonly AppDbContext _appDbContext;
  private readonly IMapper _mapper;

  public RatingServices(AppDbContext appDbContext, IMapper mapper)
  {
    _mapper = mapper;
    _appDbContext = appDbContext;
  }


  public async Task<Rating> CreateRatingServiceAsync(CreateRatingDto createRating)
  {
    try
    {
      var rate = _mapper.Map<Rating>(createRating);

      await _appDbContext.Ratings.AddAsync(rate);
      await _appDbContext.SaveChangesAsync();

      return rate;
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

  public async Task<List<RatingDto>> GetRatingAsync()
  {
    try
    {
      var Ratings = await _appDbContext.Ratings.ToListAsync();
      var requiredRatingData = _mapper.Map<List<RatingDto>>(Ratings);

      return requiredRatingData;
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

  public async Task<RatingDto> FindRatingByIdServiceAsync(Guid Id)
  {
    try
    {
      var findRating = await _appDbContext.Ratings.FindAsync(Id);

      if (findRating == null)
      {
        return null;
      }

      var ratingData = _mapper.Map<RatingDto>(findRating);

      return ratingData;
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

  public async Task<bool> DeleteRatingByIdServiceAsync(Guid Id)
  {
    try
    {
      var findRating = await _appDbContext.Ratings.FindAsync(Id);

      if (findRating == null)
      {
        return false;
      }

      _appDbContext.Remove(findRating);
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

  public async Task<RatingDto> UpdateRatingServiceAsync(Guid Id, UpdateRatingDto updateRate)
  {
    try
    {
      var findRate = await _appDbContext.Ratings.FindAsync(Id);

      if (findRate == null)
      {
        return null;
      }

      findRate.FeedBack = updateRate.FeedBack ?? findRate.FeedBack;
      findRate.RatingScore = updateRate.RatingScore;

      _appDbContext.Ratings.Update(findRate);

      await _appDbContext.SaveChangesAsync();
      var RateData = _mapper.Map<RatingDto>(findRate);

      return RateData;
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