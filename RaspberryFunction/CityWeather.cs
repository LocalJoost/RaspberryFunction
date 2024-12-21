using System.Text;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;

namespace RaspberryFunction
{
    public class CityWeather
    {
        private readonly IConfiguration configuration;

        public CityWeather(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [Function("CityWeather")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            var connectionString = configuration["AzureWebJobsStorage"];
            var cityWeatherTableClient = new TableClient(connectionString, "CityWeather");
            await cityWeatherTableClient.CreateIfNotExistsAsync();

            var city = req.Query["city"];
            var weather = req.Query["weather"];
            if(string.IsNullOrEmpty(city) || string.IsNullOrEmpty(weather))
            {
                StringBuilder response = new StringBuilder();
                await foreach (var cityWeather in cityWeatherTableClient.QueryAsync<CityWeatherItem>())
                {
                    response.AppendLine($"{cityWeather.RowKey}:{cityWeather.Weather}");
                }
                return new OkObjectResult(response.ToString());

            }
            var cityWeatherItem = new CityWeatherItem
            {
                PartitionKey = "CityWeather",
                RowKey = city,
                Weather = weather
            };
            await cityWeatherTableClient.UpsertEntityAsync(cityWeatherItem);

            return new OkObjectResult($"Weather {weather} for {city} stored");
        }
    }
}
