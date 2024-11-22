using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using NorthwindWithDapper;
using NorthwindWithDapper.Controllers;
using NorthwindWithDapper.ExceptionHandler;
using NorthwindWithDapper.Models.Mapper;
using NorthwindWithDapper.Repositories;
using NorthwindWithDapper.Services.Implement;
using NorthwindWithDapper.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddScoped<CustomerRepository>();

// AutoMapper register
builder.Services.AddAutoMapper(typeof(EntityMappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;
        ApiResponse response;

        if (exception is CustomerNotFoundException)
        {
            response = new ApiResponse(
                StatusCodes.Status400BadRequest,
                CustomerNotFoundException.ErrorMessage
            );
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            response = new ApiResponse(
                StatusCodes.Status500InternalServerError,
                CustomerNotFoundException.ErrorMessage
            );
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    });
});


app.Run();