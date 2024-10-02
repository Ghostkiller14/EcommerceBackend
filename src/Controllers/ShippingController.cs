using Microsoft.AspNetCore.Mvc;

[ApiController, Route("/api/v1/shipping")]
public class ShippingController : ControllerBase
{
  private readonly ShippingService _shippingService;
  public ShippingController(ShippingService shippingService)
  {
    _shippingService = shippingService;
  }

  [HttpGet]
  public async Task<IActionResult> GetShippment()
  {
    try{
      var getShipping = await _shippingService.GetShippingAsync();
      return ApiResponse.Success(getShipping, "Shipping data Retrived");
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  [HttpGet("{shippingId:guid}")]
  public async Task<IActionResult> GetShipping(Guid shippingId)
  {
    try{
      var getSingleShipping = await _shippingService.GetShippingByIdAsync(shippingId);
      return ApiResponse.Success(getSingleShipping, "Shippment Details returned Successfully!");
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
  [HttpPost]
  public async Task<IActionResult> CreateShipping([FromBody] CreateShippingDto newShipping)
  {
    try{
      var shipping = await _shippingService.CreateShippingAsync(newShipping);
      return ApiResponse.Created(shipping, "Shipping Created Successfully!");
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  [HttpPut("{shippingId:guid}")]
  public async Task<IActionResult> UpdateShippingByIdAsync(Guid shippingId, [FromBody] UpdateShippingDto ShippingData)
  {
    try{
      var updatedshipping = await _shippingService.UpdateShippingByIdAsync(shippingId, ShippingData);
      return ApiResponse.Success(updatedshipping, "Shipping Updated Successfully!");
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  [HttpDelete("{shippingId:guid}")]
  public async Task<IActionResult> DeleteShipping(Guid shippingId)
  {
  try{
      bool isShippDeleted = await _shippingService.DeleteShippingByIdAsync(shippingId);
      return ApiResponse.Success(isShippDeleted, "Shipping Deleted Successfully!");
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
}