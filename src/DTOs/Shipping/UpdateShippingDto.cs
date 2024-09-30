public class UpdateShippingDto
{
  public Guid ShippingId { get; set; }
  public Guid OrderId { get; set; }
  public Status Status {get; set;}
}