using microscore.domain.Enums;
using Serilog;
using System.Text.Json.Serialization;

namespace microscore.application.models.dtos.accounts
{
    public class MovementDTO : MovementRequest
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public MovementType MovementType { get; set; }

        /// <summary>
        /// Valor de la transaccion
        /// </summary>
        [JsonIgnore]
        public decimal Value { get; set; }

        /// <summary>
        /// valor del balance antes del cambio
        /// </summary>
        [JsonIgnore]
        public decimal ValueBalance { get; set; }

        [JsonIgnore]
        public Guid AccountId { get; set; }

        public bool State { get; set; }

        [JsonIgnore]
        public AccountType AccountType { get; set; }

        public MovementDTO(MovementRequest request)
        {
            AccountNumber = request.AccountNumber;
            AccountTypeString = request.AccountTypeString;
            Movement = request.Movement;
        }

        public void GetAccountType()
        {
            try
            {
                AccountType = (AccountType)Enum.Parse(typeof(AccountType), AccountTypeString, ignoreCase: true);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al momento de poder estipular el tipo del cuenta.");
                throw;
            }
        }

        public void GetMovementType()
        {
            try
            {
                if (Movement.ToLower().StartsWith("dep"))
                {
                    MovementType = MovementType.Deposit;
                }
                else if (Movement.ToLower().StartsWith("ret"))
                {
                    MovementType = MovementType.Withdrawal;
                }
                else
                {
                    Log.Error($"el movimiento no se puede determinar si es deposito o credito: {Movement}");
                    throw new InvalidCastException("Debe estipular si es un deposito o un retiro.");
                }
            }
            catch (ArgumentNullException Ex)
            {
                Log.Error(Ex, "El movimiento de la transaccion esta vacio");
                throw;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al momento de poder estipular el tipo del movimiento.");
                throw;
            }
        }

        public void GetMovementValue()
        {
            try
            {
                Value = decimal.Parse(Movement.Reverse().TakeWhile(char.IsDigit).Reverse().ToArray());
            }
            catch (ArgumentNullException Ex)
            {
                Log.Error(Ex, "El movimiento de la transaccion esta vacio");
                throw;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, $"Se produjo un error al momento de obtener el valor del movimiento: {Movement}");
                throw;
            }
        }
    }
}
