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
            LoadData();
        }

        private void LoadData()
        {
            dgBorrows.ItemsSource = borrowService.GetBorrowsByUserId(_currentReader.UserId);
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
                MessageBox.Show("This book has no image.");
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
                MessageBox.Show("Please select a borrow record.");
                return;
            }

            borrow.Status = "Returned";
            borrowService.Update(borrow);

            MessageBox.Show("Return successful.");

            LoadData();
        }
    }
}
