using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private IPaymentValidator _paymentValidator;

        public PaymentService(IPaymentValidator paymentValidator)
        {
            _paymentValidator = paymentValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore = ResolveDataStore.GetDataStore(request);
            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            var result = _paymentValidator.CheckPaymentEligible(request.PaymentScheme, account, request.Amount);

            if (result.Success)
            {
                account.Balance -= request.Amount;
                accountDataStore = ResolveDataStore.GetDataStore(request);
                accountDataStore.UpdateAccount(account);
            }

            return result;
        }


    }
}
