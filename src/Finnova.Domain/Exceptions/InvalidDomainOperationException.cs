namespace Finnova.Domain.Exceptions
{
    public class InvalidDomainOperationException : Exception
    {
        public InvalidDomainOperationException(string message) : base(message)
        {
            
        }
    }
}