using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Events;
using SharedHome.Application;
using SharedHome.Infrastructure;
using SharedHome.Shared;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .AddUserSecrets<Program>()
    .Build();

    builder.Host.UseSerilog((context, serviceProvider) => serviceProvider
          .ReadFrom.Configuration(configuration));

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

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseShared();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}