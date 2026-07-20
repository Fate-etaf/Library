using Library.Models;
using LibraryBorrowBook.Repositories;
using System;
using System.Collections.Generic;

namespace LibraryBorrowBook.Services
{
    public class FineService
    {
        private readonly FineRepository _repository;

        public FineService()
        {
            _repository = new FineRepository();
        }

        public List<Fine> GetAllFines()
        {
            return _repository.GetAllFines();
        }

        public List<Fine> GetFinesByUserId(int userId)
        {
            return _repository.GetFinesByUserId(userId);
        }

        public void AddFine(Fine fine)
        {
            _repository.AddFine(fine);
        }

        public void MarkAsPaid(int fineId)
        {
            var fine = _repository.GetFineById(fineId);
            if (fine != null && fine.Status == "Unpaid")
            {
                fine.Status = "Paid";
                fine.PaidDate = DateOnly.FromDateTime(DateTime.Now);
                _repository.UpdateFine(fine);
            }
            else
            {
                throw new Exception("Fine not found or already paid.");
            }
        }
    }
}
