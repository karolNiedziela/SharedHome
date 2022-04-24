using FluentValidation.AspNetCore;
using SharedHome.Application;
using SharedHome.Infrastructure;
using SharedHome.Shared;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddShared(builder.Configuration);

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseShared();

app.UseAuthorization();

app.MapControllers();

app.Run();
