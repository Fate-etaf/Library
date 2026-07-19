using Library.Models;
using LibraryBorrowBook.Repositories;

namespace LibraryBorrowBook.Services
{
    public class BookService
    {
        private readonly BookRepository repository;

        public BookService()
        {
            repository = new BookRepository();
        }

        public List<Book> GetAllBooks()
        {
            return repository.GetAllBooks();
        }

        public Book? GetBookById(int id)
        {
            return repository.GetBookById(id);
        }

        public void Add(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new Exception("Title cannot be empty.");

            if (book.YearPublish <= 0)
                throw new Exception("Invalid publish year.");

            repository.Add(book);
        }

        public void Update(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new Exception("Title cannot be empty.");

            repository.Update(book);
        }

        public void Delete(int bookId)
        {
            repository.Delete(bookId);
        }
    }
}
