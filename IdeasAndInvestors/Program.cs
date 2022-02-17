using IdeasAndInvestors.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IdeasAndInvestorsDbContext>(options
    => options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdeasAndInvestorsDBConnection")));

builder.Services.AddSession(

    options => {
        options.IdleTimeout = TimeSpan.FromMinutes(1);
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Home}/{id?}");

app.Run();
