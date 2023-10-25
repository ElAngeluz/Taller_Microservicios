using Microsoft.AspNetCore.Http;

namespace microscore.application.models.exeptions
{
    public class AccountDoesntMatchException : BaseCustomException
    {
        public AccountDoesntMatchException(string stackTrace = "",
                                  string message = "El tipo de cuenta que se intenta modificar no es el apropiado.",
                                  int code = StatusCodes.Status400BadRequest) : base(message, stackTrace, code)
        {
        }
    }
}
