using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeManagementApp.AutoMapperProfiles;
using RecipeManagementApp.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<RecipeManagementContext>();

builder.Services.AddAutoMapper(typeof(UserProfiles));

string connectionString = builder.Configuration.GetConnectionString("RecipeManagement");
builder.Services.AddDbContext<RecipeManagementContext>(options => options.UseSqlServer(connectionString));

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
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
