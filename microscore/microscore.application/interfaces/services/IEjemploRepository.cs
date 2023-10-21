using microscore.domain.entities.consultaejemplo;

namespace microscore.application.interfaces.services
{
    public interface IEjemploRepository
    {
        Task<DatosEjemplo> ConsulataDatosEjemplo(string canal);
    }
}
