namespace microscore.application.models.exeptions
{
    public class CustomUnauthorizedExeption : BaseCustomException
    {
        public CustomUnauthorizedExeption(string message = "Unauthorized", string description = "", int statuscode = 401) : base(message, description, statuscode)
        {

        }
    }
}
