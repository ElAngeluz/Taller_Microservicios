using AutoMapper;
using microscore.application.interfaces.repositories;
using microscore.application.interfaces.services;
using microscore.domain.entities.consultaejemplo;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace microscore.application.services
{
    public class EjemploRepository : IEjemploRepository
    {
        private readonly IEjemploRestRepository _ejemploRestRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public EjemploRepository(IConfiguration Configuration, IEjemploRestRepository ejemploRestRepository, IMapper mapper)
        {
            _ejemploRestRepository = ejemploRestRepository;
            _mapper = mapper;
            _configuration = Configuration;
        }

        public Task<DatosEjemplo> ConsulataDatosEjemplo(string canal)
        {
            return _ejemploRestRepository.ConsulataDatosEjemplo(canal);
        }
    }
}
