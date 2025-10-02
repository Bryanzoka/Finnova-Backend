using Finnova.Domain.Services;
using FinnovaAPI.Repositories;

namespace Finnova.Infrastructure.Services
{
    public class ClientUniquenessChecker : IClientUniquenessChecker
    {
        private readonly IClientRepository _clientRepository;

        public ClientUniquenessChecker(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task EnsureAllIsUniqueAsync(string cpf, string email, string phone)
        {
            if (await _clientRepository.CpfExistsAsync(cpf))
            {
                throw new InvalidOperationException("CPF already exists");
            }

            if (await _clientRepository.EmailExistsAsync(email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            if (await _clientRepository.PhoneExistsAsync(phone))
            {
                throw new InvalidOperationException("Phone already exists");
            }
        }

        public async Task EnsureCpfIsUniqueAsync(string cpf)
        {
            if (await _clientRepository.CpfExistsAsync(cpf))
            {
                throw new InvalidOperationException("CPF already exists");
            }
        }

        public async Task EnsureEmailIsUniqueAsync(string email)
        {
            if (await _clientRepository.EmailExistsAsync(email))
            {
                throw new InvalidOperationException("Email already exists");
            }
        }

        public async Task EnsurePhoneIsUniqueAsync(string phone)
        {
            if (await _clientRepository.PhoneExistsAsync(phone))
            {
                throw new InvalidOperationException("Phone already exists");
            }
        }
    }
}