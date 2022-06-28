namespace MonifiBackend.Core.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        private string _displayMessage;
        private string _excetionMessage;
        public DomainException(string displayMessage, string excetionMessage) : base()
        {
            _displayMessage = displayMessage;
            _excetionMessage = excetionMessage;
        }
        public DomainException()
        {

        }
        public override string ExceptionId => "dbd35e2d-dc2d-4568-b415-4dcd291562c1";
        public override string DisplayMessage => $"{_displayMessage}";
        public override string Message => $"Domain Exception! {_excetionMessage}";
        public override int Status => 500;
    }
}
