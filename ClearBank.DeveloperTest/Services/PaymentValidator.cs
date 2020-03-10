using ClearBank.DeveloperTest.ResolveInterfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentValidator : IPaymentValidator
    {
        private ResolvePaymentSchemeValidator _resolvePaymentSchemeValidator;
        private IAccountService _accountService;

        public PaymentValidator(ResolvePaymentSchemeValidator resolvePaymentSchemeValidator, IAccountService accountService)
        {
            _resolvePaymentSchemeValidator = resolvePaymentSchemeValidator;
            _accountService = accountService;
        }

        public MakePaymentResult CheckPaymentEligible(PaymentScheme paymentScheme, Account account, decimal amount)
        {
            var result = new MakePaymentResult();
            var paymentSchemeValidator = _resolvePaymentSchemeValidator.GetPaymentSchemeValidator(paymentScheme);

            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    result.Success = (account != null) && paymentSchemeValidator.IsPaymentSchemeAllowedOnAccount(account);
                    break;

                case PaymentScheme.FasterPayments:
                    result.Success = (account != null) && paymentSchemeValidator.IsPaymentSchemeAllowedOnAccount(account)
                        && _accountService.HasSuffientBalance(account, amount);
                    break;

                case PaymentScheme.Chaps:
                    result.Success = (account != null) && paymentSchemeValidator.IsPaymentSchemeAllowedOnAccount(account)
                        && _accountService.CheckAccountStatus(account, AccountStatus.Live);
                    break;
            }

            return result;
        }
    }
}
