using Encrypt.IO.API.Models.v1;

namespace Encrypt.IO.API.Interfaces
{
    public interface IEncryptationService
    {
        string Encrypt(MessageModel model);
        string Decrypt(MessageModel model);
    }
}