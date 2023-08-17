using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using ThietKeWebBTL_User2.Models;
using ThietKeWebBTL_User2.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ShoeWebsiteLtwContext");
builder.Services.AddDbContext<ShoeWebsiteLtwContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IMenCategoryRepository, MenCategoryRepository>();
builder.Services.AddScoped<IWomenCategoryRepository, WomenCategoryRepository>();
builder.Services.AddSession();
builder.Services.AddAuthentication().AddGoogle(option =>
{
    option.ClientId = "289658705445-qjkdf4ss749gub1795c19gbrqmhn6ovr.apps.googleusercontent.com";
    option.ClientSecret = "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";
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
