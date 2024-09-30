public class Shipping
{
  public Guid ShippingId { get; set; }
  public Guid OrderId { get; set; }
  public Status Status {get; set;}
  public int TrackingNumber { get; set; }
  public string ShippingDetails { get; set; }
  

}