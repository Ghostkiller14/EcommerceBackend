
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/v1/Ratings")]

public class RatingControllers: ControllerBase {

    private readonly IRatingServices _rateServices;


    public RatingControllers( IRatingServices rateServices){

    _rateServices = rateServices;
  }


    [HttpPost]
   public async Task<IActionResult> CreateRating([FromBody]CreateRatingDto createdRate){


       if (!ModelState.IsValid)
      {
          // Log the errors or handle them as needed
          var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
          Console.WriteLine("Validation errors:");
          errors.ForEach(error => Console.WriteLine(error));

          // Return a custom response with validation errors
          return BadRequest(new { Message = "Validation failed", Errors = errors });
      }

    var rate =  await _rateServices.CreateRatingServiceAsync(createdRate);


    var response = new { Message = "User created successfully", Rating = rate };


    return Created($"/api/users/{rate.RatingId}",response);

}


    [HttpGet]
    public async Task<IActionResult> GetUsers(){

    var rate =  await _rateServices.GetRatingAsync();

    var response = new { StatusCode = 200, Message = "Users are returned successfully", Ratings = rate };
    return Ok(response);
  }


    [HttpGet("{Id}")]
    public async Task<IActionResult> FindatingById(Guid Id){

    var rate =   await _rateServices.FindRatingByIdServiceAsync(Id);

    return Ok(rate);
  }


    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteRatingById(Guid Id){

      var rate = await _rateServices.DeleteRatoingByIdServiceAsync(Id);

      if(rate == false){
        return BadRequest("The Id you trying to find is Not Exist");
      }

      var response = new {message = "UserDeleted successfully" , Rate = rate};

      return Ok(response);

    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateRatingById(Guid Id, UpdateRatingDto updateRating){

      var userData = await  _rateServices.UpdateRatingServiceAsync(Id,updateRating);

      return Ok(userData);

    }
  }
