using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KassaSystemProject;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace KassaSystemProject
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Test_CartItem_TotalPrice_Calculation()
        {
            // Arrange:
            var product = new Product { ID = 1, Name = "Äpple", Price = 15.0m };
            var cartItem = new CartItem(product, 3);

            //Act: (15*3) 
            decimal total = cartItem.TotalPrice;
             
            //Assert: 45
            Assert.AreEqual(45.0m, total);
        }
        [TestMethod]
        public void Test_Cart_GrandTotal_Calculation()
        {
            //Arrange:
            var p1 = new Product { ID = 1, Name = "Äpple", Price = 10.0m };
            var p2 = new Product { ID = 2, Name = "Mjölk", Price = 20.0m };

            var cart = new List<CartItem>
            {
                new CartItem(p1, 2), // 20 kr
                new CartItem(p2, 1)  // 20 kr

            };

            // Act:
            decimal grandTotal = 0;
            foreach (var item in cart)
            {
                grandTotal += item.TotalPrice;
            }

            // Assert: 40
            Assert.AreEqual(40.0m, grandTotal);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-3)]
        public void test_Invalid_Quantity_Should_Not_Be_Valid(int invalidQty)
        {

            //Arrange & Act:
            bool isValid = invalidQty > 0;

            // Assert:
            Assert.IsFalse(isValid);
        }
    }
}
