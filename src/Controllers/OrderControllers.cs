

using Microsoft.AspNetCore.Mvc;

   [ApiController]
    [Route("/api/v1/orders")]
public class OrderControllers: ControllerBase {

    private readonly OrderServices _orderServices;


    public OrderControllers(OrderServices orderServices){

    _orderServices = orderServices;
  }


    [HttpPost]
   public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto createdOrder){

    var order =  await _orderServices.CreateOrderServiceAsync(createdOrder);


    var response = new { Message = "User created successfully", Order = order };
    return Created($"/api/users/{order.UserId}",response);

}
    [HttpGet]
    public async Task<IActionResult> GetOrders(){

    var orders =  await _orderServices.GetOrdersAsync();

    var response = new { StatusCode = 200, Message = "Users are returned successfully", Orders = orders };
    return Ok(response);
  }


    [HttpGet("{id}")]
    public async Task<IActionResult> FindOrderByid(Guid id){

    var order =   await _orderServices.FindOrderByIdServiceAsync(id);

    return Ok(order);
  }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderByid(Guid id){

    var order =   await _orderServices.DeleteUserByIdServiceAsync(id);

    if(!order){

      return ApiResponse.NotFound();
    }

    return ApiResponse.Success(order);
  }


    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateOrderByid(Guid id , UpdateOrderDto updateOrder){

    var updateData = await  _orderServices.UpdateOrderServiceAsync(id,updateOrder);

    return ApiResponse.Success(updateData , "The Order has bing updates succesfully");


  }





}
