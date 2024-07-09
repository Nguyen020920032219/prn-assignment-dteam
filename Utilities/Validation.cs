using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Utilities
{
    public class Validation
    {
        // Kiểm tra chuỗi rỗng hoặc null
        public bool IsStringValid(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        // Kiểm tra độ dài chuỗi
        public bool IsStringLengthValid(string input, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return input.Length >= minLength && input.Length <= maxLength;
        }

        // Kiểm tra số nguyên dương
        public bool IsPositiveInteger(int input)
        {
            return input > 0;
        }

        // Kiểm tra số thập phân dương
        public bool IsPositiveDecimal(decimal input)
        {
            return input > 0;
        }

        // Kiểm tra định dạng email
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

        // Kiểm tra ngày hợp lệ
        public bool IsDateValid(DateTime date)
        {
            return date != default(DateTime);
        }

        // Kiểm tra giá trị nằm trong một khoảng nhất định
        public bool IsWithinRange(int input, int minValue, int maxValue)
        {
            return input >= minValue && input <= maxValue;
        }

        // Kiểm tra giá trị duy nhất trong danh sách
        public bool IsUnique<T>(T value, IEnumerable<T> collection)
        {
            return !collection.Contains(value);
        }

        // Kiểm tra số điện thoại hợp lệ
        public bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            // Giả sử định dạng số điện thoại là 10 chữ số
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10;
        }

        // Kiểm tra giá trị không âm
        public bool IsNonNegativeInteger(int input)
        {
            return input >= 0;
        }

        // Kiểm tra giá trị không âm cho số thập phân
        public bool IsNonNegativeDecimal(decimal input)
        {
            return input >= 0;
        }

        // Kiểm tra giá trị ngày nằm trong khoảng hợp lệ
        public bool IsDateWithinRange(DateTime date, DateTime minDate, DateTime maxDate)
        {
            return date >= minDate && date <= maxDate;
        }

        // Kiểm tra chuỗi có chứa ký tự đặc biệt
        public bool ContainsSpecialCharacters(string input)
        {
            // Giả sử các ký tự đặc biệt là những ký tự không phải chữ cái và số
            return input.Any(ch => !char.IsLetterOrDigit(ch));
        }

        // Kiểm tra mật khẩu mạnh
        public bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpperChar = password.Any(char.IsUpper);
            bool hasLowerChar = password.Any(char.IsLower);
            bool hasMiniMaxChars = password.Length >= 8;
            bool hasNumber = password.Any(char.IsDigit);
            bool hasSymbols = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperChar && hasLowerChar && hasMiniMaxChars && hasNumber && hasSymbols;
        }
    }
}
