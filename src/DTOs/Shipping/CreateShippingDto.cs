public class CreateShippingDto
{
  public Status Status {get; set;}
  public Guid OrderId {get; set; }
  public int TrackingNumber { get; set; }
  public string ShippingDetails { get; set; }
}