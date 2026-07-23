using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LibraryBorrowBook.View
{
    public partial class AddBook : Window
    {
        private readonly BookService bookService = new BookService();
        private readonly User _currentReader;

        public AddBook(User currentReader)
        {
            InitializeComponent();
            _currentReader = currentReader;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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

                Book newBook = new Book()
                {
                    Title = title,
                    Author = string.IsNullOrWhiteSpace(author) ? null : author,
                    Publisher = string.IsNullOrWhiteSpace(publisher) ? null : publisher,
                    YearPublish = yearPublish,
                    Quantity = quantity,
                    CategoryId = categoryId,
                    Image = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl
                };

                bookService.Add(newBook);

                MessageBox.Show("Book added successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Go back to main window
                MainWindow mainWindow = new MainWindow(_currentReader);
                this.SwitchTo(mainWindow);
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
            this.SwitchTo(mainWindow);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPublisher.Text = "";
            txtYearPublish.Text = "";
            txtQuantity.Text = "";
            txtCategoryId.Text = "";
            txtImageUrl.Text = "";
            imgPreview.Source = null;
            txtPlaceholder.Visibility = Visibility.Visible;
        }

        private void txtImageUrl_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string url = txtImageUrl.Text.Trim();
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

