using System.Windows.Controls;
using System.Globalization;

namespace WpfDataBinding
{
    public class PrijmeniValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Vstup nemůže být prázdný");
            }

            if ((value as string)?.Length < 3)
            {
                return new ValidationResult(false, "Příjmení musí být delší než 3 znaky.");
            }

            return ValidationResult.ValidResult;
        }
    }


}
