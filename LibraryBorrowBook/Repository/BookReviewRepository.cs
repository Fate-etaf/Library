using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBorrowBook.Repositories
{
    public class BookReviewRepository
    {
        private readonly LibraryDbContext _context;

        public BookReviewRepository()
        {
            _context = new LibraryDbContext();
        }

        public List<BookReview> GetReviewsByBookId(int bookId)
        {
            return _context.BookReviews
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .OrderByDescending(r => r.ReviewDate)
                .ToList();
        }

        public void AddReview(BookReview review)
        {
            _context.BookReviews.Add(review);
            _context.SaveChanges();
        }

        public void DeleteReview(int reviewId)
        {
            var review = _context.BookReviews.Find(reviewId);
            if (review != null)
            {
                _context.BookReviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}
