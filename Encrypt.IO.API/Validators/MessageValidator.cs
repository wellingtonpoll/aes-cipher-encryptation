using Encrypt.IO.API.Models.v1;
using FluentValidation;

namespace Encrypt.IO.API.Validators
{
    public class MessageValidator : AbstractValidator<MessageModel>
    {
        public MessageValidator()
        {
            RuleFor(c => c.CryptoKey)
                .NotNull()
                .NotEmpty()
                .Length(16, 16);

            RuleFor(c => c.CryptoIV)
                .NotNull()
                .NotEmpty()
                .Length(16, 16);

            RuleFor(c => c.Message)
                .NotNull()
                .NotEmpty();
        }
    }
}