using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MKProxyGatewayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WSConfigController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public WSConfigController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var url = "";
            if (MonitoringHealth.MainServerHealth)
            {
                url = configuration.GetSection("ProxyServer:MainServer").Value;
            }
            else if(MonitoringHealth.BakeServerHealth)
            {
                url = configuration.GetSection("ProxyServer:BakeServer").Value;
            }

            return Ok(new
            {
                res = url
            });
        }

    }
}