using AutoMapper;
using microscore.application.interfaces.repositories;
using microscore.application.interfaces.services;
using microscore.application.models.dtos.accounts;
using microscore.application.models.exeptions;
using microscore.domain.entities.Accounts;
using microscore.domain.Enums;
using Serilog;

namespace microscore.application.services
{
    public class MovementsServices : IMovementsServices
    {
        private readonly IMovementRepository _IMovementRepository;
        private readonly IAccountRepository _IAccountRepository;
        private readonly IMapper _Imapper;

        public MovementsServices(IMovementRepository iMovementRepository,
                                 IMapper imapper,
                                 IAccountRepository iAccountRepository)
        {
            _IMovementRepository = iMovementRepository;
            _Imapper = imapper;
            _IAccountRepository = iAccountRepository;
        }

        public async Task<List<MovementReportDTO>> CreateReportbyUserAndDate(DateTime Finit, string ClientName)
        {
            try
            {
                var result = await _IMovementRepository.GetAllAsync(Finit, ClientName);
                return _Imapper.Map<List<MovementReportDTO>>(result);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al momento de generar el reporte.");
                throw;
            }
        }

        public async Task<MovementDTO> DebitAndCreditProcess(MovementRequest request)
        {
            try
            {
                MovementDTO movementDTO = new(request);
                movementDTO.GetMovementValue();
                movementDTO.GetMovementType();
                movementDTO.GetAccountType();


                Account account = await _IAccountRepository.GetAccountbyNumberAsync(movementDTO.AccountNumber);
                if (account.Type != movementDTO.AccountType)
                {
                    throw new AccountDoesntMatchException();
                }

                movementDTO.ValueBalance = account.Balance;
                movementDTO.AccountId = account.Id;

                switch (movementDTO.MovementType)
                {
                    case MovementType.Deposit:
                        account.Balance += movementDTO.Value;
                        break;
                    case MovementType.Withdrawal:
                        if ((account.Balance - movementDTO.Value) < 0)
                        {
                            Log.Error($"No se puede cubrir el valor del debito: {movementDTO.Value}, cuenta: {request.AccountNumber}");
                            throw new InsufficientFundsException();
                        }
                        account.Balance -= movementDTO.Value;
                        break;
                    default:
                        break;
                }

                await _IAccountRepository.UpdateAsync(account);
                movementDTO.Id = (await _IMovementRepository.AddAsync(_Imapper.Map<Movement>(movementDTO))).Id;
                return movementDTO;
            }
            catch (ArgumentNullException Ex)
            {
                Log.Error(Ex, "El tipo de cuenta no se ha establecido.");
                throw;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Se produjo un error en la funcion de Debito Credito para la transaccion: {request.Movement}, para la cuenta: {request.AccountNumber}");
                throw;
            }
        }

    }
}
