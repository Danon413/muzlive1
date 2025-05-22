using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopWebsite.Client;
using ShopWebsite.Client.Services.CartService;
using ShopWebsite.Client.Services.ProductService;
using ShopWebsite.Client.Services.ProductTypeService;
using ShopWebsite.Client.Services.CategoryService;
using ShopWebsite.Client.Services.AuthService;
using ShopWebsite.Client.Services.AddressService;
using ShopWebsite.Client.Services.OrderService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();

await builder.Build().RunAsync();
