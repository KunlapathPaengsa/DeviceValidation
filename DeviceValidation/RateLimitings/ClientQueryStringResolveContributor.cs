using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceValidation.RateLimitings
{
    public class ClientQueryStringResolveContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var deviceHeaderValue = string.Empty;
            
            if(httpContext.Request.Headers.TryGetValue("DeviceId", out var values))
            {
                deviceHeaderValue = values.First();
            }

            return Task.FromResult(deviceHeaderValue);
        }
    }
}
