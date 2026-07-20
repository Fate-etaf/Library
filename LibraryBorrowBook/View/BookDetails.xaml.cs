using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LibraryBorrowBook.View
{
    public partial class BookDetails : Window
    {
        private readonly User _currentUser;
        private readonly Book _currentBook;
        private readonly BookReviewService _reviewService;

        public BookDetails(User currentUser, Book book)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _currentBook = book;
            _reviewService = new BookReviewService();

            LoadBookInfo();
            LoadReviews();
        }

        private void LoadBookInfo()
        {
            tbTitle.Text = _currentBook.Title;
            tbAuthor.Text = "Author: " + _currentBook.Author;
            tbCategory.Text = "Category: " + (_currentBook.Category?.CategoryName ?? "Unknown");
            tbQuantity.Text = "Available: " + _currentBook.Quantity;

            try
            {
                if (!string.IsNullOrEmpty(_currentBook.Image))
                {
                    imgBook.Source = new BitmapImage(new Uri(_currentBook.Image));
                }
            }
            catch
            {
                // Ignore image load errors
            }
        }

        private void LoadReviews()
        {
            lvReviews.ItemsSource = _reviewService.GetReviewsByBookId(_currentBook.BookId);
        }

        private void btnSubmitReview_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Please enter a comment.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int rating = int.Parse(((System.Windows.Controls.ComboBoxItem)cbRating.SelectedItem).Content.ToString());

            BookReview review = new BookReview
            {
                BookId = _currentBook.BookId,
                UserId = _currentUser.UserId,
                Rating = rating,
                Comment = txtComment.Text,
                ReviewDate = DateTime.Now
            };

            try
            {
                _reviewService.AddReview(review);
                txtComment.Text = string.Empty;
                cbRating.SelectedIndex = 4; // Default to 5 stars
                LoadReviews();
                MessageBox.Show("Review submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
