namespace Finnova.Domain.Exceptions
{
    public class UnauthorizedDomainException : Exception
    {
        public UnauthorizedDomainException(string message) : base(message)
        {
            
        }
    }
}