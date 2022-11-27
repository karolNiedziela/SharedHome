using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SharedHome.Api;
using SharedHome.Api.Constants;
using SharedHome.Api.HealthChecks;
using SharedHome.Application;
using SharedHome.Identity;
using SharedHome.Infrastructure;
using SharedHome.Notifications;
using SharedHome.Shared;
using System.Text.Json;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

    var seqUri = configuration.GetValue<string>("Seq:Uri");

    builder.Host.UseSerilog((context, serviceProvider) => serviceProvider
          .ReadFrom.Configuration(configuration));

    builder.Services.AddShared();
    builder.Services.AddSharedHomeIdentity();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler(ApiRoutes.Errors.ErrorDevelopment);
    }
    else
    {
        app.UseExceptionHandler(ApiRoutes.Errors.Error);
    }

    var localizationOption = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(localizationOption!.Value);

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseCors("Origin");

    app.UseShared();

    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";

            var response = new HealthCheckResponse
            {
                Status = report.Status.ToString(),
                HealthChecks = report.Entries.Select(x => new HealthCheck
                {
                    Component = x.Key,
                    Status = x.Value.Status.ToString(),
                    Description = x.Value.Description
                }),
                Duration = report.TotalDuration
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    });

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseNotifications();

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

#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces