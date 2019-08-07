using System;
using Encrypt.IO.API.Interfaces;
using Encrypt.IO.API.Models.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Encrypt.IO.API.Controllers.v1
{
    /// <summary>
    /// AES cipher encryptation controller
    /// By default CBC mode with Pkcs7 padding and 128-bit encryption key.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EncryptationController : ControllerBase
    {
        private readonly IEncryptationService encryptationService;
        public EncryptationController(IEncryptationService encryptationService)
        {
            this.encryptationService = encryptationService;
        }
        
        /// <summary>
        /// AES cipher decryptation algorithms.
        /// </summary>
        /// <param name="model">Message model to decrypt.</param>
        /// <response code="200">Decrypted message.</response>
        /// <response code="400">Any problem with your request data.</response>
        /// <response code="500">There was an internal problem, please try again..</response>
        [HttpPost("decrypt")]
        [Produces("application/json")]
        [Consumes("application/json" )]
        [ProducesResponseType(typeof(MessageModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Decrypt([FromBody] MessageModel model)
        {
            try
            {
                var json = this.encryptationService.Decrypt(model);
                return Ok(json);
            }
            catch (Exception ex)
            {                
               Log.Error($"Fail on decrypt. Details: {ex}");
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// AES cipher encryptation algorithms.
        /// </summary>
        /// <param name="model">Message model to encrypt.</param>
        /// <response code="200">Encrypted message.</response>
        /// <response code="400">Any problem with your request data.</response>
        /// <response code="500">There was an internal problem, please try again..</response>
        [HttpPost("encrypt")]
        [Produces("application/json")]
        [Consumes("application/json" )]
        [ProducesResponseType(typeof(MessageModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Encrypt([FromBody] MessageModel model)
        {
            try
            {
                var jsonCrip = this.encryptationService.Encrypt(model);
                return Ok(jsonCrip);
            }
            catch (Exception ex)
            {
                Log.Error($"Fail on encrypt. Details: {ex}");
            }

            return new StatusCodeResult(500);
        }
    }
}