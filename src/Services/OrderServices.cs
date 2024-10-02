

using AutoMapper;
using Microsoft.EntityFrameworkCore;
public class OrderServices{


 private readonly AppDbContext _appDbContext;
   private readonly IMapper _mapper;



      public OrderServices(AppDbContext appDbContext , IMapper mapper){
          _mapper = mapper;
          _appDbContext = appDbContext;
      }

  public async Task<Order> CreateOrderServiceAsync(CreateOrderDto createOrder){

      // Map Create User To user
        var order = _mapper.Map<Order>(createOrder);

        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();

        return order;

  }



  public async Task<List<OrderDto>> GetOrdersAsync(){

    var orders =  await _appDbContext.Orders.ToListAsync();

    var requiredorderData = _mapper.Map<List<OrderDto>>(orders);

    return requiredorderData;


  }


public async Task<OrderDto> FindOrderByIdServiceAsync(Guid Id){

    var findOrder = await _appDbContext.Orders.Include(u => u.User).FirstOrDefaultAsync(o => o.OrderId == Id );

    if(findOrder == null){
      return null;
    }


      var userData = _mapper.Map<OrderDto>(findOrder);

    return userData;


  }



  public async Task<bool> DeleteUserByIdServiceAsync(Guid Id){

        var findOrder = await _appDbContext.Orders.FindAsync(Id);


        if(findOrder == null){
          return false;

        }

        _appDbContext.Remove(findOrder);
        await _appDbContext.SaveChangesAsync();

        return true;

  }





  public async Task<OrderDto> UpdateOrderServiceAsync(Guid Id , UpdateOrderDto updateOrder){
    var findOrder = await _appDbContext.Orders.FindAsync(Id);

    if (findOrder == null){
      return null;
    }

    // Map the updateUser values to the found user entity
    _mapper.Map(updateOrder, findOrder);

    // Update the entity in the database
    _appDbContext.Orders.Update(findOrder);
    await _appDbContext.SaveChangesAsync();

    // Map the updated entity to UserDto
    var orderData = _mapper.Map<OrderDto>(findOrder);

    return orderData;
  }




}
