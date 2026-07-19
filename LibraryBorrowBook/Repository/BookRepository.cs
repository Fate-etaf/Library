using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBorrowBook.Repositories
{
    public class BookRepository
    {
        public List<Book> GetAllBooks()
        {
            using LibraryDbContext context = new LibraryDbContext();

            return context.Books
                .Include(b => b.Category)
                .ToList();
        }

        public Book? GetBookById(int bookId)
        {
            using LibraryDbContext context = new LibraryDbContext();

            return context.Books
                .Include(b => b.Category)
                .FirstOrDefault(b => b.BookId == bookId);
        }

        public void Add(Book book)
        {
            using LibraryDbContext context = new LibraryDbContext();

            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            using LibraryDbContext context = new LibraryDbContext();

            context.Books.Update(book);
            context.SaveChanges();
        }

        public void Delete(int bookId)
        {
            using LibraryDbContext context = new LibraryDbContext();

            var book = context.Books.Find(bookId);
            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }
    }
}