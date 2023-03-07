global using Microsoft.EntityFrameworkCore;
global using OnlineShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data;
using OnlineShop.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//scoped services ..
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Authent and autho
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Seed DB
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
