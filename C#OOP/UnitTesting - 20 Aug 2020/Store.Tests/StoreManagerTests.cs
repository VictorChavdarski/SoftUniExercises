using NUnit.Framework;
using System;

namespace Store.Tests
{
    public class StoreManagerTests
    {
        Product product;
        StoreManager storeManager;

        [SetUp]
        public void Setup()
        {
            product = new Product("Test", 10, 10);
            storeManager = new StoreManager();
        }

        [Test]
        public void ConstructorProductTrue()
        {
            Assert.AreEqual("Test",product.Name);
            Assert.AreEqual(10,product.Quantity);
            Assert.AreEqual(10,product.Price);
        }


        [Test]
        public void StoreManagerCount()
        {
            Assert.AreEqual(0,storeManager.Products.Count);
        }

        [Test]
        public void StoreManagerAddThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => storeManager.AddProduct(null));
        }

        [Test]
        public void StoreManagerAddThrowsExceptionWhenQuantity()
        {
            Assert.Throws<ArgumentException>(() => storeManager.AddProduct(new Product("Test2",-5,10)));
            Assert.Throws<ArgumentException>(() => storeManager.AddProduct(new Product("Test2",0,10)));
        }

        [Test]
        public void StoreManagerAddCorrect()
        {
            storeManager.AddProduct(product);
            Assert.AreEqual(1, storeManager.Products.Count);
            storeManager.AddProduct(new Product("Test3",1,10));
            Assert.AreEqual(2, storeManager.Count);
        }

        [Test]
        public void StoreManagerBuyProductNull()
        {
            Product productTest = new Product("nqma go", 10, 20);
            Assert.Throws<ArgumentNullException>(() => storeManager.BuyProduct(productTest.Name,1));
            
        }


        [Test]
        public void StoreManagerBuyProductNotEnoughQuantity()
        {
            Product productTest = new Product("Laptop", 3, 100);
            storeManager.AddProduct(productTest);
            Assert.Throws<ArgumentException>(() => storeManager.BuyProduct(productTest.Name, 4));

        }

        [Test]
        public void StoreManagerBuyProductCorrect()
        {
            Product productToBuy = new Product("Z", 5, 1);
            StoreManager manager = new StoreManager();
            manager.AddProduct(productToBuy);
            manager.BuyProduct(productToBuy.Name, 1);
            Assert.AreEqual(4, productToBuy.Quantity);

        }

        [Test]
        public void StoreManagerBuyProductPriceCorrect()
        {
            Product productTest = new Product("Laptop", 3, 100);
            storeManager.AddProduct(productTest);
            Assert.AreEqual(storeManager.BuyProduct(productTest.Name, 2), 200);

        }

        [Test]
        public void StoreManagerGetExpensiveProduct()
        {
            Product productTest = new Product("Laptop", 3, 100);
            Product productTest2 = new Product("Laptop", 3, 200); 
            storeManager.AddProduct(productTest);
            storeManager.AddProduct(productTest2);
            Assert.AreEqual(storeManager.GetTheMostExpensiveProduct(), productTest2);

        }
    }
}