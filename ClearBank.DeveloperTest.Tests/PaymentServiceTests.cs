using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;
using System;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private MakePaymentRequest makePaymentRequest;
        private PaymentService paymentService;

        [SetUp]
        public void SetUp()
        {
            // arrange
            makePaymentRequest = new MakePaymentRequest();
        }

        [Test]
        public void MakePaymentNotEligible()
        {         
            //arrange
            makePaymentRequest.Amount = 10;
            makePaymentRequest.DebtorAccountNumber = "12345";
            makePaymentRequest.PaymentScheme = PaymentScheme.Bacs;
            makePaymentRequest.PaymentDate = DateTime.Now;

            var makePaymentResult = new MakePaymentResult()
            {
                Success = false
            };

            var mockResolvePaymentValidator = new Mock<IPaymentValidator>();
            mockResolvePaymentValidator.SetupAllProperties();
            mockResolvePaymentValidator.Setup(x => x.CheckPaymentEligible(PaymentScheme.Bacs, It.IsAny<Account>(), It.IsAny<decimal>())).Returns(makePaymentResult);
            paymentService = new PaymentService(mockResolvePaymentValidator.Object);

            //act
            var result = paymentService.MakePayment(makePaymentRequest);

            //assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void MakePaymentSuccess()
        {
            // arrange
            makePaymentRequest.Amount = 10;
            makePaymentRequest.DebtorAccountNumber = "12345";
            makePaymentRequest.PaymentScheme = PaymentScheme.Bacs;
            makePaymentRequest.PaymentDate = DateTime.Now;

            var makePaymentResult = new MakePaymentResult()
            {
                Success = true
            };
            var mockResolvePaymentValidator = new Mock<IPaymentValidator>();
            mockResolvePaymentValidator.SetupAllProperties();
            mockResolvePaymentValidator.Setup(x => x.CheckPaymentEligible(PaymentScheme.Bacs, It.IsAny<Account>(), It.IsAny<decimal>())).Returns(makePaymentResult);
            paymentService = new PaymentService(mockResolvePaymentValidator.Object);

            //act
            var result = paymentService.MakePayment(makePaymentRequest);

            //assert
            Assert.IsTrue(result.Success);
        }
    }
}
