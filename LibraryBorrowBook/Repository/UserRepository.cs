using Library.Models;

namespace LibraryBorrowBook.Repositories
{
    public class UserRepository
    {
        public User? GetUserByUsername(string username)
        {
            using LibraryDbContext context = new LibraryDbContext();

            return context.Users.FirstOrDefault(r =>
                r.UserName == username);
        }

        public List<User> GetAllReaders()
        {
            using LibraryDbContext context = new LibraryDbContext();
            return context.Users.ToList();
        }

        public void Update(User reader)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Users.Update(reader);
            context.SaveChanges();
        }

        public void Delete(int userId)
        {
            using LibraryDbContext context = new LibraryDbContext();
            var reader = context.Users.Find(userId);
            if (reader != null)
            {
                // Delete associated borrows first to avoid foreign key constraint violations
                var borrows = context.Borrows.Where(b => b.ReaderId == userId).ToList();
                if (borrows.Any())
                {
                    context.Borrows.RemoveRange(borrows);
                }
                
                context.Users.Remove(reader);
                context.SaveChanges();
            }
        }

        public void Add(User reader)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Users.Add(reader);
            context.SaveChanges();
        }
    }
}