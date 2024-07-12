using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Service
{
    public class ValidationService
    {
        public bool IsStringValid(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public bool IsStringLengthValid(string input, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return input.Length >= minLength && input.Length <= maxLength;
        }

        public bool IsPositiveInteger(int input)
        {
            return input > 0;
        }

        public bool IsPositiveDecimal(decimal input)
        {
            return input > 0;
        }

        public bool IsEmailValid(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsDateValid(DateTime date)
        {
            return date != default(DateTime);
        }

        public bool IsWithinRange(decimal input, decimal minValue, decimal maxValue)
        {
            return input >= minValue && input <= maxValue;
        }

        public bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            return phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10;
        }

        public bool IsNonNegativeInteger(int input)
        {
            return input >= 0;
        }

        public bool IsNonNegativeDecimal(decimal input)
        {
            return input >= 0;
        }

        public bool IsDateWithinRange(DateTime date, DateTime minDate, DateTime maxDate)
        {
            return date >= minDate && date <= maxDate;
        }

        public bool ContainsSpecialCharacters(string input)
        {
            return input.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public bool IsNumber(string input)
        {
            return decimal.TryParse(input, out _);
        }

        public bool ValidateLicensePlate(string licensePlate)
        {
            string pattern = @"^[A-Za-z]{3}\d{3}$";
            return Regex.IsMatch(licensePlate, pattern);
        }
    }
}
