using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public static class CheckInputIsEmailOrMobile
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase
            );

        private static readonly Regex MobileRegex = new Regex(
            @"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled
            );

        public static InputType GetInputType(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("مقداری برای ارسال کد وارد نشده است", nameof(input));
            }

            if (EmailRegex.IsMatch(input))
            {
                return InputType.Email;
            }

            else if (MobileRegex.IsMatch(input))
            {
                return InputType.Mobile;
            }
            else
            {
                return InputType.Unknown;
            }
        }
    }
    public enum InputType
    {
        Email,
        Mobile,
        Unknown
    }
}
