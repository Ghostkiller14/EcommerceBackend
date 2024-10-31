using AutoMapper;
using Microsoft.EntityFrameworkCore;

public  interface IProductServices{
    public  Task<ProductDto> CreateProductServiceAsync(CreateProductDto createProduct);
    public PagedResult<ProductDto> GetAllProducts(int pageNumber, int pageSize);
    public  Task<List<ProductDto>> GetProductAsync();
    public  Task<ProductDto> FindProductByIdServiceAsync(Guid Id);
    public  Task<List<ProductDto>> FindProductByNameAsync(string name);
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

    public async Task<ProductDto> CreateProductServiceAsync(CreateProductDto createProduct){
        try{
            var product = _mapper.Map<Product>(createProduct);

            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

           var productData = _mapper.Map<ProductDto>(product);

            return productData;

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
            var products =  await _appDbContext.Products.Include(c =>c.Category).ToListAsync();

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


    public PagedResult<ProductDto> GetAllProducts(int pageNumber, int pageSize){
      var totalProducts = _appDbContext.Products.Count();
      Console.WriteLine($"{totalProducts}");

      var paginatedProducts = _appDbContext.Products.Skip((pageNumber -1) * pageSize).Take(pageSize).Select(p => new ProductDto {

        ProductId = p.ProductId,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        CreatedAt = p.CreatedAt
      }).ToList();

      return new PagedResult<ProductDto> {
      PageNumber = pageNumber,
      PageSize = pageSize,
      TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
      TotalItems = totalProducts,
      Items = paginatedProducts
      };






    }


    public async Task<ProductDto> FindProductByIdServiceAsync(Guid id){
        try{
            var findProduct = await _appDbContext.Products.Include(r =>r.Ratings).Include(c => c.Category).FirstOrDefaultAsync(p => p.ProductId == id) ;

            if(findProduct == null){
                return null;
            }

            // int TotalRatingScore = findProduct.Ratings.Where(r => r != null)
            // .Sum( r => r.RatingScore);

            // if(findProduct.Ratings.Count() <= 0){
            //   return null;

            // }
            // float Average = TotalRatingScore/(findProduct.Ratings.Count());


            var product = _mapper.Map<ProductDto>(findProduct);

            // product.AverageRatingScore = Average;
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

    public async Task<List<ProductDto>> FindProductByNameAsync(string name){
            try{
            var findProduct = await _appDbContext.Products.Include(r =>r.Ratings).Include(c => c.Category).Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync() ;

            if(findProduct == null){
              return null;
            }

            var product = _mapper.Map<List<ProductDto>>(findProduct);


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
