using Library.Models;
using LibraryBorrowBook.Repositories;
using System.Collections.Generic;

namespace LibraryBorrowBook.Services
{
    public class SystemConfigService
    {
        private readonly SystemConfigRepository _repository;

        public SystemConfigService()
        {
            _repository = new SystemConfigRepository();
        }

        public List<SystemConfig> GetAllConfigs()
        {
            return _repository.GetAllConfigs();
        }

        public string GetConfigValue(string key, string defaultValue)
        {
            var config = _repository.GetConfigByKey(key);
            return config?.ConfigValue ?? defaultValue;
        }

        public int GetConfigValueAsInt(string key, int defaultValue)
        {
            var valueStr = GetConfigValue(key, defaultValue.ToString());
            if (int.TryParse(valueStr, out int result))
            {
                return result;
            }
            return defaultValue;
        }

        public decimal GetConfigValueAsDecimal(string key, decimal defaultValue)
        {
            var valueStr = GetConfigValue(key, defaultValue.ToString());
            if (decimal.TryParse(valueStr, out decimal result))
            {
                return result;
            }
            return defaultValue;
        }

        public void UpdateConfig(SystemConfig config)
        {
            _repository.UpdateConfig(config);
        }
    }
}
