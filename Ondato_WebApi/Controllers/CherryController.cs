using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ondato_WebApi.Attributes;
using Ondato_WebApi.Exceptions;
using Ondato_WebApi.Logic.Interfaces;
using Ondato_WebApi.Models.Dto;

namespace Ondato_WebApi.Controllers
{
    [ApiController]
    [TypeFilter(typeof(AuthorizationAttribute))]
    [Route("api/[controller]")]
    public class CherryController : ControllerBase
    {
        private readonly ICherryLogic _cherryLogic;
        private readonly ILogger<CherryController> _logger;

        public CherryController(ILogger<CherryController> logger, ICherryLogic cherryLogic)
        {
            _logger = logger;
            _cherryLogic = cherryLogic;
        }

        [HttpGet("get/{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string key)
        {
            CherryDto result;
            try
            {
                result = _cherryLogic.Get(key);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateUpdateRequestDto createUpdateRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string response;
            try
            {
                response = _cherryLogic.CreateUpdate(createUpdateRequestDto);
            }
            catch (ExpirationDateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

        [HttpPut("append")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Append([FromBody] CreateUpdateRequestDto createUpdateRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string response;
            try
            {
                response = _cherryLogic.CreateUpdate(createUpdateRequestDto);
            }
            catch (ExpirationDateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                _cherryLogic.Delete(key);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
