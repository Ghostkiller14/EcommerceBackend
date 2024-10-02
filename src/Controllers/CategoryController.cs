using Microsoft.AspNetCore.Mvc;

[ApiController, Route("/api/v1/categories")]

public class CategoryController : ControllerBase {
    private readonly CategoryService _categoryService;
    public CategoryController(CategoryService categoryService) {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto newCategory) {
        if (!ModelState.IsValid) {
            return ApiResponse.BadRequest("Invalid category Data");
        }
        try {
            var category = await _categoryService.CreateCategoryServiceAsync(newCategory);
            return ApiResponse.Created(category, "Category has been created");
        }
        catch (ApplicationException ex) {
            return ApiResponse.ServerError("Server error: " + ex.Message);
        }
        catch (System.Exception ex) {
            return ApiResponse.ServerError("Server error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories() {
        try {
            var categories = await _categoryService.GetCategoryServiceAsync();
            return ApiResponse.Success(categories, "Categories are returned succesfully");
        }
        catch (ApplicationException ex) {
            return ApiResponse.ServerError("Server error: " + ex.Message);
        }
        catch (System.Exception ex) {
            return ApiResponse.ServerError("Server error: " + ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryById(Guid id){
        var category = await _categoryService.DeleteCategoryByIdServiceAsync(id);

        if(category == false){
          return BadRequest("The Id you trying to find is Not Exist");
        }

        var response = new {message = "Category Deleted successfully" , Category = category};
        return Ok(response);
    }
}