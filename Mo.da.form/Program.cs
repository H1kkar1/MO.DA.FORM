using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MO.DA.FORM.Models;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using MO.DA.FORM.infrastructure;
using Humanizer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Users/Login");

builder.Services.AddTransient<IAuthorizationHandler, LeaderHandler>();

builder.Services.AddAuthorization(opts => {
    // устанавливаем ограничение по возрасту
    opts.AddPolicy("Limit",
        policy => policy.AddRequirements(new LeaderRequirement("True")));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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


builder.Services.AddCors();
app.Run();
