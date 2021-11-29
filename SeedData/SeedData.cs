using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TesteTecnico_.NET.Data;
using TesteTecnico_.NET.Models;

namespace TesteTecnico_.NET.SeedData
{
  public static class SeedData
  {
    static HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://api.openweathermap.org") };
    public static async Task Inicialize(IServiceProvider serviceProvider)
    {
      using (var context = new ApplicationDbContext(
          serviceProvider.GetRequiredService<
              DbContextOptions<ApplicationDbContext>>()))
      {
        var curitiba = await GetCuritibaAsync();
        var porto_alegre = await GetPortoAlegreAsync();
        var florianopolis = await GetFlorianopolisAsync();

        if (context.Temps.AnyAsync() == Task.FromResult(true))
        {
          context.RemoveRange(context.Temps);
          context.SaveChanges();
        }

        await context.AddRangeAsync(
            new Main
            {
              temp = curitiba.main.temp,
              name = "curitiba",
              feels_like = curitiba.main.feels_like,
              temp_min = curitiba.main.temp_min,
              temp_max = curitiba.main.temp_max,
              pressure = curitiba.main.pressure,
              humidity = curitiba.main.humidity,
              date = DateTime.Now.Date,
              hour = DateTime.Now.Hour.ToString()
            },
            new Main
            {
              temp = porto_alegre.main.temp,
              name = "porto alegre",
              feels_like = porto_alegre.main.feels_like,
              temp_min = porto_alegre.main.temp_min,
              temp_max = porto_alegre.main.temp_max,
              pressure = porto_alegre.main.pressure,
              humidity = porto_alegre.main.humidity,
              date = DateTime.Now.Date,
              hour = DateTime.Now.Hour.ToString()
            },
            new Main
            {
              temp = florianopolis.main.temp,
              name = "florianópolis",
              feels_like = florianopolis.main.feels_like,
              temp_min = florianopolis.main.temp_min,
              temp_max = florianopolis.main.temp_max,
              pressure = florianopolis.main.pressure,
              humidity = florianopolis.main.humidity,
              date = DateTime.Now.Date,
              hour = DateTime.Now.Hour.ToString()
            }
        ); ;

        await context.SaveChangesAsync();
      }
    }

    private static async Task<Consulta> GetCuritibaAsync()
    {
      var response = await httpClient.GetAsync("/data/2.5/weather?id=3464975&appid=cdc8a1582641b6f11e7a2a487e1e0d93");
      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Consulta>(content);
    }

    private static async Task<Consulta> GetPortoAlegreAsync()
    {
      var response = await httpClient.GetAsync("/data/2.5/weather?id=3452925&appid=cdc8a1582641b6f11e7a2a487e1e0d93");
      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Consulta>(content);
    }

    private static async Task<Consulta> GetFlorianopolisAsync()
    {
      var response = await httpClient.GetAsync("/data/2.5/weather?id=3463237&appid=cdc8a1582641b6f11e7a2a487e1e0d93");
      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Consulta>(content);
    }


  }
}
