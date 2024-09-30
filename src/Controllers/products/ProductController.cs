using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/products")]

public class ProductControllers : ControllerBase
{

    private readonly ProductServices _productServices;


    public ProductControllers(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createdProduct)
    {
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
}