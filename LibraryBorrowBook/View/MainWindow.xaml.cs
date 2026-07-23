using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryBorrowBook.View
{
    public partial class MainWindow : Window
    {
        private readonly BookService bookService = new BookService();
        private readonly BorrowService borrowService = new BorrowService();
        private readonly User _currentReader;

        public MainWindow(User currentReader)
        {
            InitializeComponent();
            _currentReader = currentReader;
            LoadDataBook();

            // Show Add Book and Readers buttons only for admin
            if (_currentReader.Role != null &&
                _currentReader.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                btnAddBook.Visibility = Visibility.Visible;
                btnReaders.Visibility = Visibility.Visible;
                btnCategories.Visibility = Visibility.Visible;
                btnSettings.Visibility = Visibility.Visible;
                DashboardPanel.Visibility = Visibility.Visible;

                // Load Dashboard Statistics
                txtTotalBooks.Text = bookService.GetAllBooks().Count.ToString();
                txtActiveBorrows.Text = borrowService.GetAllBorrows().Count(b => b.Status != null && b.Status.Equals("Borrowing", StringComparison.OrdinalIgnoreCase)).ToString();
                txtTotalReaders.Text = (new UserService()).GetAllReaders().Count.ToString();

                var visibleStyle = new Style(typeof(Button), (Style)FindResource(typeof(Button)));
                visibleStyle.Setters.Add(new Setter(VisibilityProperty, Visibility.Visible));
                Resources["AdminOnlyButtonStyle"] = visibleStyle;
            }
        }

        public void LoadDataBook()
        {
            var books = bookService.GetAllBooks();
            string keyword = txtSearch?.Text?.Trim().ToLower() ?? "";

            if (!string.IsNullOrEmpty(keyword))
            {
                books = books.Where(b => 
                    (b.Title != null && b.Title.ToLower().Contains(keyword)) ||
                    (b.Author != null && b.Author.ToLower().Contains(keyword))
                ).ToList();
            }

            icBooks.ItemsSource = books;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDataBook();
        }

        private void btnMyBorrow_Click(object sender, RoutedEventArgs e)
        {
            MyBorrow myBorrow = new MyBorrow(_currentReader);
            this.SwitchTo(myBorrow);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.SwitchTo(loginWindow);
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)((Button)sender).DataContext;

            Borrow borrow = new Borrow()
            {
                ReaderId = _currentReader.UserId,
                BookId = selectedBook.BookId
            };

            try
            {
                borrowService.Add(borrow);
                MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                
                MyBorrow myBorrow = new MyBorrow(_currentReader);
                this.SwitchTo(myBorrow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)((Button)sender).DataContext;

            UpdateBook updateBook = new UpdateBook(selectedBook, _currentReader);
            this.SwitchTo(updateBook);
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook(_currentReader);
            this.SwitchTo(addBook);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)((Button)sender).DataContext;

            var result = MessageBox.Show(
                $"Are you sure you want to delete \"{selectedBook.Title}\"?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bookService.Delete(selectedBook.BookId);
                    MessageBox.Show("Book deleted successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDataBook();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnReaders_Click(object sender, RoutedEventArgs e)
        {
            ReaderManagement readerManagement = new ReaderManagement(_currentReader);
            this.SwitchTo(readerManagement);
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            CategoryManagement categoryManagement = new CategoryManagement(_currentReader);
            this.SwitchTo(categoryManagement);
        }

        private void btnFines_Click(object sender, RoutedEventArgs e)
        {
            FinesManagement finesManagement = new FinesManagement(_currentReader);
            this.SwitchTo(finesManagement);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SystemSettings settings = new SystemSettings(_currentReader);
            this.SwitchTo(settings);
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)((Button)sender).DataContext;
            BookDetails bookDetails = new BookDetails(_currentReader, selectedBook);
            bookDetails.ShowDialog();
        }
    }
}
