using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        Item item;

        [SetUp]
        public void Setup()
        {
            item = new Item("Test", "TestID");
        }

        [Test]
        public void ItemConstructorWorksFine()
        {
            Assert.AreEqual("Test",item.Owner);
            Assert.AreEqual("TestID",item.ItemId);
        }

        [Test]
        public void  BankVaultWorksFine()
        {
            BankVault bankVault = new BankVault();
            Assert.IsNotNull(bankVault.VaultCells);
            
        }


        [Test]
        public void BankVaultAddItem()
        {
            BankVault bank = new BankVault();
            Item testItem = new Item("Nqmago", "nqmago");
            Assert.Throws<ArgumentException>(() => bank.AddItem("Nqmago", testItem));
        }


        [Test]
        public void BankVaultAddExistingItem()
        {
            BankVault bank = new BankVault();
            Assert.Throws<ArgumentException>(() => bank.AddItem("nqmaq", new Item("owner", "123")));

        }

        [Test]
        public void BankVaultAddCount()
        {
            BankVault bank = new BankVault();
            Item newitem = new Item("owner4e", "test");
            bank.AddItem("A1", newitem);
            Assert.AreEqual(12, bank.VaultCells.Count);
        }

        [Test]
        public void BankVaultAddEisitngCell()
        {
            BankVault bank = new BankVault();
            Item item = new Item("test", "test");
            bank.AddItem("A2", item);
            Assert.Throws<ArgumentException>(() => bank.AddItem("A2", new Item("owner", "id")));
            
        }

        //[Test]
        //public void BankVaultAddEisitngCell()
        //{
           


        //}

        [Test]
        public void BankVaultRemoveTest()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Test", "Test");
            bank.AddItem("A1", item);
            bank.RemoveItem("A1", item);
            Assert.AreEqual(bank.VaultCells["A1"], null);
        }

        [Test]
        public void BankVaultRemoveTestThrowExcp()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Test", "Test");
            bank.AddItem("A1", item);
            Assert.Throws<ArgumentException>(() => bank.RemoveItem("nqmago", item));
        }

        [Test]
        public void BankVaultRemoveTestThrowExcp2()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Test", "Test");
            Item item2 = new Item("Test2", "Test2");
            bank.AddItem("A1", item);
            bank.AddItem("A2", item2);
            Assert.Throws<ArgumentException>(() => bank.RemoveItem("A1", item2));
        }

        [Test]
        public void BankVaultRemoveWorksOkay()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Test", "Test");
            
            bank.AddItem("A1", item);
            bank.RemoveItem("A1", item);
            Assert.AreEqual(bank.VaultCells["A1"], null);
        }


    }
}