using DiscountManagement.configuration;
using InventoryManagement.Configuration;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnectionString = builder.Configuration.GetConnectionString("OnionShopDb");
builder.Services.AddRazorPages();
ShopManagementBootstrapper.Configuration(builder.Services, ConnectionString);
DiscountCustomerBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configuration(builder.Services, ConnectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
