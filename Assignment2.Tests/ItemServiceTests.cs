// File: Assignment2.Tests/ItemServiceTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting; // MSTest
using Moq; // Moq
using Assignment2.BLL.Services; // ItemService
using Assignment2.DAL;          // Item entity
using Assignment2.DAL.Repositories; // ItemRepository
using System; // Exceptions etc.
using System.Collections.Generic; // List

// Ensure the namespace matches your test project structure if needed
namespace Assignment2.Tests
{
    [TestClass] // Marks this class as containing tests for MSTest
    public class ItemServiceTests
    {
        // Declare Mock object for the repository dependency
        private Mock<ItemRepository> _mockItemRepository;
        // Declare the service under test
        private ItemService _itemService;

        // This method runs before each test method in this class
        [TestInitialize]
        public void Setup() // Method name can be anything, [TestInitialize] is key
        {
            // Create a fresh mock repository for each test to ensure isolation
            _mockItemRepository = new Mock<ItemRepository>();

            // Create an instance of the ItemService, injecting the mocked repository
            // This relies on ItemService having a constructor that accepts ItemRepository
            _itemService = new ItemService(_mockItemRepository.Object);
        }

        // --- Test Methods for AddItem ---

        [TestMethod] // Marks this method as a test
        public void AddItem_EmptyName_ReturnsFalseAndDoesNotCallRepository()
        {
            // Arrange: Define the input data for the test case
            string emptyName = "";
            string size = "AnySize";

            // Act: Call the method being tested
            bool result = _itemService.AddItem(emptyName, size);

            // Assert: Verify the outcome is as expected
            Assert.IsFalse(result, "AddItem should return false when ItemName is empty.");

            // Verify: Ensure the repository's AddItem method was never invoked
            // because the service's internal validation should have stopped it.
            _mockItemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Never);
        }

        [TestMethod]
        public void AddItem_NullName_ReturnsFalseAndDoesNotCallRepository()
        {
            // Arrange
            string nullName = null;
            string size = "AnySize";

            // Act
            bool result = _itemService.AddItem(nullName, size);

            // Assert
            Assert.IsFalse(result, "AddItem should return false when ItemName is null.");
            _mockItemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Never);
        }

        [TestMethod]
        public void AddItem_WhitespaceName_ReturnsFalseAndDoesNotCallRepository()
        {
            // Arrange
            string whitespaceName = "   ";
            string size = "AnySize";

            // Act
            bool result = _itemService.AddItem(whitespaceName, size);

            // Assert
            Assert.IsFalse(result, "AddItem should return false when ItemName is only whitespace.");
            _mockItemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Never);
        }

        [TestMethod]
        public void AddItem_ValidInput_CallsRepositoryAddAndReturnsTrue()
        {
            // Arrange
            string validName = "Test Widget";
            string size = "Medium";

            // Setup the mock repository's AddItem method. Since AddItem returns void,
            // we don't need a .Returns(). Moq handles setting up void methods.
            // This setup primarily allows us to Verify the call later.
            // Note: If AddItem was NOT virtual in ItemRepository, this Setup might not work as expected
            // and the Verify below could fail, but we assume it is virtual now.
            _mockItemRepository.Setup(repo => repo.AddItem(It.IsAny<Item>()));

            // Act
            bool result = _itemService.AddItem(validName, size);

            // Assert
            Assert.IsTrue(result, "AddItem should return true for valid input.");

            // Verify that the repository's AddItem method WAS called exactly once.
            // It.IsAny<Item>() matches any Item object passed to the method.
            _mockItemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Once);
        }

        [TestMethod]
        public void AddItem_RepositoryThrowsException_ReturnsFalse()
        {
            // Arrange
            string validName = "Test Widget";
            string size = "Medium";

            // Setup the mock repository to throw a specific exception when AddItem is called.
            // This simulates a database error during the save operation.
            _mockItemRepository.Setup(repo => repo.AddItem(It.IsAny<Item>()))
                               .Throws(new InvalidOperationException("Simulated database error"));

            // Act
            bool result = _itemService.AddItem(validName, size);

            // Assert
            Assert.IsFalse(result, "AddItem should return false when the repository throws an exception.");

            // Optional: Verify AddItem was still called (attempted) once, even though it threw.
            _mockItemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Once);
        }


    }
}