using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WorkWearApi.Services;

namespace WorkWearApi.Controllers
{
    [ApiController]
    [Produces("text/plain")]
    [Consumes("text/plain")]
    [Route("[controller]")]
    public class KeyValueController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IValidationService _validationService;

        public KeyValueController(IRepository repository, IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        /// <summary>
        /// Returns a value for the given key
        /// </summary>        
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{key}")]
        public IActionResult Get(string key)
        {
            if (_validationService.IsValidKey(key) == false)
                return BadRequest("Key not in valid format");

            if (_repository.Exists(key) == false)
                return NotFound($"Resource not found for key: {key}");

            try
            {
                var result = _repository.Get(key);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Resource not found for key: {key}");
            }
        }


        /// <summary>
        /// Adds an entry with the given key and value
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("{key}")]
        public IActionResult Post(string key, [FromBody] string value)
        {
            if (_validationService.IsValidKey(key) == false)
                return BadRequest("Key is not in a valid format");

            if (_validationService.isValidValue(value) == false)
                return BadRequest("Value is not in a valid format");

            try
            {
                _repository.Add(key, value);

                return Created($"/KeyValue/{key}", value);
            }
            catch (ArgumentException)
            {
                return BadRequest("Key already exists, did not add value");
            }
        }

        /// <summary>
        /// updates an entry at the given key with the supplied value
        /// </summary>
        /// <response code="400">Bad Request</response>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("{key}")]
        public IActionResult Put(string key, [FromBody] string value)
        {
            if (_validationService.IsValidKey(key) == false)
                return BadRequest("Key is not in a valid format");

            if (_validationService.isValidValue(value) == false)
                return BadRequest("Value is not in a valid format");

            try
            {
                _repository.Update(key, value);
                return Accepted();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Key not found");
            }
        }
    }
}
