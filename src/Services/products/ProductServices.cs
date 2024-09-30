using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class ProductServices
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public ProductServices(AppDbContext appDbContext, IMapper mapper){
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Product> CreateProductServiceAsync(CreateProductDto createProduct)
    {
        try{

        var product = _mapper.Map<Product>(createProduct);

        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        return product;
        
        }catch(DbUpdateException ex){
            Console.WriteLine($"Database Update Err: {ex.Message}");
            throw new ApplicationException("A DB Error Happened during the creation of the Product");
        }catch(Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }
    }







    
}
