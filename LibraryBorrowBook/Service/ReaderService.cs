using Library.Models;
using LibraryBorrowBook.Repositories;

namespace LibraryBorrowBook.Services
{
    public class ReaderService
    {
        private readonly ReaderRepository _repository;

        public ReaderService()
        {
            _repository = new ReaderRepository();
        }

        public User? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required.");

            return _repository.CheckLogin(username, password);
        }

        public List<User> GetAllReaders()
        {
            return _repository.GetAllReaders();
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("New password cannot be empty.");

            var reader = _repository.GetAllReaders().FirstOrDefault(r => r.UserId == userId);
            if (reader != null)
            {
                reader.Password = newPassword;
                _repository.Update(reader);
            }
            else
            {
                throw new Exception("Reader not found.");
            }
        }

        public void Delete(int userId)
        {
            _repository.Delete(userId);
        }

        public void Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required.");

            // Check if user already exists
            var existingUsers = _repository.GetAllReaders();
            if (existingUsers.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Username already exists.");
            }

            User newUser = new User
            {
                UserName = username,
                Password = password,
                Role = "Reader" // Force all registered accounts to be Reader
            };

            _repository.Add(newUser);
        }
    }
}