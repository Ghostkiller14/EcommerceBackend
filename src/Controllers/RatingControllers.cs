using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/Ratings")]

public class RatingControllers: ControllerBase {
  private readonly IRatingServices _rateServices;

  public RatingControllers( IRatingServices rateServices){
    _rateServices = rateServices;
  }


  //[Authorize("User")]
  [HttpPost]
  public async Task<IActionResult> CreateRating([FromBody]CreateRatingDto createdRate){
    if (!ModelState.IsValid){
      return ApiResponse.BadRequest("Invalid Rating Data");
    }
    try{
      var rate =  await _rateServices.CreateRatingServiceAsync(createdRate);

      return ApiResponse.Created(rate, "Rate Created Successfully!");
    }catch(ApplicationException ex){
      return ApiResponse.ServerError("Server error: " + ex.Message);
    }catch(Exception ex){
      return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    }
}


  [HttpGet]
  public async Task<IActionResult> GetRatings(){
    try{
      var rate =  await _rateServices.GetRatingAsync();

      return ApiResponse.Success(rate, "Ratings returned Successfully");
    }catch(ApplicationException ex){
      return ApiResponse.ServerError("Server error: " + ex.Message);
    }catch(Exception ex){
      return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    }
  }

  [Authorize("Admin")]
  [HttpGet("{Id}")]
  public async Task<IActionResult> FindRatingById(Guid Id){
    try{
      var rate = await _rateServices.FindRatingByIdServiceAsync(Id);

      if (rate == null){
        return ApiResponse.NotFound("Rating id doesn't exist");
      }

      return ApiResponse.Success(rate, "Rate returned Successfully!");
    }catch(ApplicationException ex){
      return ApiResponse.ServerError("Server error: " + ex.Message);
    }catch(Exception ex){
      return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    }
  }

  [Authorize("User")]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteRatingById(Guid id){
    try{
      var rate = await _rateServices.DeleteRatingByIdServiceAsync(id);

      if (rate == false)
      {
        return ApiResponse.NotFound("Can't find Rating from ID to delete.");
      }

      return ApiResponse.Success(rate, "Successfully Deleted the rating");
    }catch(ApplicationException ex){
      return ApiResponse.ServerError("Server error: " + ex.Message);
    }catch(Exception ex){
      return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    }
  }


  [Authorize("User")]
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateRatingById(Guid id, UpdateRatingDto updateRating){
    try{
      var ratingData = await  _rateServices.UpdateRatingServiceAsync(id,updateRating);

      if (ratingData == null){
        return ApiResponse.NotFound("Rating Data wasn't found by the ID provided.");
      }
      return ApiResponse.Success(ratingData, "Rating updated Successfully!");
    }catch(ApplicationException ex){
      return ApiResponse.ServerError("Server error: " + ex.Message);
    }catch(Exception ex){
      return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    }
  }
}
