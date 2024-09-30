using AutoMapper;

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

        var product = _mapper.Map<Product>(createProduct);

        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        return product;
    }






    
}
