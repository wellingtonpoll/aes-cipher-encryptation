using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Encrypt.IO.API.Interfaces;
using Encrypt.IO.API.Models.v1;
using Serilog;

namespace Encrypt.IO.API.Services
{
    public class EncryptationService : IEncryptationService
    {
        public MessageModel Encrypt(MessageModel model)
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
            return MessageModel.Factor(jsonCrip);
        }

        public MessageModel Decrypt(MessageModel model)
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
            return MessageModel.Factor(json);
        }
    }
}