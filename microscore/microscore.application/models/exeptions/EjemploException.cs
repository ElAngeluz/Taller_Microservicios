namespace microscore.application.models.exeptions
{
    public class EjemploException : BaseCustomException
    {
        public EjemploException(string message = "Contrato Contacto Exception", string description = "", int statuscode = 500) : base(message, description, statuscode)
        {

        }
    }
}
