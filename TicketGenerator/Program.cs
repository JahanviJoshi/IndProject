using Microsoft.EntityFrameworkCore;
using TicketGenerator.Models;
using TicketGenerator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TickectCreatingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppconnStr"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IService<Login, int>, UserService>();
builder.Services.AddScoped<IService<Tickect, int>, TickectGenerate>();

// COfigure Sessions
// The Session Time out is 20 Mins for Idle Request
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=GetUser}/{id?}");

app.Run();
