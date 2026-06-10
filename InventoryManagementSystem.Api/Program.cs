using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
// Make sure to add the using statement for your API's DbContext location if needed!

var builder = WebApplication.CreateBuilder(args);

// 1. Add the Database Context using your Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Controllers
builder.Services.AddControllers();
// Register the service with a Scoped lifecycle (Created once per HTTP request)
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS POLICY: The Network Bridge
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        // BLAZOR APP'S Localhost URL
        policy.WithOrigins("https://localhost:7127")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthorization();
app.MapControllers();

app.Run();

