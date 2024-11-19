using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

var jwtKey = Environment.GetEnvironmentVariable("JWT__KEY") ?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER") ?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
var jwtAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE") ?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");
var dbConnectionaString = Environment.GetEnvironmentVariable("DEFAILT__CONNECTION__STRING") ?? throw new InvalidOperationException("datbase connection url is missing in environment variables.");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConnectionaString));

builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disable automatic model validation response
});
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IRatingServices , RatingServices>();
builder.Services.AddScoped<ICategoryServices , CategoryService>();
builder.Services.AddScoped<IProductServices,ProductServices>();
builder.Services.AddScoped<IOrderService,OrderService>();


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAutoMapper(typeof(Program));
var Configuration = builder.Configuration; // Ensure this is accessible

var key = Encoding.ASCII.GetBytes(jwtKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = Configuration["Jwt:Issuer"],
        ValidAudience = Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement

    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod() // Allows all HTTP methods
            .AllowCredentials();
    });
});



var app = builder.Build();

app.MapGet("/", () => "Api running well");

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();
