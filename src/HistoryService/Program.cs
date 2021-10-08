using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace LT.DigitalOffice.HistoryService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
  #if DEBUG
        .AddJsonFile("appsettings.Development.json")
  #else
        .AddJsonFile("appsettings.Production.json")
  #endif
        .Build();

      string seqServerUrl = Environment.GetEnvironmentVariable("seqServerUrl");
      if (string.IsNullOrEmpty(seqServerUrl))
      {
        seqServerUrl = configuration["Serilog:WriteTo:1:Args:serverUrl"];
      }

      string seqApiKey = Environment.GetEnvironmentVariable("seqApiKey");
      if (string.IsNullOrEmpty(seqApiKey))
      {
        seqApiKey = configuration["Serilog:WriteTo:1:Args:apiKey"];
      }

      Log.Logger = new LoggerConfiguration().ReadFrom
        .Configuration(configuration)
        .Enrich.WithProperty("Service", "HistoryService")
        .WriteTo.Seq(
          serverUrl: seqServerUrl,
          apiKey: seqApiKey)
        .CreateLogger();

      try
      {
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception exc)
      {
        Log.Fatal(exc, "Can not properly start HistoryService.");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
  }
}
