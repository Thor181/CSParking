using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Controllers
{
    [ApiController]
    [Route("/common/[action]")]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string GetServerTime()
        {
            var format = _configuration.GetRequiredSection("TimeFormat").Value;

            return DateTime.Now.ToString(format);
        }
    }
}
