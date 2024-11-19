using AutoMapper;
using Microsoft.EntityFrameworkCore;

public  interface ICategoryServices{
    public  Task<Category> CreateCategoryServiceAsync(CreateCategoryDto createCategory);
    public  Task<List<CategoryDto>> GetCategoryServiceAsync();
    public  Task<bool> DeleteCategoryByIdServiceAsync(Guid Id);
    public Task<CategoryDto> GetCategoryById(Guid categoryId);

}

public class CategoryService : ICategoryServices{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext appDbContext, IMapper mapper){
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Category> CreateCategoryServiceAsync(CreateCategoryDto newCategory){
        try{
            var slug = newCategory.Name.Replace(" ", "-");
            newCategory.Slug = slug;

            var category = _mapper.Map<Category>(newCategory);
            await _appDbContext.AddAsync(category);
            await _appDbContext.SaveChangesAsync();

            return category;
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

    public async Task<List<CategoryDto>> GetCategoryServiceAsync(){
        try{
            var categories = await _appDbContext.Categories.ToListAsync();
            var categoriesData = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesData;
        }
        catch (DbUpdateException dbEx){
            Console.WriteLine($"Database Update Error: {dbEx.Message}");
            throw new ApplicationException("An error occurred while saving to the database. Please check the data and try again.");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }
    }


  public async Task<CategoryDto> GetCategoryById(Guid categoryId){


    try{
      var findCategory = await  _appDbContext.Categories.FindAsync(categoryId);

      if(findCategory == null){
        return null;
      }

      var getCategoryById = await _appDbContext.Categories.Include(p=>p.Products).FirstOrDefaultAsync(c => c.CategoryId == categoryId);

      return _mapper.Map<CategoryDto>(getCategoryById);

    } catch (DbUpdateException dbEx){
            Console.WriteLine($"Database Update Error: {dbEx.Message}");
            throw new ApplicationException("An error occurred while saving to the database. Please check the data and try again.");
        }
        catch (Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }





  }




    public async Task<bool> DeleteCategoryByIdServiceAsync(Guid Id){
        try
        {
            var findCategory = await _appDbContext.Categories.FindAsync(Id);

            if (findCategory == null){return false;}

            _appDbContext.Remove(findCategory);
            await _appDbContext.SaveChangesAsync();

            return true;
        }catch (DbUpdateException dbEx){
            Console.WriteLine($"Database error related to the updated has happened {dbEx.Message}");
            throw new ApplicationException("An error has occurred while saving the data to the database");
        }catch (Exception ex){
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred. Please try again later.");
        }
    }


}
