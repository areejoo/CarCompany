using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using web.core;
using web.core.Entities;
using web.core.Interfaces;
using web.infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("web.infrastructure.Data"))

    );
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// builder.Services.AddScoped<typeof(IGenericRepository<>), typeof(GenericReository<>)>();
    // builder.Services.AddScoped(typeof(IGenericRepository < > ), typeof(GenericReository< > ));  


builder.Services.AddMemoryCache();
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
