using microscore.domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace microscore.application.utils
{
    public class AccountTypeConverter : JsonConverter<AccountType>
    {
        public override AccountType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (Enum.TryParse(value, out AccountType accountType))
                {
                    return accountType;
                }
            }

            return AccountType.Ahorro; // Valor predeterminado o manejo de errores
        }

        public override void Write(Utf8JsonWriter writer, AccountType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
