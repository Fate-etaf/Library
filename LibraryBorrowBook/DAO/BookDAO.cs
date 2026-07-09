using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBorrowBook
{
    class BookDAO
    {
        public static List<Book> GetAllBooks()
        {
            LibraryDbContext context = new LibraryDbContext();
            return context.Books
                .Include(s => s.Category)
                .ToList();
        }
    }
}
