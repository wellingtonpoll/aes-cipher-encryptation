using System.ComponentModel.DataAnnotations;

namespace Encrypt.IO.API.Models.v1
{
    /// <summary>
    /// Model containing message to be encrypted or decrypted with encryption key 
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Message to be encrypted.
        /// </summary>
        /// <value>String</value>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Exactly 16 character encryption key.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string CryptoKey { get; set; }

        /// <summary>
        /// Exactly 16 character encryption random key.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string CryptoIV { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message model</returns>
        public static MessageModel Factor(string message)
            => new MessageModel { Message = message, CryptoKey = null };

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="cryptoKey">Crypto key</param>
        /// <param name="cryptoIV">Crypto IV</param>
        /// <returns>Message model</returns>
        public static MessageModel Factor(string message, string cryptoKey, string cryptoIV) 
            => new MessageModel { Message = message, CryptoKey = cryptoKey, CryptoIV = cryptoIV};
    }
}