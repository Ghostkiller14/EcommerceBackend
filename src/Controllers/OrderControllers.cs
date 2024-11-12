
using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllOrder(){
      try{

        var orders = await  _orderService.GetAllOrdersServiceAsync();

        if(orders == null){
          return ApiResponse.NotFound("There is no order Founds");
        }

        return ApiResponse.Success(orders, "All orders has returned Successfully :-)");

      }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }



    }

    // POST: /api/orders
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
      try{

          var order = await _orderService.CreateOrderAsync(createOrderDto);


        if (!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        return ApiResponse.Created(order, "the Order has been created Successfully! :-)");

      }
     catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }

    }

    // GET: /api/orders/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {

      try{

        var order = await _orderService.GetOrderByIdAsync(id);

        if(order == null){
          return ApiResponse.BadRequest("Invalid ID not found");
        }
        return ApiResponse.Success(order,"Order has Returned Successfully :-)");
      }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }





    }

    // PUT: /api/orders/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] CreateOrderDto updateOrderDto)
    {

      try{

        var updatedOrder = await _orderService.UpdateOrderAsync(id, updateOrderDto);
          if (!ModelState.IsValid){
             return BadRequest();
          }

          if(updatedOrder == null){
            return ApiResponse.BadRequest("Invalid ID not found :-( ");
          }

        return ApiResponse.Success(updatedOrder, "Order has been Updated Successfully :-)");
      }
          catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }


      }

    // DELETE: /api/orders/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
      try{
        var result = await _orderService.DeleteOrderByIdAsync(id);


        if (!result){
            return ApiResponse.BadRequest("The Id you trying to find does Not Exist");
      }

      return ApiResponse.Success(result , "Order has Deleted Successfully :-) ");
      }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }


    }
}





