



using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IRatingServices{

  public  Task<Rating> CreateRatingServiceAsync(CreateRatingDto createRating);
  public  Task<List<RatingDto>> GetRatingAsync();
  public  Task<RatingDto> FindRatingByIdServiceAsync(Guid Id);
  public  Task<bool> DeleteRatoingByIdServiceAsync(Guid Id);
  public Task<RatingDto> UpdateRatingServiceAsync(Guid Id , UpdateRatingDto updateRate);

}

public class RatingServices : IRatingServices{


    private readonly AppDbContext _appDbContext;
     private readonly IMapper _mapper;

      public RatingServices(AppDbContext appDbContext , IMapper mapper){
          _mapper = mapper;
          _appDbContext = appDbContext;
      }


public async Task<Rating> CreateRatingServiceAsync(CreateRatingDto createRating){



      // Map Create User To user
        var rate = _mapper.Map<Rating>(createRating);

        await _appDbContext.Ratings.AddAsync(rate);
        await _appDbContext.SaveChangesAsync();

        return rate;

  }


  public async Task<List<RatingDto>> GetRatingAsync(){

    var Ratings =  await _appDbContext.Ratings.ToListAsync();

    var requiredRatingData = _mapper.Map<List<RatingDto>>(Ratings);


    return requiredRatingData;

  }



  public async Task<RatingDto> FindRatingByIdServiceAsync(Guid Id){

    var findUser = await _appDbContext.Ratings.FindAsync(Id);

    if(findUser == null){
      return null;
    }

      var userData = _mapper.Map<RatingDto>(findUser);

      return userData;


  }


  public async Task<bool> DeleteRatoingByIdServiceAsync(Guid Id){

        var findRating = await _appDbContext.Ratings.FindAsync(Id);


        if(findRating == null){
          return false;

        }

        _appDbContext.Remove(findRating);
        await _appDbContext.SaveChangesAsync();

        return true;

  }


  public async Task<RatingDto> UpdateRatingServiceAsync(Guid Id , UpdateRatingDto updateRate){

       var findRate = await _appDbContext.Ratings.FindAsync(Id);


       if(findRate == null){
        return null;
       }

       findRate.FeedBack = updateRate.FeedBack ?? findRate.FeedBack;
       findRate.RatingScore = updateRate.RatingScore;



         _appDbContext.Ratings.Update(findRate);

         await _appDbContext.SaveChangesAsync();


         var RateData = _mapper.Map<RatingDto>(findRate);

         return RateData;

  }










}
