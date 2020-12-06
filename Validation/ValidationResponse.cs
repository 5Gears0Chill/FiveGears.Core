namespace FiveGears.Core.Validation
{
    public class ValidationResponse
    {
        public ValidatorResult ValidationResults{ get; set; }

        public ValidationResponse()
        {
            ValidationResults = new ValidatorResult();
        }
    }
}
