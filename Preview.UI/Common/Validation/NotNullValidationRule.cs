using System.Globalization;
using System.Windows.Controls;

namespace Xylia.Preview.UI.Common.Validation;
public class NotNullValidationRule : ValidationRule
{
	public override ValidationResult Validate(object value, CultureInfo cultureInfo)
	{
		if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
		{
			return new ValidationResult(false, "Required field");
		}

		return ValidationResult.ValidResult;
	}
}