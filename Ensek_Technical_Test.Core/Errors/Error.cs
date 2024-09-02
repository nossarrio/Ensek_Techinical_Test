namespace Ensek_Techinical_Test.Core
{
    public class Error : Exception
    {
        public ErrorType ErrorType { get; }
        public ErrorSeverity Severity { get; }
        public string ErrorMessage { get; }

        public Error(ErrorType errorType, string errorMessage, ErrorSeverity severity = ErrorSeverity.Low)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            Severity = severity;
        }
    }
}
