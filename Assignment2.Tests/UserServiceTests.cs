using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; // Moq namespace
using Assignment2.BLL.Services; // Your UserService namespace
using Assignment2.DAL;          // Your User entity namespace
using Assignment2.DAL.Repositories; // Your UserRepository namespace (needed for mocking interface/class)
using System;

namespace Assignment2.Tests
{
    [TestClass] // MSTest attribute to indicate a test class
    public class UserServiceTests
    {
        // Declare Mock object for the repository dependency
        private Mock<UserRepository> _mockUserRepository;
        // Declare the service under test
        private UserService _userService;

        // TestInitialize runs before each test method in this class
        [TestInitialize]
        public void TestInitialize()
        {
            // Create a new mock object for the repository before each test
            _mockUserRepository = new Mock<UserRepository>();

            
            _userService = new UserService(_mockUserRepository.Object);

            Console.WriteLine("TestInitialize completed.");
        }


        [TestMethod] // MSTest attribute for a test method
        public void ValidateLogin_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var expectedUser = new User { UserID = 1, UserName = "test", Password = "password", Lock = false };
            string username = "test";
            string password = "password";

            // Setup the mock repository: When GetUserByCredentials is called with these specific inputs, return the expectedUser
            _mockUserRepository.Setup(repo => repo.GetUserByCredentials(username, password))
                               .Returns(expectedUser);

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            Assert.IsNotNull(actualUser, "User should not be null for valid credentials.");
            Assert.AreEqual(expectedUser.UserID, actualUser.UserID, "UserID does not match.");
            Assert.AreEqual(expectedUser.UserName, actualUser.UserName, "UserName does not match.");
            // We don't usually assert the password itself
        }

        [TestMethod]
        public void ValidateLogin_InvalidUsername_ReturnsNull()
        {
            // Arrange
            string username = "wronguser";
            string password = "password";

            // Setup the mock: When called with these inputs, return null (user not found)
            _mockUserRepository.Setup(repo => repo.GetUserByCredentials(username, password))
                               .Returns((User)null); // Explicitly cast null

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            Assert.IsNull(actualUser, "User should be null for invalid username.");
        }

        [TestMethod]
        public void ValidateLogin_InvalidPassword_ReturnsNull()
        {
            // Arrange
            string username = "test";
            string password = "wrongpassword";

            // Setup the mock: Return null for this combination
            _mockUserRepository.Setup(repo => repo.GetUserByCredentials(username, password))
                               .Returns((User)null);

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            Assert.IsNull(actualUser, "User should be null for invalid password.");
        }

        [TestMethod]
        public void ValidateLogin_LockedAccount_ReturnsNull()
        {
            // Arrange
            // Simulate the repository returning a user whose 'Lock' property is true
            var lockedUser = new User { UserID = 2, UserName = "locked", Password = "password", Lock = true };
            string username = "locked";
            string password = "password";

            _mockUserRepository.Setup(repo => repo.GetUserByCredentials(username, password))
                               .Returns(lockedUser);

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            // Based on our BLL logic, it should return null if user.Lock is true
            Assert.IsNull(actualUser, "User should be null for a locked account.");
        }

        [TestMethod]
        public void ValidateLogin_EmptyUsername_ReturnsNull()
        {
            // Arrange
            string username = "";
            string password = "password";
            // No mock setup needed as the service should handle this before calling repo

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            Assert.IsNull(actualUser, "User should be null for empty username.");
        }

        [TestMethod]
        public void ValidateLogin_EmptyPassword_ReturnsNull()
        {
            // Arrange
            string username = "test";
            string password = "";
            // No mock setup needed

            // Act
            User actualUser = _userService.ValidateLogin(username, password);

            // Assert
            Assert.IsNull(actualUser, "User should be null for empty password.");
        }

        

    }
}