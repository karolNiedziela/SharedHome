using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SharedHome.Api;
using SharedHome.Api.Constants;
using SharedHome.Application;
using SharedHome.Identity;
using SharedHome.Infrastructure;
using SharedHome.Notifications;
using SharedHome.Shared;

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

    builder.Services.AddSharedHomeIdentity(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddShared(builder.Configuration);
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