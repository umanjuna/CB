using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentSchemeValidator
    {
        bool IsPaymentSchemeAllowedOnAccount(Account account);
    }
}