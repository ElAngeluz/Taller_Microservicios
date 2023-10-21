using microscore.domain.entities.consultaejemplo;

namespace microscore.application.interfaces.repositories
{
    public interface IEjemploRestRepository
    {
        Task<DatosEjemplo> ConsulataDatosEjemplo(string canal);
    }
}
