using FiveGears.Core.Constants;

namespace FiveGears.Core.Models
{
    public class ValidationModel
    {
        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
        public static ValidationModel Success => new ValidationModel
        {
            IsValid = true,
            ValidationMessage = Codes.ValidationMessageConstants.Success
        };
    }
}
