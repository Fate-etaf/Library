using Library.Models;
using LibraryBorrowBook.Repositories;
using System.Collections.Generic;

namespace LibraryBorrowBook.Services
{
    public class BookReviewService
    {
        private readonly BookReviewRepository _repository;

        public BookReviewService()
        {
            _repository = new BookReviewRepository();
        }

        public List<BookReview> GetReviewsByBookId(int bookId)
        {
            return _repository.GetReviewsByBookId(bookId);
        }

        public void AddReview(BookReview review)
        {
            _repository.AddReview(review);
        }

        public void DeleteReview(int reviewId)
        {
            _repository.DeleteReview(reviewId);
        }
    }
}
