using Microsoft.EntityFrameworkCore;

//TO DO
// create seeding data 
// linq query : join , left join 
// groupBy() as a filter for products or anything else
// Google Authentication
// inside user entity: isGoogleLogin true or false 
//TO DO 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disable automatic model validation response
});
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRatingServices , RatingServices>();

builder.Services.AddScoped<ProductServices>();

builder.Services.AddScoped<OrderServices>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.MapGet("/", () => "Api running well");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
