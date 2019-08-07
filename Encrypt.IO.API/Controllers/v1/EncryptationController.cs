using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
                Log.Information("");
                Log.Information("Starting decryptation...");
                Log.Information($"Decrypt message: {model.Message}");
                var keyArr = Encoding.UTF8.GetBytes(model.CryptoKey);
                var encrypted = Convert.FromBase64String(model.Message);
                var json = "";

                using(var aesAlg = Aes.Create()) 
                {
                    aesAlg.Key = keyArr;
                    aesAlg.IV = new byte[16];

                    Log.Information("Creating AES decryptor...");
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    Log.Information("Decrypting...");
                    using(var msDecrypt = new MemoryStream(encrypted)) 
                    using(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) 
                    using(var srDecrypt = new StreamReader(csDecrypt)) 
                        json = srDecrypt.ReadToEnd();
                }

                Log.Information($"Decrypted: {json}");
                return Ok(MessageModel.Factor(json));
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
                Log.Information("");
                Log.Information("Starting encryptation...");
                Log.Information($"Encrypt message: {model.Message}");
                var keyArr = Encoding.UTF8.GetBytes(model.CryptoKey);
                byte[] encrypted;
                var jsonCrip = "";

                using(var aesAlg = Aes.Create())  
                {
                    aesAlg.Key = keyArr;
                    aesAlg.IV = new byte[16];

                    Log.Information("Creating AES encryptor...");
                    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    Log.Information("Encrypting...");
                    using(var msEncrypt = new MemoryStream()) 
                    using(var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) 
                    {
                        using(var swEncrypt = new StreamWriter(csEncrypt)) 
                            swEncrypt.Write(model.Message);
                        encrypted = msEncrypt.ToArray();
                    }
                }

                jsonCrip = Convert.ToBase64String(encrypted);
                Log.Information($"Encrypted: {jsonCrip}");
                return Ok(MessageModel.Factor(jsonCrip));
            }
            catch (Exception ex)
            {
                Log.Error($"Fail on encrypt. Details: {ex}");
            }

            return new StatusCodeResult(500);
        }
    }
}