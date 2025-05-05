global using ShopWebsite.Shared.Models.Data;
global using ShopWebsite.Shared.Models.DataTransferObjects;
global using Microsoft.EntityFrameworkCore;
global using ShopWebsite.Server.Data;
global using ShopWebsite.Server.Services.ProductService;
global using ShopWebsite.Server.Services.CategoryService;
global using ShopWebsite.Server.Services.CartService;
global using ShopWebsite.Server.Services.AuthService;
global using ShopWebsite.Server.Services.OrderService;
global using ShopWebsite.Server.Services.PaymentService;
global using ShopWebsite.Server.Services.AddressService;
global using ShopWebsite.Server.Services.ProductTypeService;
using Microsoft.AspNetCore.ResponseCompression;
using ShopWebsite.Server.Utils.ServiceRegistration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7160") // порт клиента
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddShopWebsiteServices();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

app.UseBlazorFrameworkFiles();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.UseCors("AllowBlazorClient");
app.MapFallbackToFile("index.html");

app.Run();
