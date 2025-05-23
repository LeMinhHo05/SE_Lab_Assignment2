using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.DAL.Repositories;
using Assignment2.DAL;

namespace Assignment2.BLL.Services
{
    public class UserService : IDisposable
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            // Create instance of the repository
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public UserService() : this(new UserRepository())
        {
            
        }
        // Method to validate user login credentials
        public User ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null; // Basic validation: username and password cannot be empty
            }

            // Call the repository method to check credentials against the database
            User user = _userRepository.GetUserByCredentials(username, password);

            // Optional: Add more business logic here, e.g., check if account is locked
            if (user != null && user.Lock == true)
            {
                // Optionally handle locked accounts differently, maybe throw an exception
                // For now, just return null as if login failed
                return null;
            }

            return user; // Return the user object if valid and not locked, otherwise null
        }

        // Implement IDisposable to dispose the repository
        public void Dispose()
        {
            _userRepository?.Dispose(); // Dispose the repository if it's not null
        }
    }
}
