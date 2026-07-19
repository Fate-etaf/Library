using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LibraryBorrowBook.View
{
    public partial class UpdateBook : Window
    {
        private readonly BookService bookService = new BookService();
        private readonly User _currentReader;
        private readonly Book _bookToUpdate;

        public UpdateBook(Book bookToUpdate, User currentReader)
        {
            InitializeComponent();
            _bookToUpdate = bookToUpdate;
            _currentReader = currentReader;

            // Auto-fill existing details
            AutoFillData();
        }

        private void AutoFillData()
        {
            txtTitle.Text = _bookToUpdate.Title;
            txtAuthor.Text = _bookToUpdate.Author;
            txtPublisher.Text = _bookToUpdate.Publisher;
            txtYearPublish.Text = _bookToUpdate.YearPublish?.ToString();
            txtQuantity.Text = _bookToUpdate.Quantity?.ToString();
            txtCategoryId.Text = _bookToUpdate.CategoryId?.ToString();
            txtImageUrl.Text = _bookToUpdate.Image;
            
            UpdateImagePreview(_bookToUpdate.Image);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = txtTitle.Text.Trim();
                string author = txtAuthor.Text.Trim();
                string publisher = txtPublisher.Text.Trim();
                string yearText = txtYearPublish.Text.Trim();
                string quantityText = txtQuantity.Text.Trim();
                string categoryIdText = txtCategoryId.Text.Trim();
                string imageUrl = txtImageUrl.Text.Trim();

                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Title is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(yearText, out int yearPublish) || yearPublish <= 0)
                {
                    MessageBox.Show("Please enter a valid publish year.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int? quantity = null;
                if (!string.IsNullOrWhiteSpace(quantityText))
                {
                    if (!int.TryParse(quantityText, out int qty) || qty < 0)
                    {
                        MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    quantity = qty;
                }

                int? categoryId = null;
                if (!string.IsNullOrWhiteSpace(categoryIdText))
                {
                    if (!int.TryParse(categoryIdText, out int catId))
                    {
                        MessageBox.Show("Please enter a valid Category ID.", "Validation Error",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    categoryId = catId;
                }

                // Update the object
                _bookToUpdate.Title = title;
                _bookToUpdate.Author = string.IsNullOrWhiteSpace(author) ? null : author;
                _bookToUpdate.Publisher = string.IsNullOrWhiteSpace(publisher) ? null : publisher;
                _bookToUpdate.YearPublish = yearPublish;
                _bookToUpdate.Quantity = quantity;
                _bookToUpdate.CategoryId = categoryId;
                _bookToUpdate.Image = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl;

                bookService.Update(_bookToUpdate);

                MessageBox.Show("Book updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Go back to main window
                MainWindow mainWindow = new MainWindow(_currentReader);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_currentReader);
            mainWindow.Show();
            this.Close();
        }

        private void txtImageUrl_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateImagePreview(txtImageUrl.Text.Trim());
        }

        private void UpdateImagePreview(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    imgPreview.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                    txtPlaceholder.Visibility = Visibility.Collapsed;
                }
                catch
                {
                    imgPreview.Source = null;
                    txtPlaceholder.Visibility = Visibility.Visible;
                }
            }
            else
            {
                imgPreview.Source = null;
                txtPlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
