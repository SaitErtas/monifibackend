namespace MonifiBackend.Core.Domain.Exceptions
{
    public class BusinessValidationException : BaseException
    {
        private string _displayMessage;
        private string _excetionMessage;
        public BusinessValidationException(string displayMessage, string excetionMessage) : base()
        {
            _displayMessage = displayMessage;
            _excetionMessage = excetionMessage;
        }
        public override string ExceptionId => "59caf220-9308-4a63-86d9-281f5bd88f64";
        public override string DisplayMessage => $"{_displayMessage}";
        public override string Message => $"Business Exception! {_excetionMessage}";
        public override int Status => 500;
    }
}
