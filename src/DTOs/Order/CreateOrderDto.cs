



public class  CreateOrderDto{



    public Guid UserId {get;set;}

       public ICollection<CreateOrderItemDto> OrderItems { get; set; } =new List<CreateOrderItemDto>();



  // public ICollection<CreateOrderProductDto> OrderProducts { get; set; } = new List<CreateOrderProductDto>();


}
