using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBorrowBook.Repositories
{
    public class FineRepository
    {
        private readonly LibraryDbContext _context;

        public FineRepository()
        {
            _context = new LibraryDbContext();
        }

        public List<Fine> GetAllFines()
        {
            return _context.Fines
                .Include(f => f.Borrow)
                    .ThenInclude(b => b.User)
                .Include(f => f.Borrow)
                    .ThenInclude(b => b.Book)
                .OrderByDescending(f => f.CreatedDate)
                .ToList();
        }

        public List<Fine> GetFinesByUserId(int userId)
        {
            return _context.Fines
                .Include(f => f.Borrow)
                    .ThenInclude(b => b.Book)
                .Where(f => f.Borrow.ReaderId == userId)
                .OrderByDescending(f => f.CreatedDate)
                .ToList();
        }

        public Fine? GetFineById(int fineId)
        {
            return _context.Fines.Find(fineId);
        }

        public void AddFine(Fine fine)
        {
            _context.Fines.Add(fine);
            _context.SaveChanges();
        }

        public void UpdateFine(Fine fine)
        {
            _context.Fines.Update(fine);
            _context.SaveChanges();
        }
    }
}
