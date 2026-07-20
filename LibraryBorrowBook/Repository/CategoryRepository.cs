using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBorrowBook.Repositories
{
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            using LibraryDbContext context = new LibraryDbContext();
            return context.Categories.ToList();
        }

        public void Add(Category category)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            using LibraryDbContext context = new LibraryDbContext();
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void Delete(int categoryId)
        {
            using LibraryDbContext context = new LibraryDbContext();
            var category = context.Categories.Find(categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
