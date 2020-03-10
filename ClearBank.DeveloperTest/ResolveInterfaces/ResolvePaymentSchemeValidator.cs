using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.ResolveInterfaces
{
    public class ResolvePaymentSchemeValidator
    {
        public virtual IPaymentSchemeValidator GetPaymentSchemeValidator(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsPaymentSchemeValidator();
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentSchemeValidator();
                default:
                    return new ChapsPaymentSchemeValidator();
            }
        }
    }
}
