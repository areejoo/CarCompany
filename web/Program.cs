using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using web.api.Dtos.Incomming;
using web.core;
using web.core.Entities;
using web.core.Interfaces;
using web.infrastructure.Data;
using web.infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer(

builder.Configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("web.infrastructure")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped< IValidator<CreateCarDto>, CreateDtoValidator >();
builder.Services.AddScoped< IValidator<UpdateCarDto>, UpdateDtoValidator >();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

 //builder.Services.AddScoped<typeof(IGenericRepository<>), typeof(GenericReository<>)>();
//builder.Services.AddScoped(typeof(IGenericRepository < > ), typeof(GenericReository< > ));  


builder.Services.AddMemoryCache();
var app = builder.Build();
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

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