using AutoMapper;
using Microsoft.EntityFrameworkCore;

public  interface IProductServices{
    public  Task<Product> CreateProductServiceAsync(CreateProductDto createProduct);
    public  Task<List<ProductDto>> GetProductAsync();
    public  Task<ProductDto> FindProductByIdServiceAsync(Guid Id);
    public  Task<bool> DeleteProductByIdServiceAsync(Guid Id);
    public  Task<ProductDto> UpdateProductServiceAsync(Guid Id , UpdateProductDto updateProduct);
}
public class ProductServices: IProductServices{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public ProductServices(AppDbContext appDbContext, IMapper mapper){
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Product> CreateProductServiceAsync(CreateProductDto createProduct){
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

    public async Task<List<ProductDto>> GetProductAsync(){
        var products =  await _appDbContext.Products.ToListAsync();
        var requiredProductData = _mapper.Map<List<ProductDto>>(products);

        return requiredProductData;
    }

    public async Task<ProductDto> FindProductByIdServiceAsync(Guid Id){
        var findProduct = await _appDbContext.Products.FindAsync(Id);

        if(findProduct == null){
          return null;
        }

        var product = _mapper.Map<ProductDto>(findProduct);
        return product;
    }

    public async Task<bool> DeleteProductByIdServiceAsync(Guid Id){
        var findProduct = await _appDbContext.Products.FindAsync(Id);

        if(findProduct == null){
          return false;
        }

        _appDbContext.Remove(findProduct);
        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<ProductDto> UpdateProductServiceAsync(Guid Id , UpdateProductDto updateProduct){

       var findProduct = await _appDbContext.Products.FindAsync(Id);

       if(findProduct == null){
        return null;
       }

        _mapper.Map(updateProduct, findProduct);
        _appDbContext.Products.Update(findProduct);
        await _appDbContext.SaveChangesAsync();
        
        var productData = _mapper.Map<ProductDto>(findProduct);

        return productData;
    }

}