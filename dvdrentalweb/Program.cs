using dvdrentalweb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShopContextConnection"); 

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ShopContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppConnection"),
        b => b.MigrationsAssembly(typeof(ShopContext).Assembly.FullName));
});

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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
