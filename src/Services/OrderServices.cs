using AutoMapper;
using Microsoft.EntityFrameworkCore;


public interface IOrderService{
    public  Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    public  Task<OrderDto?> GetOrderByIdAsync(Guid orderId);

    public  Task<OrderDto?> UpdateOrderAsync(Guid orderId, CreateOrderDto updateOrderDto);

    public  Task<bool> DeleteOrderByIdAsync(Guid orderId);


}



public class OrderService:IOrderService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public OrderService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    // Create a new order
    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
      try{

        var order = new Order
        {
            UserId = createOrderDto.UserId,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var OrderItemDto in createOrderDto.OrderItem)
        {
          // check whether this product is existed or not
          // quantity or stock
           var orderProduct = new OrderItem
           {
        ProductId = OrderItemDto.ProductId,
        Quantity = OrderItemDto.Quantity,
        Price = OrderItemDto.Price
        };
        order.OrderItem.Add(orderProduct);

        }


        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();

        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
      }
       catch(DbUpdateException ex){
            Console.WriteLine($"Database Update Err: {ex.Message}");
            throw new ApplicationException("A DB Error Happened during the creation of the Product");
        }catch(Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");

    }
  }

    // Get order by ID
    public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
    {
      try{
       var order = await _appDbContext.Orders
        .Include(o => o.OrderItem)
        .ThenInclude(op => op.Product)  // Include Product details
        .Include(o => o.User)  // Include User details
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
            return null;

        return _mapper.Map<OrderDto>(order);


      } catch(DbUpdateException ex){
            Console.WriteLine($"Database Update Err: {ex.Message}");
            throw new ApplicationException("A DB Error Happened during the creation of the Product");
        }catch(Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");


    }
  }

    // Update an order
    public async Task<OrderDto?> UpdateOrderAsync(Guid orderId, CreateOrderDto updateOrderDto)
    {

      try{
        var order = await _appDbContext.Orders
            .Include(o => o.OrderItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
            return null;

        // Clear existing products and add updated products
        order.OrderItem.Clear();
        foreach (var orderProductDto in updateOrderDto.OrderItem)
        {
            var orderProduct = new OrderItem
            {
                ProductId = orderProductDto.ProductId,
                Quantity = orderProductDto.Quantity,
                Price = orderProductDto.Price
            };

            order.OrderItem.Add(orderProduct);
        }

        _appDbContext.Orders.Update(order);
        await _appDbContext.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);

      } catch(DbUpdateException ex){
            Console.WriteLine($"Database Update Err: {ex.Message}");
            throw new ApplicationException("A DB Error Happened during the creation of the Product");
        }catch(Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }


    }

    // Delete an order by ID
    public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
    {
      try{
        var order = await _appDbContext.Orders
            .Include(o => o.OrderItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
            return false;

        _appDbContext.Orders.Remove(order);
        await _appDbContext.SaveChangesAsync();
        return true;
      } catch(DbUpdateException ex){
            Console.WriteLine($"Database Update Err: {ex.Message}");
            throw new ApplicationException("A DB Error Happened during the creation of the Product");
        }catch(Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }

    }
}
