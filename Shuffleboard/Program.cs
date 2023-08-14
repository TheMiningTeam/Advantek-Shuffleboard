using Microsoft.EntityFrameworkCore;
using Shuffleboard.Data;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://[::]:443/;http://[::]:80/;https://0.0.0.0:443/;http://0.0.0.0:80/;");

// Add services to the container.
builder.Services.AddControllersWithViews();
// Creating the connection between website and database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Adding browser coockies fuunctionality
builder.Services.AddHttpContextAccessor();

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
