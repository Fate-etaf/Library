using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBorrowBook.DAO
{
    class LoginDAO
    {
        public static Reader? CheckLogin(string username, string password)
        {
            using (LibraryDbContext context = new LibraryDbContext())
            {
                return context.Readers.FirstOrDefault(a =>
                a.ReaderName == username &&
                a.Password == password);
            }
        }
    }
}
