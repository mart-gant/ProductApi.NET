using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProductApi.Data;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YourProjectName API", Version = "v1" });
});

// Configure Entity Framework and the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IProductService, ProductService>();
IServiceCollection serviceCollection = builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourProjectName API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Define the interfaces in separate files
// ProductApi/Services/IProductService.cs
public interface IProductService
{
    // Define methods for product-related operations
}

// ProductApi/Services/IWarehouseService.cs
public interface IWarehouseService
{
    // Define methods for warehouse-related operations
}

// ProductApi/Services/IAvailabilityService.cs
public interface IAvailabilityService
{
    // Define methods for availability-related operations
}
