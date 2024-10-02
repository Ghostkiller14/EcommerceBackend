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
        try{
            var products =  await _appDbContext.Products.ToListAsync();
            var productData = _mapper.Map<List<ProductDto>>(products);

            return productData;
        }
        catch (DbUpdateException dbEx){
            Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
            throw new ApplicationException("An error has occurred while saving the data to the database");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error has occurred");
        }
    }

    public async Task<ProductDto> FindProductByIdServiceAsync(Guid Id){
        try{
            var findProduct = await _appDbContext.Products.FindAsync(Id);

            if(findProduct == null){
                return null;
            }

            var product = _mapper.Map<ProductDto>(findProduct);
            return product;
        }
        catch (DbUpdateException dbEx){
            Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
            throw new ApplicationException("An error has occurred while saving the data to the database");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error has occurred");
        }
    }

    public async Task<bool> DeleteProductByIdServiceAsync(Guid Id){
        try{
            var findProduct = await _appDbContext.Products.FindAsync(Id);

            if(findProduct == null){
                return false;
            }

            _appDbContext.Remove(findProduct);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (DbUpdateException dbEx){
            Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
            throw new ApplicationException("An error has occurred while saving the data to the database");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error has occurred");
        }
    }

    public async Task<ProductDto> UpdateProductServiceAsync(Guid Id , UpdateProductDto updateProduct){
        try{
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
        catch (DbUpdateException dbEx){
            Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
            throw new ApplicationException("An error has occurred while saving the data to the database");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error has occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error has occurred");
        }
    }
}