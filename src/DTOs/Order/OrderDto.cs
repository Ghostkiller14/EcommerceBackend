public class OrderDto{

  public Guid OrderId {get;set;}

  public Guid UserId {get;set;}
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();



}
