using Microsoft.EntityFrameworkCore;

public class ShippingService
{
  private readonly AppDbContext _appDbContext;

  public ShippingService(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }
  public async Task<Shipping> CreateShippingAsync(CreateShippingDto newShippingDto)
  {
    // Hash the password using a library like BCrypt.Net
    // install => dotnet add package BCrypt.Net-Next


    var shipping = new Shipping
    {
      ShippingId = Guid.NewGuid(),
      Status = newShippingDto.Status,
      TrackingNumber = newShippingDto.TrackingNumber,
      ShippingDetails = newShippingDto.ShippingDetails,
    };

    await _appDbContext.shippings.AddAsync(shipping);
    await _appDbContext.SaveChangesAsync();
    return shipping;
  }
  public async Task<List<Shipping>> GetShippingAsync()
  {
    var shipps = await _appDbContext.shippings.ToListAsync();
    return shipps;
  }
  public async Task<Shipping> UpdateShippingByIdAsync(Guid ShippingId, UpdateShippingDto ShippingData)
  {
    var updatesshipp = await _appDbContext.shippings.FindAsync(ShippingId);
    updatesshipp.Status = ShippingData.Status;

    _appDbContext.shippings.Update(updatesshipp);
    await _appDbContext.SaveChangesAsync();
    return updatesshipp;

  }
  public async Task<Shipping> GetShippingByIdAsync(Guid ShippingId)
  {
    var getShippingById = await _appDbContext.shippings.FindAsync(ShippingId);

    if (getShippingById == null)
    {
      return null;
    }
    var singleShipping = new Shipping
    {
      ShippingId = getShippingById.ShippingId,
      Status = getShippingById.Status,
      TrackingNumber = getShippingById.TrackingNumber,
      ShippingDetails = getShippingById.ShippingDetails,
    };
    return singleShipping;
  }

public async Task<bool> DeleteShippingByIdAsync(Guid shippingId){
  var Deletedshipp =await _appDbContext.shippings.FindAsync(shippingId);
  _appDbContext.shippings.Remove(Deletedshipp);
  await _appDbContext.SaveChangesAsync();
  return true;
}
}