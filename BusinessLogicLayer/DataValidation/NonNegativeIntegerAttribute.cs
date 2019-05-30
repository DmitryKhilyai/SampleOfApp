using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DataValidation
{
    public class NonNegativeIntegerAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int intValue && intValue >= 0)
            {
                return true;
            }

            return false;
        }
    }
}
