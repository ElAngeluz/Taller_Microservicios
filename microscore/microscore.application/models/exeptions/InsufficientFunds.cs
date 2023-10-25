using Microsoft.AspNetCore.Http;

namespace microscore.application.models.exeptions
{
    public class InsufficientFundsException : BaseCustomException
    {
        public InsufficientFundsException(string message = "Saldo no disponible.", string stackTrace = "", int code = StatusCodes.Status400BadRequest) : base(message, stackTrace, code)
        {
        }
    }
}
