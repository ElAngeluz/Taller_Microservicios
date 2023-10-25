using microscore.application.models.dtos.accounts;

namespace microscore.application.interfaces.services
{
    public interface IMovementsServices
    {
        Task<MovementDTO> DebitAndCreditProcess(MovementRequest request);
        Task<List<MovementReportDTO>> CreateReportbyUserAndDate(DateTime Finit, string ClientName);
    }
}
