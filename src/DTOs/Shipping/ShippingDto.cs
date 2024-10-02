public class ShippingDto
{
  public Guid OrderId { get; set; }
  public Status Status {get; set;}
  public int TrackingNumber { get; set; }
  public string ShippingDetails { get; set; }
}