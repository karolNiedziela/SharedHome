using FluentValidation.AspNetCore;
using SharedHome.Application;
using SharedHome.Infrastructure;
using SharedHome.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(SharedHome.Application.AssemblyReference).Assembly));

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

app.MapControllers();

app.Run();
