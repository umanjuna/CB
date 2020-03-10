using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        bool HasSuffientBalance(Account account, decimal amount);

        bool CheckAccountStatus(Account account, AccountStatus accountStatus);
    }
}
