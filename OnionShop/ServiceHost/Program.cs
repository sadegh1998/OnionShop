using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Application.Zipal;
using _0_Framework.Infrstructure;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.configuration;
using InventoryManagement.Configuration;
using InventoryManagement.Presentation.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ServiceHost;
using ShopManagement.Configuration;
using ShopManagement.Presentation.Api;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnectionString = builder.Configuration.GetConnectionString("OnionShopDb");
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your desired timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>(); 

builder.Services.AddHttpContextAccessor();
ShopManagementBootstrapper.Configuration(builder.Services, ConnectionString);
DiscountCustomerBootstrapper.Configure(builder.Services, ConnectionString);
InventoryManagementBootstrapper.Configuration(builder.Services, ConnectionString);
BlogManagementBootstrapper.Configure(builder.Services, ConnectionString);
CommentManagementBootstrapper.Configuration(builder.Services, ConnectionString);
AccountBootstrapper.Configure(builder.Services , ConnectionString);
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddSingleton<HmacTokenHelper>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<IZipalFactory, ZipalFactory>();

builder.Services.AddTransient<ISmsService,SmsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<CookieTempDataProviderOptions>(options => {
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<CookiePolicyOptions>(options => { options.CheckConsentNeeded = context => true; options.MinimumSameSitePolicy = SameSiteMode.Lax;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    o => {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
        }

    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", builder => builder.RequireRole(Roles.Administrator,Roles.InventoryUser));
    options.AddPolicy("Shop", builder => builder.RequireRole(Roles.Administrator, Roles.InventoryUser));
    options.AddPolicy("Inventory", builder => builder.RequireRole(Roles.Administrator));
    options.AddPolicy("Account", builder => builder.RequireRole(Roles.Administrator));
    options.AddPolicy("Comment", builder => builder.RequireRole(Roles.Administrator));



});

builder.Services
    .AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administrator", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/Account", "Account");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/Inventory", "Inventory");
        options.Conventions.AuthorizeAreaFolder("Administrator", "/Comments", "Comment");
    })
   .AddApplicationPart(typeof(ProductController).Assembly)
   .AddApplicationPart(typeof(InventoryController).Assembly)
   .AddNewtonsoftJson();
   
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
//app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
