using Encrypt.IO.API.Models.v1;

namespace Encrypt.IO.API.Interfaces
{
    public interface IEncryptationService
    {
        MessageModel Encrypt(MessageModel model);
        MessageModel Decrypt(MessageModel model);
    }
}