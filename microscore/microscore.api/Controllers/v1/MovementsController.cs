using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.accounts;
using Microsoft.AspNetCore.Mvc;

namespace microscore.api.Controllers.v1
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovementsController : BaseApiController
    {

        private readonly IMovementsServices _IMovementsServices;

        public MovementsController(IMovementsServices iMovementsServices)
        {
            _IMovementsServices = iMovementsServices;
        }

        [HttpPost]
        public async Task<ActionResult<MsDtoResponse<MovementDTO>>> DebitCreditProcess(MovementRequest movementRequest)
        {
            var result = await _IMovementsServices.DebitAndCreditProcess(movementRequest);
            return Ok(new MsDtoResponse<MovementDTO>(result, HttpContext.TraceIdentifier));
        }

        [HttpGet("reporte")]
        public async Task<ActionResult<MsDtoResponse<List<MovementReportDTO>>>> GetReportMovements(DateTime FechaTrasaccion, string NombreCliente)
        {
            var result = await _IMovementsServices.CreateReportbyUserAndDate(FechaTrasaccion, NombreCliente);
            return Ok(new MsDtoResponse<List<MovementReportDTO>>(result, HttpContext.TraceIdentifier));
        }

    }
}
