using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/products")]

public class ProductControllers : ControllerBase{

    private readonly IProductServices _productServices;


    public ProductControllers(ProductServices productServices){
        _productServices = productServices;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createdProduct){
        if (!ModelState.IsValid){
            // Log the errors or handle them as needed
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            Console.WriteLine("Validation errors:");
            errors.ForEach(error => Console.WriteLine(error));
            // Return a custom response with validation errors
            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }

        var product = await _productServices.CreateProductServiceAsync(createdProduct);
        var response = new { Message = "Product created successfully", Product = product };

        return Created($"/api/v1/products/{product.ProductId}", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(){
        var products =  await _productServices.GetProductAsync();

        var response = new { StatusCode = 200, Message = "Products are returned successfully", Products = products};
        return Ok(response);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> FindProductById(Guid Id){
        var product =   await _productServices.FindProductByIdServiceAsync(Id);
        return Ok(product);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteProductById(Guid Id){
        var product = await _productServices.DeleteProductByIdServiceAsync(Id);

        if(product == false){
          return BadRequest("The Id you trying to find is Not Exist");
        }

        var response = new {message = "Product Deleted successfully" , Product = product};
        return Ok(response);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateProductById(Guid Id, UpdateProductDto updateProduct){
        var productData = await  _productServices.UpdateProductServiceAsync(Id,updateProduct);
        return Ok(productData);
    }
}