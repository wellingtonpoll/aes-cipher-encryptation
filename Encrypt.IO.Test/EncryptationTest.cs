using Xunit;
using Encrypt.IO.API.Interfaces;
using Encrypt.IO.API.Services;
using Encrypt.IO.API.Models.v1;

namespace Encrypt.IO.Test
{
    [Collection("Encryptation")]
    public class EncryptationTest 
    {
        private MessageModel model;
        private readonly IEncryptationService encryptationService;
        public EncryptationTest()
        {
            this.encryptationService = new EncryptationService();
            this.model = MessageModel.Factor("message", "1234567890123456", "0000000000000000");
        }

        [Fact()]
        public void CryptoKey_Length_Equals_16_Characters()
        {
            
            Assert.True(model.CryptoIV.Length == 16);
        }

        [Fact]
        public void CryptoIV_Length_Equals_16_Characters()
        {
            Assert.True(model.CryptoIV.Length == 16);
        }

        [Fact]
        public void Message_NotNull_And_NotEmpty()
        {
            Assert.False(string.IsNullOrWhiteSpace(model.Message));
        }

        [Fact]
        public void Encrypted_Correct()
        {
            var encryptedModel = this.encryptationService.Encrypt(model);
            Assert.True(encryptedModel.Message == "de3ryCD58+yiZAR7YKR0nA==");
        }

        [Fact]
        public void Decrypted_Correct()
        {
            this.model = MessageModel.Factor("de3ryCD58+yiZAR7YKR0nA==", "1234567890123456", "0000000000000000");
            var decryptedModel = this.encryptationService.Decrypt(model);
            Assert.True(decryptedModel.Message == "message");
        }
    }
}