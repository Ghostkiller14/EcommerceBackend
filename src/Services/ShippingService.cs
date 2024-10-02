using Microsoft.EntityFrameworkCore;
using AutoMapper;

public interface IShippingServices
{
  public Task<Shipping> CreateShippingAsync(CreateShippingDto newShippingDto);
  public Task<List<ShippingDto>> GetShippingAsync();
  public Task<ShippingDto> UpdateShippingByIdAsync(Guid ShippingId, UpdateShippingDto ShippingData);
  public Task<ShippingDto> GetShippingByIdAsync(Guid ShippingId);
  public Task<bool> DeleteShippingByIdAsync(Guid shippingId);
}

public class ShippingService : IShippingServices
{
  private readonly AppDbContext _appDbContext;
  private readonly IMapper _mapper;

  public ShippingService(AppDbContext appDbContext, IMapper mapper)
  {
    _appDbContext = appDbContext;
    _mapper = mapper;
  }
  public async Task<Shipping> CreateShippingAsync(CreateShippingDto newShippingDto)
  {
    try{
      var shipping = _mapper.Map<Shipping>(newShippingDto);

      await _appDbContext.shippings.AddAsync(shipping);
      await _appDbContext.SaveChangesAsync();

      return shipping;
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
  public async Task<List<ShippingDto>> GetShippingAsync()
  {
    try{
      var shipments = await _appDbContext.shippings.ToListAsync();
      var shipmentsData = _mapper.Map<List<ShippingDto>>(shipments);

      return shipmentsData;
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
  public async Task<ShippingDto> UpdateShippingByIdAsync(Guid shippingId, UpdateShippingDto shippingData)
  {
    try{
      var updateShipping = await _appDbContext.shippings.FindAsync(shippingId);

      if(updateShipping == null){
        return null;
      }

      _mapper.Map(shippingData, updateShipping);

      _appDbContext.shippings.Update(updateShipping);
      await _appDbContext.SaveChangesAsync();

      var ShippingReturnDto = _mapper.Map<ShippingDto>(updateShipping);
      return ShippingReturnDto;
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
  public async Task<ShippingDto> GetShippingByIdAsync(Guid shippingId)
  {
    try{
      var getShippingById = await _appDbContext.shippings.FindAsync(shippingId);

      if (getShippingById == null)
      {
        return null;
      }

      var singleShipping = _mapper.Map<ShippingDto>(getShippingById);

      return singleShipping;
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }

  public async Task<bool> DeleteShippingByIdAsync(Guid shippingId)
  {
    try{
      var deletedShip = await _appDbContext.shippings.FindAsync(shippingId);

      if(deletedShip == null){
        return false;
      }

      _appDbContext.shippings.Remove(deletedShip);
      await _appDbContext.SaveChangesAsync();

      return true;
    }
    catch (ApplicationException dbEx){
      Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
      throw new ApplicationException("An error has occurred while saving the data to the database");
    }
    catch (Exception ex){
      Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
      throw new ApplicationException("An unexpected error has occurred");
    }
  }
}