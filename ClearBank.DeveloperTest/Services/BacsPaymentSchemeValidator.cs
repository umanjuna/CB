using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class BacsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentSchemeAllowedOnAccount(Account account)
        {
            if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return true;
            }

            return false;
        }
    }
}
