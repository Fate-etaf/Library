using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryBorrowBook.View
{
    public partial class FinesManagement : Window
    {
        private readonly FineService _fineService;
        private readonly BorrowService _borrowService;
        private readonly User _currentUser;

        public FinesManagement(User currentUser)
        {
            // Adding a simple converter dynamically to avoid adding a new converter class file
            if (!Application.Current.Resources.Contains("StatusToVisibilityConverter"))
            {
                Application.Current.Resources.Add("StatusToVisibilityConverter", new StatusToVisibilityConverter());
            }

            InitializeComponent();
            _currentUser = currentUser;
            _fineService = new FineService();
            _borrowService = new BorrowService();
            LoadData();

            if (_currentUser.Role != "Admin")
            {
                tbTitle.Text = "💸 My Fines";
                dgFines.Columns[6].Visibility = Visibility.Collapsed; // Hide 'Action' column for normal readers
            }
            else
            {
                panelAddFine.Visibility = Visibility.Visible;
                LoadActiveBorrows();
            }
        }

        private void LoadActiveBorrows()
        {
            var activeBorrows = _borrowService.GetAllBorrows()
                .Where(b => b.Status == "Borrowing")
                .Select(b => new
                {
                    BorrowId = b.BorrowId,
                    DisplayInfo = $"ID: {b.BorrowId} - {b.User.UserName} (Book: {b.Book.Title})"
                }).ToList();

            cbBorrows.ItemsSource = activeBorrows;
        }

        private void LoadData()
        {
            if (_currentUser.Role == "Admin")
            {
                dgFines.ItemsSource = _fineService.GetAllFines();
            }
            else
            {
                dgFines.ItemsSource = _fineService.GetFinesByUserId(_currentUser.UserId);
            }
        }

        private void btnMarkPaid_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Fine fine)
            {
                var result = MessageBox.Show($"Are you sure you want to mark this fine as PAID for {fine.Amount:N0} VND?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _fineService.MarkAsPaid(fine.FineId);
                        MessageBox.Show("Fine marked as Paid.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(_currentUser);
            main.Show();
            this.Close();
        }

        private void btnAddFine_Click(object sender, RoutedEventArgs e)
        {
            if (cbBorrows.SelectedValue == null)
            {
                MessageBox.Show("Please select a Borrow Record.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int borrowId = (int)cbBorrows.SelectedValue;
            
            Fine newFine = new Fine
            {
                BorrowId = borrowId,
                Amount = amount,
                Reason = txtReason.Text,
                Status = "Unpaid",
                CreatedDate = DateOnly.FromDateTime(DateTime.Now)
            };

            try
            {
                _fineService.AddFine(newFine);
                MessageBox.Show("Fine created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAmount.Text = "";
                txtReason.Text = "";
                cbBorrows.SelectedIndex = -1;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Helper converter to hide "Mark as Paid" button if it's already Paid
    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string status && status == "Unpaid")
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
