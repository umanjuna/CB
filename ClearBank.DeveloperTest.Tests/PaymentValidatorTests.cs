using ClearBank.DeveloperTest.ResolveInterfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class PaymentValidatorTests
    {
        private IPaymentValidator paymentValidator;
        private Account account;

        [Test]
        public void BacsSchemeValidationSuccess()
        {
            var mockResolvePaymentValidator = new Mock<ResolvePaymentSchemeValidator>();
            mockResolvePaymentValidator.SetupAllProperties();
            mockResolvePaymentValidator.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.Bacs)).Returns(new BacsPaymentSchemeValidator());

            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.SetupAllProperties();

            var account = new Account()
            {
                Balance = 10,
                AccountNumber = "12345",
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            paymentValidator = new PaymentValidator(mockResolvePaymentValidator.Object, mockAccountService.Object);

            var result = paymentValidator.CheckPaymentEligible(PaymentScheme.Bacs, account, 10);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void FasterPaymentsSchemeValidationSuccess()
        {
            var mockResolvePaymentValidator = new Mock<ResolvePaymentSchemeValidator>();
            mockResolvePaymentValidator.SetupAllProperties();
            mockResolvePaymentValidator.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.FasterPayments)).Returns(new FasterPaymentSchemeValidator());

            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.SetupAllProperties();
            mockAccountService.Setup(x => x.HasSuffientBalance(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(true);
            var account = new Account()
            {
                Balance = 10,
                AccountNumber = "12345",
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            paymentValidator = new PaymentValidator(mockResolvePaymentValidator.Object, mockAccountService.Object);

            var result = paymentValidator.CheckPaymentEligible(PaymentScheme.FasterPayments, account, 10);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void FasterPaymentsSchemeValidationInsufficientBalance()
        {
            var mockResolvePaymentValidator = new Mock<ResolvePaymentSchemeValidator>();
            mockResolvePaymentValidator.SetupAllProperties();
            mockResolvePaymentValidator.Setup(x => x.GetPaymentSchemeValidator(PaymentScheme.FasterPayments)).Returns(new FasterPaymentSchemeValidator());

            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.SetupAllProperties();
            mockAccountService.Setup(x => x.HasSuffientBalance(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(false);
            var account = new Account()
            {
                Balance = 9,
                AccountNumber = "12345",
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            paymentValidator = new PaymentValidator(mockResolvePaymentValidator.Object, mockAccountService.Object);

            var result = paymentValidator.CheckPaymentEligible(PaymentScheme.FasterPayments, account, 10);

            Assert.IsFalse(result.Success);
        }

        // Similar tests for each condition 
    }
}
