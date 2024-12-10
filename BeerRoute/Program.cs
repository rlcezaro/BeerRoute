using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeerRoute.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar suporte para appsettings.Local.json
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Adicionar suporte para variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("BeerRouteContext");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("ConnectionStringsAzure:BeerRouteContext");
}

builder.Services.AddDbContext<BeerRouteContext>(options =>
    options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'BeerRouteContext' not found."),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// Adicionar o Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BeerRouteContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adicionar autenticação
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Mapear Razor Pages

app.Run();