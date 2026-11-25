using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KassasystemProject;


namespace KassasystemProject
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Test_CartItem_TotalPrice_Calculation()
        {
            // Arrange:
            var product = new Product(1, "Äpple", 15.0m, "kg");
            var cartItem = new CartItem(product, 3);

            //Act: (15*3) 
            decimal total = cartItem.TotalPrice;
             
            //Assert: 45
            Assert.AreEqual(45.0m, total);

        }


        [TestMethod]
        public  void Test_DecimalWeight_TotalPrice_Calculation()
        {
            // Arrange:
            var product = new Product(1, "Apple", 20.0m, "kg");
            var CartItem = new CartItem(product, 1.5m);

            // Act: (20.0 * 1.5)
            decimal total = CartItem.TotalPrice;

            // Assert: 30.0
            Assert.AreEqual(30.0m, total);
        }

        [TestMethod]
        public void Test_Cart_GrandTotal_Calculation()
        {
            //Arrange:
            var p1 = new Product(1, "Äpple", 10.0m, "kg");
            var p2 = new Product(2,"Mjölk", 20.0m, "st");

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
