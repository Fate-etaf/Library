using Library.Models;
using LibraryBorrowBook.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LibraryBorrowBook.View
{
    public partial class CategoryManagement : Window
    {
        private readonly CategoryService _categoryService;
        private readonly User _currentReader;
        private Category? _selectedCategory;

        public CategoryManagement(User currentReader)
        {
            InitializeComponent();
            _currentReader = currentReader;
            _categoryService = new CategoryService();
            LoadData();
        }

        private void LoadData()
        {
            dgCategories.ItemsSource = _categoryService.GetAllCategories();
            txtCategoryName.Text = string.Empty;
            _selectedCategory = null;
            btnSave.Content = "Add";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedCategory == null)
                {
                    // Add new
                    Category newCat = new Category { CategoryName = txtCategoryName.Text };
                    _categoryService.Add(newCat);
                    MessageBox.Show("Category added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Update
                    _selectedCategory.CategoryName = txtCategoryName.Text;
                    _categoryService.Update(_selectedCategory);
                    MessageBox.Show("Category updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCategories.SelectedItem is Category cat)
            {
                _selectedCategory = cat;
                txtCategoryName.Text = cat.CategoryName;
                btnSave.Content = "Update";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Category cat)
            {
                var result = MessageBox.Show($"Are you sure you want to delete category '{cat.CategoryName}'?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _categoryService.Delete(cat.CategoryId);
                        MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot delete category. It might be in use.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_currentReader);
            this.SwitchTo(mainWindow);
        }
    }
}

