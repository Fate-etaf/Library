using Library.Models;
using LibraryBorrowBook.Repositories;

namespace LibraryBorrowBook.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository repository;

        public CategoryService()
        {
            repository = new CategoryRepository();
        }

        public List<Category> GetAllCategories()
        {
            return repository.GetAllCategories();
        }

        public void Add(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new Exception("Category name cannot be empty.");

            repository.Add(category);
        }

        public void Update(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new Exception("Category name cannot be empty.");

            repository.Update(category);
        }

        public void Delete(int categoryId)
        {
            // Optional: Check if any books are using this category before deleting.
            // But EF Core might handle it via constraints or cascade. 
            repository.Delete(categoryId);
        }
    }
}
