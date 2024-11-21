using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace GoldenRaspberryAwards.Api.Helpers
{
    public class YesNoBooleanConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            // Remover espaços em branco antes e depois
            text = text?.Trim();

            // Log detalhado para verificar o valor a ser convertido
            Console.WriteLine($"Converting value: '{text}'");

            if (string.IsNullOrEmpty(text)) // Se estiver vazio ou nulo, considerar como 'false'
            {
                return false;
            }

            // Convertendo para minúsculas para tratar de forma insensível ao caso
            string lowerText = text.ToLowerInvariant();

            // Comparação insensível ao caso
            if (lowerText == "yes")
            {
                return true;
            }
            else if (lowerText == "no")
            {
                return false;
            }

            // Se o valor não for 'yes' nem 'no', lançar exceção
            throw new TypeConverterException(this, memberMapData, text, row.Context);
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return (bool)value ? "yes" : "no";
        }
    }
}
