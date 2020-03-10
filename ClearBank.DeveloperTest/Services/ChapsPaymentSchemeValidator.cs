using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ChapsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentSchemeAllowedOnAccount(Account account)
        {
            if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return true;
            }

            return false;
        }
    }
}
