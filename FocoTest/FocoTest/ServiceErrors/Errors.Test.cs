using ErrorOr;


namespace FocoTest.ServiceErrors;

public static class Errors
{
    public static class Test
    {
        public static Error InvalidId => Error.Validation(
            code: "Test.InvalidId",
            description: $"Personal Id must be smaller than 50 characters");

            public static Error InvalidPhone => Error.Validation(
            code: "Test.InvalidPhone",
            description: $"Phone number must be smaller than 12 digits");

            public static Error InvalidFirstName => Error.Validation(
            code: "Test.InvalidFirstName",
            description: $"First name must be smaller than 20 characters");

            public static Error InvalidLastName => Error.Validation(
            code: "Test.InvalidLastName",
            description: $"Last name must be smaller than 20 characters");

            public static Error InvalidBirthDate => Error.Validation(
            code: "Test.InvalidBirthDate",
            description: $"Birth date must be bigger than {FocoTest.Models.Test.MinBirthDate} and smaller than {FocoTest.Models.Test.MaxBirthDate}");


            
    }
}