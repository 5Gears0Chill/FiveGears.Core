using System.Collections.Generic;
using System.Linq;

namespace FiveGears.Core.Validation
{
    public class ValidatorResult
    {
        public bool IsValid => !ErrorMessages.Any();

        public List<string> ErrorMessages { get; set; }

        public ValidatorResult()
            :this(new List<string>())
        {
        }

        public ValidatorResult(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
