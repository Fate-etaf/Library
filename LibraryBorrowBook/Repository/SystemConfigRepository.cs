using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBorrowBook.Repositories
{
    public class SystemConfigRepository
    {
        private readonly LibraryDbContext _context;

        public SystemConfigRepository()
        {
            _context = new LibraryDbContext();
        }

        public List<SystemConfig> GetAllConfigs()
        {
            return _context.SystemConfigs.ToList();
        }

        public SystemConfig? GetConfigByKey(string key)
        {
            return _context.SystemConfigs.FirstOrDefault(c => c.ConfigKey == key);
        }

        public void UpdateConfig(SystemConfig config)
        {
            _context.SystemConfigs.Update(config);
            _context.SaveChanges();
        }
    }
}
