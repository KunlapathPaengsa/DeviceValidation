using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceValidation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceValidationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DeviceValidationController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeviceValidationController(ILogger<DeviceValidationController> logger
            , IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers;
            var deviceheaders = new HttpHeader();
            foreach (var item in header)
            {
                if (item.Key == EnumHeader.OS.ToString())
                {
                    deviceheaders.OS = item.Value;
                }
                if (item.Key == EnumHeader.Platform.ToString())
                {
                    deviceheaders.Platform = item.Value;
                }
                if (item.Key == EnumHeader.DeviceId.ToString())
                {
                    deviceheaders.DeviceId = item.Value;
                }
            }
            //if(!CommandRepo.CommonAny())
            var deviceJson = JsonSerializer.Serialize(deviceheaders);
            //else
            var dataDeviceJson = "{\"OS\":\"Andriod\",\"Platform\":\"Samsung Note 10s\",\"DeviceId\":\"DNPQKE8ZGRY8\"}";
            var dataDeviceList = JsonSerializer.Deserialize<HttpHeader>(deviceJson);

            //ClassValidation
            bool validation = deviceheaders == dataDeviceList;
            //SMS Verify







            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
