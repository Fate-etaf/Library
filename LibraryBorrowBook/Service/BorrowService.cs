using Library.Models;
using LibraryBorrowBook.Repositories;

namespace LibraryBorrowBook.Services
{
    public class BorrowService
    {
        private readonly BorrowRepository repository;
        private readonly SystemConfigService configService;
        private readonly FineService fineService;

        public BorrowService()
        {
            repository = new BorrowRepository();
            configService = new SystemConfigService();
            fineService = new FineService();
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

            var bookRepo = new BookRepository();
            var book = bookRepo.GetBookById(borrow.BookId);
            if (book == null) throw new Exception("Book not found.");
            if (book.Quantity <= 0) throw new Exception("Book is out of stock.");

            var activeBorrows = repository.GetBorrowsByUserId(borrow.ReaderId)
                .Where(b => b.Status != null && b.Status.Equals("Borrowing", StringComparison.OrdinalIgnoreCase))
                .ToList();

            int maxBooks = configService.GetConfigValueAsInt("MaxBooksPerReader", 3);

            if (activeBorrows.Count >= maxBooks)
                throw new Exception($"You cannot borrow more than {maxBooks} books at the same time.");

            if (activeBorrows.Any(b => b.BookId == borrow.BookId))
                throw new Exception("You are already borrowing this book.");

            borrow.BorrowDate = DateOnly.FromDateTime(DateTime.Now);
            borrow.Status = "Borrowing";

            book.Quantity -= 1;
            bookRepo.Update(book);

            repository.Add(borrow);
        }

        public void Update(Borrow borrow)
        {
            if (borrow.Status != null && borrow.Status.Equals("Returned", StringComparison.OrdinalIgnoreCase))
            {
                if (borrow.ReturnDate == null) // Process return logic only once
                {
                    borrow.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                    var bookRepo = new BookRepository();
                    var book = bookRepo.GetBookById(borrow.BookId);
                    if (book != null)
                    {
                        book.Quantity += 1;
                        bookRepo.Update(book);
                    }

                    // Calculate Fines
                    int maxDays = configService.GetConfigValueAsInt("MaxBorrowDays", 14);
                    decimal feePerDay = configService.GetConfigValueAsDecimal("LateFeePerDay", 5000);
                    
                    var borrowDateDateTime = borrow.BorrowDate.ToDateTime(new TimeOnly(0, 0));
                    var returnDateDateTime = borrow.ReturnDate.Value.ToDateTime(new TimeOnly(0, 0));
                    int daysBorrowed = (returnDateDateTime - borrowDateDateTime).Days;

                    if (daysBorrowed > maxDays)
                    {
                        int lateDays = daysBorrowed - maxDays;
                        decimal totalFine = lateDays * feePerDay;
                        
                        Fine newFine = new Fine
                        {
                            BorrowId = borrow.BorrowId,
                            Amount = totalFine,
                            Reason = $"Late by {lateDays} day(s).",
                            Status = "Unpaid",
                            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
                        };
                        fineService.AddFine(newFine);
                    }
                }
            }
            repository.Update(borrow);
        }
    }
}
