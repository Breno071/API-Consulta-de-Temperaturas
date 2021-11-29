using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Timers;


namespace TesteTecnico_.NET
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      await Seedinicial(host);
      var timer = new Timer();
      timer.Interval = TimeSpan.FromMinutes(15).TotalMilliseconds;
      timer.Elapsed += async (sender, e) =>
      {

        using (var scope = host.Services.CreateScope())
        {
          var services = scope.ServiceProvider;
          try
          {
            await SeedData.SeedData.Inicialize(services);
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Dados semeados");
          }
          catch (Exception ex)
          {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocorreu um erro ao tentar semear a base de dados");
          }
        }
      };

      timer.AutoReset = true;
      timer.Enabled = true;
      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });

    private static async Task Seedinicial(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          await SeedData.SeedData.Inicialize(services);
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogInformation("Dados semeados");
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "Ocorreu um erro ao tentar semear a base de dados");
        }
      }
    }
  }
}
