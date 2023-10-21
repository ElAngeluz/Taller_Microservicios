using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.domain.entities.consultaejemplo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace microscore.api.Controllers.v1
{
    [ApiExplorerSettings(GroupName = "v1")]
    public class EjemploController : BaseApiController
    {
        private readonly IEjemploRepository _ejemploRepository;

        public EjemploController(IEjemploRepository _ejemploRepository)
        {
            this._ejemploRepository = _ejemploRepository;
        }
        /// <summary>
        /// Descripcion del metodo 
        /// </summary>  
        [HttpGet]
        [Route("dominio/version/nombreServicio")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<DatosEjemplo>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        public async Task<ActionResult<MsDtoResponse<DatosEjemplo>>> ConsultaPersonaContratosCanalPathAsync([FromQuery][Required] string valor)
        {


            DatosEjemplo _response = await _ejemploRepository.ConsulataDatosEjemplo(valor);
            return Ok(new MsDtoResponse<DatosEjemplo>(_response, HttpContext?.TraceIdentifier));
        }

    }
}
