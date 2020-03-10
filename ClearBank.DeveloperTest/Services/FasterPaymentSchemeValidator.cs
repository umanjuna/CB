using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class FasterPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public bool IsPaymentSchemeAllowedOnAccount(Account account)
        {
            if (account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return true;
            }

            return false;
        }
    }
}

