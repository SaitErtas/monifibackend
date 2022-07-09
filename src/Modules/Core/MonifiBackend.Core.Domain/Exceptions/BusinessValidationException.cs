namespace MonifiBackend.Core.Domain.Exceptions;

public class BusinessValidationException : BaseException
{
    private string _displayMessage;
    private string _excetionMessage;
    public BusinessValidationException(string displayMessage, string excetionMessage) : base()
    {
        _displayMessage = displayMessage;
        _excetionMessage = excetionMessage;
    }
    public BusinessValidationException(BusinessValidationMessageType messageType, string property, object value) : base()
    {
        _displayMessage = BusinessValidationExceptionMessages.SetMessage(messageType, property);
        _excetionMessage = BusinessValidationExceptionMessages.SetLogMessage(messageType, property, value.ToString());
    }
    public override string ExceptionId => "59caf220-9308-4a63-86d9-281f5bd88f64";
    public override string DisplayMessage => $"{_displayMessage}";
    public override string Message => $"Business Exception! {_excetionMessage}";
    public override int Status => 500;
}
public enum BusinessValidationMessageType
{
    ALREADY_EXIST = 1,
    NotCreated = 2
}
public static class BusinessValidationExceptionMessages
{
    public static readonly string ALREADY_EXIST = "{0} already exist.";
    public static readonly string LOG_NEGATIVE_OR_ZERO = "{0} already exist. {0}: {1}";
    public static string SetMessage(BusinessValidationMessageType messageType, string message)
    {
        switch (messageType)
        {
            case BusinessValidationMessageType.ALREADY_EXIST:
                return string.Format(ALREADY_EXIST, message);
        }
        throw new NotImplementedException();
    }
    public static string SetLogMessage(BusinessValidationMessageType messageType, string message, string value)
    {
        switch (messageType)
        {
            case BusinessValidationMessageType.ALREADY_EXIST:
                return string.Format(LOG_NEGATIVE_OR_ZERO, message, value);
        }
        throw new NotImplementedException();
    }
}
