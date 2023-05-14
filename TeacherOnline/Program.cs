using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.BLL.Services;
using TeacherOnline.DAL;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DataBase");
builder.Services.AddDbContext<AssistantTeachingContext>(option => option.UseSqlServer(connection));

builder.Services.AddTransient<IAuth, AuthService>();
builder.Services.AddTransient<IEstimate, EstimateService>();
builder.Services.AddTransient<IFile, FileService>();
builder.Services.AddTransient<IGroup, GroupService>();
builder.Services.AddTransient<IProfile, ProfileService>();
builder.Services.AddTransient<ISubject, SubjectService>();
builder.Services.AddTransient<IUser, UserService>();

// Add services to the container.
builder.Services.AddAuthentication("Cookies")
    .AddCookie(adress =>
    {
        adress.LoginPath = "/Users/Autorization";
        adress.AccessDeniedPath = "/Users/AccessDenied";
    });
builder.Services.AddAuthorization();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync("Cookies");
    if (context.Request.Cookies.ContainsKey("Id"))
    {
        context.Response.Cookies.Delete("Id");
    }
    return Results.Redirect("/");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();