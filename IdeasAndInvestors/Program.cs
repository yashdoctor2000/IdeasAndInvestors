using IdeasAndInvestors.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Add a global anti-caching filter for all controllers
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ResponseCacheAttribute
    {
        NoStore = true,
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None,
    });
});
builder.Services.AddDbContext<IdeasAndInvestorsDbContext>(options
    => options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdeasAndInvestorsDBConnection")));

builder.Services.AddSession(

    options => {
        options.IdleTimeout = TimeSpan.FromMinutes(1);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware to prevent caching of responses globally
app.Use(async (context, next) =>
{
    // Add headers to prevent caching
    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Home}/{id?}");

app.Run();
