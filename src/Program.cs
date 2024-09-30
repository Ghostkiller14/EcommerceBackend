using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<ShippingService>();

// inside the Startup file / Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();
app.Run();