using Library.Models;
using LibraryBorrowBook.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ClosedXML.Excel;
using Microsoft.Win32;
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
                string keyword = txtSearchPhone?.Text?.Trim() ?? "";
                if (!string.IsNullOrEmpty(keyword))
                {
                    allBorrows = allBorrows.Where(b => 
                        b.User != null && 
                        (
                            (b.User.Phone != null && b.User.Phone.Contains(keyword)) ||
                            b.User.UserId.ToString().Contains(keyword) ||
                            (b.User.UserName != null && b.User.UserName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        )
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
            this.SwitchTo(mainWindow);
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

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            var data = dgBorrows.ItemsSource as IEnumerable<Borrow>;
            if (data == null || !data.Any())
            {
                MessageBox.Show("No data to export.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Save an Excel File",
                FileName = "BorrowList.xlsx"
            };

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Borrows");
                        
                        worksheet.Cell(1, 1).Value = "Borrow ID";
                        worksheet.Cell(1, 2).Value = "Reader ID";
                        worksheet.Cell(1, 3).Value = "Reader Name";
                        worksheet.Cell(1, 4).Value = "Phone";
                        worksheet.Cell(1, 5).Value = "Book";
                        worksheet.Cell(1, 6).Value = "Borrow Date";
                        worksheet.Cell(1, 7).Value = "Return Date";
                        worksheet.Cell(1, 8).Value = "Status";

                        var headerRow = worksheet.Range("A1:H1");
                        headerRow.Style.Font.Bold = true;
                        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                        int row = 2;
                        foreach (var borrow in data)
                        {
                            worksheet.Cell(row, 1).Value = borrow.BorrowId;
                            worksheet.Cell(row, 2).Value = borrow.User?.UserId ?? 0;
                            worksheet.Cell(row, 3).Value = borrow.User?.UserName ?? "";
                            worksheet.Cell(row, 4).Value = "'" + (borrow.User?.Phone ?? "");
                            worksheet.Cell(row, 5).Value = borrow.Book?.Title ?? "";
                            worksheet.Cell(row, 6).Value = borrow.BorrowDate.ToString("dd/MM/yyyy");
                            worksheet.Cell(row, 7).Value = borrow.ReturnDate?.ToString("dd/MM/yyyy") ?? "";
                            worksheet.Cell(row, 8).Value = borrow.Status;
                            row++;
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Exported to Excel successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

