using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cloudnative_weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public IConfiguration _configuration { get; }
        public Settings _options { get;}
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IOptions<Settings> options, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<WeatherResponse[]> Get()
        {
            var uri = $"{_options.uri}/{_options.location}?apikey={_options.accuweathertoken}";
            var client = _httpClientFactory.CreateClient("Weather");
            var results = await client.GetStringAsync(uri);

            return JsonSerializer.Deserialize<WeatherResponse[]>(results);
        }
    }
}
