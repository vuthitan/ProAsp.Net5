using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products =
        {
            new Product {Name= "Kayak", Category = "Watersprots", Price = 275M},
            new Product {Name= "Lifejacket", Category= "Watersprots", Price = 48.95M},
            new Product {Name= "Soccer ball", Category= "Soccer", Price = 19.50M},
            new Product {Name= "Corner flag", Category= "Soccer", Price = 34.95M}
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            //var discounter = new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            var target = new LinqValueCalculator(mock.Object);
            var goalTotal = products.Sum(e => e.Price);
            var result = target.ValueProducts(products);
            Assert.AreEqual(goalTotal, result);
        }

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            //arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                             .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                            .Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                            .Returns<decimal>(total => total * 0.9M);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                            .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            //assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 fail");
            target.ValueProducts(createProduct(0));
        }

    }
}

