using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


[ApiController, Route("/api/v1/shipping")]
public class ShippingController : ControllerBase
{
  private readonly ShippingService _shippingService;
  public ShippingController(ShippingService shippingService)
  {
    _shippingService = shippingService;
  }


  // GET => /api/users => Get all the users
  [HttpGet]
  public async Task<IActionResult> Getshippment()
  {

    var getshipping = await _shippingService.GetShippingAsync();
    return Ok(getshipping);
  }

  [HttpGet("{ShippingId:guid}")]
  public async Task<IActionResult> GetShipping(Guid ShippingId)
  {
    var getSingelShipping = await _shippingService.GetShippingByIdAsync(ShippingId);
    return Ok(getSingelShipping);
  }
  [HttpPost]

  public async Task<IActionResult> CreateShipping([FromBody] CreateShippingDto newShipping)
  {
    var shipping = await _shippingService.CreateShippingAsync(newShipping);
    var response = new { Message = "An order will Shipp successfully", Shipping = shipping };
    return Created($"/api/shipping/{shipping.ShippingId}", response);
  }

  [HttpPut("{ShippingId:guid}")]
  public async Task<IActionResult> UpdateShippingByIdAsync(Guid ShippingId, [FromBody] UpdateShippingDto ShippingData)
  {
    var updatedshipping = await _shippingService.UpdateShippingByIdAsync(ShippingId, ShippingData);
    return Ok(updatedshipping);
  }

  [HttpDelete("{ShippingId:guid}")]
  public async Task<IActionResult> DeleteShipping(Guid ShippingId)
  {
    bool isShippDeleted = await _shippingService.DeleteShippingByIdAsync(ShippingId);
    return Ok(isShippDeleted);
  }
}


