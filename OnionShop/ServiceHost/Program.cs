using _0_Framework.Application;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.configuration;
using InventoryManagement.Configuration;
using ServiceHost;
using ShopManagement.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnectionString = builder.Configuration.GetConnectionString("OnionShopDb");
builder.Services.AddRazorPages();
ShopManagementBootstrapper.Configuration(builder.Services, ConnectionString);
DiscountCustomerBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configuration(builder.Services, ConnectionString);
BlogManagementBootstrapper.Configure(builder.Services, ConnectionString);
CommentManagementBootstrapper.Configuration(builder.Services, ConnectionString);
AccountBootstrapper.Configure(builder.Services , ConnectionString);
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.AddTransient<IFileUploader, FileUploader>();
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
