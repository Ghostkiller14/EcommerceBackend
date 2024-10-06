



public class  CreateOrderDto{



    public Guid UserId {get;set;}

    public ICollection<CreateOrderProductDto> OrderProducts { get; set; } = new List<CreateOrderProductDto>();





}
