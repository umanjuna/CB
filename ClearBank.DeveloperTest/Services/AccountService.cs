using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        public bool CheckAccountStatus(Account account, AccountStatus accountStatus)
        {
            if (account.Status != accountStatus)
            {
                return false;
            }

            return true;
        }

        public bool HasSuffientBalance(Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                return false;
            }

            return true;
        }
    }
}
