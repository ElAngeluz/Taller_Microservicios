using AutoMapper;
using microscore.application.interfaces.repositories;
using microscore.application.models.exeptions;
using microscore.domain.entities.consultaejemplo;
using microscore.infrastructure.data.context;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace microscore.infrastructure.data.repositories
{
    internal class EjemploRestRepository : IEjemploRestRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly MicrosContext _context;

        public EjemploRestRepository(IConfiguration Configuration, IMapper Mapper, MicrosContext Context)
        {
            _configuration = Configuration;
            _mapper = Mapper;
            _context = Context;
        }
        public async Task<DatosEjemplo> ConsulataDatosEjemplo(string canal)
        {
            Log.Information("{Proceso} {Descripcion} - {Canal}", "ConsultaContratosCanal IN", "Consulta lista contraros por canal", canal);
            DatosEjemplo response = null;

            if (canal != null)
            {

                response = new DatosEjemplo
                {
                    Datos = new DatosEjemploDto
                    {
                        Nombre = "ProjecTemplate",
                        Descripcion = "Plantilla de ejemplo en .NET"
                    }
                };
            }
            else
            {
                throw new EjemploException("Error de aplicativo", "El valor es vacio.", 204);
            }
            if (response == null)
            {
                throw new EjemploException("Error de aplicativo", "Contrato no encontrado.", 204);
                //Log.Information("{Proceso} {Descripcion} );
            }
            return response;
        }
    }
}
