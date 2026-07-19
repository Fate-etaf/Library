using Library.Models;
using LibraryBorrowBook.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LibraryBorrowBook.View
{
    public partial class MyBorrow : Window
    {
        private int borrowId;
        private readonly BorrowService borrowService = new BorrowService();
        private readonly User _currentReader;

        public MyBorrow(User currentReader)
        {
            InitializeComponent();
            _currentReader = currentReader;

            if (_currentReader.Role != null && _currentReader.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                txtHeader.Text = "📋 Manage Borrows";
                btnReturn.Visibility = Visibility.Visible;
                adminSearchPanel.Visibility = Visibility.Visible;
            }
            else
            {
                txtHeader.Text = "📋 My Borrows";
                btnReturn.Visibility = Visibility.Collapsed;
                adminSearchPanel.Visibility = Visibility.Collapsed;
            }

            LoadData();
        }

        private void LoadData()
        {
            if (_currentReader.Role != null && _currentReader.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                var allBorrows = borrowService.GetAllBorrows();
                string phoneKeyword = txtSearchPhone?.Text?.Trim() ?? "";
                if (!string.IsNullOrEmpty(phoneKeyword))
                {
                    allBorrows = allBorrows.Where(b => 
                        b.User != null && 
                        b.User.Phone != null && 
                        b.User.Phone.Contains(phoneKeyword)
                    ).ToList();
                }
                dgBorrows.ItemsSource = allBorrows;
            }
            else
            {
                var borrows = borrowService.GetBorrowsByUserId(_currentReader.UserId);
                dgBorrows.ItemsSource = borrows.Where(b => b.Status != null && b.Status.Equals("Borrowing", StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private void txtSearchPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_currentReader);
            mainWindow.Show();
            this.Close();
        }

        private void scReturn(object sender, SelectionChangedEventArgs e)
        {
            Borrow? borrow = dgBorrows.SelectedItem as Borrow;

            if (borrow == null)
                return;

            if (string.IsNullOrWhiteSpace(borrow.Book.Image))
            {
                imgBook.Source = null;
                return;
            }
            imgBook.Source = new BitmapImage(new Uri(borrow.Book.Image, UriKind.RelativeOrAbsolute));
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            Borrow? borrow = dgBorrows.SelectedItem as Borrow;

            if (borrow == null)
            {
                MessageBox.Show("Please select a borrow record.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (borrow.Status != null && borrow.Status.Equals("Returned", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("This book has already been returned.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            borrow.Status = "Returned";
            borrowService.Update(borrow);

            MessageBox.Show("Return successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadData();
        }
    }
}
