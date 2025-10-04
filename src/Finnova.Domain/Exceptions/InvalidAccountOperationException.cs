namespace Finnova.Domain.Exceptions
{
    public class InvalidAccountOperationException : DomainException
    {
        public InvalidAccountOperationException(string message) : base(message)
        {
        }
    }
}