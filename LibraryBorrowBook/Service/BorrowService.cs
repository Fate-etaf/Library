using Library.Models;
using LibraryBorrowBook.Repositories;

namespace LibraryBorrowBook.Services
{
    public class BorrowService
    {
        private readonly BorrowRepository repository;

        public BorrowService()
        {
            repository = new BorrowRepository();
        }

        public List<Borrow> GetAllBorrows()
        {
            return repository.GetAllBorrows();
        }

        public List<Borrow> GetBorrowsByUserId(int userId)
        {
            return repository.GetBorrowsByUserId(userId);
        }
        public void Add(Borrow borrow)
        {
            if (borrow == null)
                throw new Exception("Borrow cannot be null.");
            repository.Add(borrow);
        }

        public void Update(Borrow borrow)
        {
            repository.Update(borrow);
        }
    }
}
