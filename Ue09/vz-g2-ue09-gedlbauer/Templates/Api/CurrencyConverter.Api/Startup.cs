using CurrencyConverter.Api.Filters;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyConverter.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
              .AddNewtonsoftJson() // default formatter
              .AddXmlDataContractSerializerFormatters();
      
      // AddNewtonsoftJson: By default System.Text.Json is used in .NET core 3.0.
      // Although System.Text.Json is said to be faster than NewtonsoftJson, 
      // NewtonsoftJson has still a more comprehensive functionality.
      // E.g. by now it is not possible to require a property to included in a
      // JSON object.

      services.AddOpenApiDocument(settings =>
        settings.PostProcess = doc => doc.Info.Title = "Currency Converter API");

      services.AddCors(builder =>
        builder.AddDefaultPolicy(policy =>
          policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

      services.AddSingleton<ICurrencyCalculator, CurrencyCalculatorImpl>();
      services.AddScoped<LoggingFilter>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseDefaultFiles();
      app.UseStaticFiles();

      // app.UseOpenApi(); // Open API document is generated at build time.
                           // Therefore, it need not be generated at runtime.
      app.UseSwaggerUi3(settings => settings.DocumentPath = "api/v1/openapi.json");
                           // Specify location of open API document
                           // (relative to wwwroot).

      app.UseCors();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
