using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkWearApi.Services
{
    public interface IValidationService
    {
        bool IsValidKey(string key);

        bool isValidValue(string value);
    }

    public class ValidationService : IValidationService
    {
        public bool IsValidKey(string key)
        {
            // Business Requirement: Keys must be url-safe (only alphanumeric, hyphen, period, underscore, tilde),
            // treated as case-insensitive and limited to 32 characters in size
            
            var regex = new Regex("^[a-zA-Z0-9-_.~]{1,32}$");
            return regex.IsMatch(key);
        }

        public bool isValidValue(string value)
        {
            // Business Requirement: Value is any text up to 1024 characters. 
            return value.Length < 1024;
        }
    }
}
