global using WebLucky_Serwer.Service;
global using WebLucky_Serwer.Service.IService;
using MudBlazor;
using MudBlazor.Services;
using WebLucky_Serwer.Data;
using Tangy_DataAccess_1.Data;
using Tangy_Business.Repository;
using Tangy_Business.Repository.IRepository;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Identity;
using Stripe;
//@attribute[Authorize]

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzIwNzMxMUAzMjM1MmUzMDJlMzBYeXN6Y2dwZHdjektVMTVsQmtvSWplbmJMWVdzc1FkWXpETGFaUzVScHNRPQ==");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["ApiKey"];

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

SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}