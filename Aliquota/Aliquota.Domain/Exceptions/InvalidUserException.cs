namespace Aliquota.Domain.Services.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string message) : base(message) { }
    }
}
