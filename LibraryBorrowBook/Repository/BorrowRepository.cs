using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBorrowBook.Repositories
{
    public class BorrowRepository
    {
        public List<Borrow> GetAllBorrows()
        {
            using LibraryDbContext context = new LibraryDbContext();

            return context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToList();
        }

        public List<Borrow> GetBorrowsByUserId(int userId)
        {
            using LibraryDbContext context = new LibraryDbContext();

            return context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToList();
        }

        public void Add(Borrow borrow)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Borrows.Add(borrow);
            context.SaveChanges();
        }

        internal void Update(Borrow borrow)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Borrows.Update(borrow);
            context.SaveChanges();
        }
    }
}