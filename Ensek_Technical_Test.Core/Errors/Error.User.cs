namespace Ensek_Techinical_Test.Core
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error Duplicate = new(ErrorType.Conflict, "Meter reading already exist");
        }
    }
}
