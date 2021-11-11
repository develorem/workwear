using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkWearApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyValueController : ControllerBase
    {
        private readonly ILogger<KeyValueController> _logger;

        public KeyValueController(ILogger<KeyValueController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            throw new NotImplementedException($"Get key={key}");
        }

        [HttpPost]
        [Route("{key}")]
        public Task<IActionResult> Post(string key)
        {
            throw new NotImplementedException($"Post key={key}");
        }

        [HttpPut]
        [Route("{key}")]
        public Task<IActionResult> Put(string key)
        {
            throw new NotImplementedException($"Put key={key}");
        }
    }
}
