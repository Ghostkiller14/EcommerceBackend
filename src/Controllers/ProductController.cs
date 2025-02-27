using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/v1/products")]

public class ProductControllers : ControllerBase{

    private readonly IProductServices _productServices;

    public ProductControllers(IProductServices productServices){
        _productServices = productServices;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createdProduct){
        try{
            var product = await _productServices.CreateProductServiceAsync(createdProduct);

            if (!ModelState.IsValid){
                return ApiResponse.BadRequest("Product data input is incorrect");
            }

            return ApiResponse.Created(product, "the Product has been created Successfully!");
        }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }
    }

    // [HttpGet]
    // public async Task<IActionResult> GetProducts(){
    //     try{
    //         var products =  await _productServices.GetProductAsync();

    //         return ApiResponse.Success(products, "Products returned Successfully!");
    //     }catch(ApplicationException ex){
    //       return ApiResponse.ServerError("Server error: " + ex.Message);
    //     }catch(Exception ex){
    //       return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
    //     }
    // }

  // [HttpGet("sort")]
  // public async Task<IActionResult> SortProducts([FromQuery] QueryParameters queryParams){

  //   var product = await _productServices.SortProducts(queryParams);

  //   if(!product.Any()){
  //     return ApiResponse.NotFound("No products found To Sort.");

  //   }



  //   return ApiResponse.Success(product, "Products sorted successfully.");

  // }


  [HttpGet]
   public async Task<IActionResult> GetAllProducts([FromQuery] QueryParameters queryParams )
  {
    Console.WriteLine($"{queryParams}");

    var products =await  _productServices.GetAllProductsServiceAsync(queryParams);

    Console.WriteLine($"{products}");


    if(!products.Items.Any()){
    return ApiResponse.NotFound("No products found.:-)");

    }
    return ApiResponse.Success(products, "Products return successfully.");
  }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindProductById(Guid id){
        try{
            var product =   await _productServices.FindProductByIdServiceAsync(id);

            if (product == null){
                return ApiResponse.BadRequest("Invalid ID not found");
            }

            return ApiResponse.Success(product, "Product by ID found Successfully!");
        }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }
    }






    [HttpGet("product-name/{name}")]
    public async Task<IActionResult> FindProductByName(string name){

    try{
            var product =   await _productServices.FindProductByNameAsync(name);

            if (product == null){
                return ApiResponse.BadRequest(" name not found");
            }

            return ApiResponse.Success(product, "Product by ID found Successfully!");
        }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }




    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductById(Guid id){
        try{
            var product = await _productServices.DeleteProductByIdServiceAsync(id);

            if(product == false){
                return BadRequest("The Id you trying to find does Not Exist");
            }

            return ApiResponse.Success(product, "Product Deleted Successfully from ID");
        }catch(ApplicationException ex){
          return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
          return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductById(Guid id, UpdateProductDto updateProduct){
        try{
            var productData = await _productServices.UpdateProductServiceAsync(id,updateProduct);

            if (!ModelState.IsValid){
                return ApiResponse.BadRequest("Can't find ID of Product.");
            }

            return ApiResponse.Success(productData, "Product has been Updated Successfully!");
        }catch(ApplicationException ex){
            return ApiResponse.ServerError("Server error: " + ex.Message);
        }catch(Exception ex){
            return ApiResponse.ServerError("unexpected error has happened: " + ex.Message);
        }
    }
}
