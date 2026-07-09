using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBorrowBook
{
    class BorrowDAO
    {
        public List<Borrow> GetAllBorrows()
        {
            LibraryDbContext context = new LibraryDbContext();
            return context.Borrows.ToList();
        }
    }
}
