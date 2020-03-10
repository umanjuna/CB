using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentValidator
    {
        MakePaymentResult CheckPaymentEligible(PaymentScheme paymentScheme, Account account, decimal amount);
    }
}