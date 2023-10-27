using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MO.DA.FORM.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres")));

// Add services to the container.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


builder.Services.AddCors();
app.Run();
