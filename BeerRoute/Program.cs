using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BeerRoute.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar suporte para appsettings.Local.json
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);


builder.Services.AddDbContext<BeerRouteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeerRouteContext") ?? throw new InvalidOperationException("Connection string 'BeerRouteContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
