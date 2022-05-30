using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Serilog;
using SharedHome.Api;
using SharedHome.Application;
using SharedHome.Infrastructure;
using SharedHome.Shared;
using System.Globalization;
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
        .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly))
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                return factory.Create("DataAnnotationMessage", "SharedHome.Api"); ;
            };
        });

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new List<CultureInfo>()
        {
            new CultureInfo("en-US"),
            new CultureInfo("pl-PL")
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "Origin",
                          policy =>
                          {
                              policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200");
                          });
    });

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