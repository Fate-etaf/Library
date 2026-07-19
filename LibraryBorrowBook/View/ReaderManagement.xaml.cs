using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
namespace LibraryBorrowBook.View
{
    public partial class ReaderManagement : Window
    {
        private readonly User _currentReader;
        private readonly ReaderService _readerService;
        private User? _selectedReader;

        public ReaderManagement(User currentReader)
        {
            InitializeComponent();
            _currentReader = currentReader;
            _readerService = new ReaderService();
            LoadData();
        }

        private void LoadData()
        {
            var readers = _readerService.GetAllReaders();
            dgReaders.ItemsSource = readers.Where(r => r.Role != null && r.Role.Equals("Reader", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private void dgReaders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedReader = dgReaders.SelectedItem as User;
            if (_selectedReader != null)
            {
                txtSelectedReader.Text = $"Selected Reader: {_selectedReader.UserName}";
            }
            else
            {
                txtSelectedReader.Text = "Selected Reader: None";
            }
        }

        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedReader == null)
            {
                MessageBox.Show("Please select a reader from the list first.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string newPassword = txtNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("New password cannot be empty.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _readerService.UpdatePassword(_selectedReader.UserId, newPassword);
                MessageBox.Show("Password updated successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                txtNewPassword.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteReader_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedReader == null)
            {
                MessageBox.Show("Please select a reader from the list first.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_selectedReader.UserId == _currentReader.UserId)
            {
                MessageBox.Show("You cannot delete your own account.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete reader '{_selectedReader.UserName}'? This will also remove their borrow records.",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _readerService.Delete(_selectedReader.UserId);
                    MessageBox.Show("Reader deleted successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_currentReader);
            mainWindow.Show();
            this.Close();
        }
    }
}
