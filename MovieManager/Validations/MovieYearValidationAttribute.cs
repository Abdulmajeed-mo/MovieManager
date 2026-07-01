//using System.ComponentModel.DataAnnotations;

//namespace MovieManager.Validations
//{
//    public class MovieYearValidationAttribute : ValidationAttribute
//    {
//        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//        {
//            int year = (int)value!;

//            if (year >= 1900)
//            {
//                return ValidationResult.Success;
//            }

//            return new ValidationResult("Movie year must be greater than or equal to 1900.");
//        }
//    }
//}