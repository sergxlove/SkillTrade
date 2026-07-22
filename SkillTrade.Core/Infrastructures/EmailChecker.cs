using System.Text.RegularExpressions;

namespace SkillTrade.Core.Infrastructures
{
    public class EmailChecker
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(250)
        );

        private static readonly HashSet<char> AllowedLocalChars = new()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '.', '_', '-', '+'
        };

        private static readonly HashSet<char> AllowedDomainChars = new()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            '-', '.'
        };

        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();

            if (email.Length < 5 || email.Length > 254)
                return false;

            if (!email.Contains('@'))
                return false;

            if (email.Contains(' '))
                return false;

            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            var localPart = parts[0];
            var domainPart = parts[1];

            if (!IsValidLocalPart(localPart))
                return false;

            if (!IsValidDomainPart(domainPart))
                return false;

            return EmailRegex.IsMatch(email);
        }

        private static bool IsValidLocalPart(string localPart)
        {
            if (string.IsNullOrEmpty(localPart))
                return false;

            if (localPart.Length < 1 || localPart.Length > 64)
                return false;

            if (localPart.StartsWith('.') || localPart.EndsWith('.'))
                return false;

            if (localPart.Contains(".."))
                return false;

            foreach (char c in localPart)
            {
                if (!AllowedLocalChars.Contains(c))
                    return false;
            }

            return true;
        }

        private static bool IsValidDomainPart(string domainPart)
        {
            if (string.IsNullOrEmpty(domainPart))
                return false;

            if (domainPart.Length < 3 || domainPart.Length > 253)
                return false;

            if (domainPart.StartsWith('.') || domainPart.EndsWith('.') ||
                domainPart.StartsWith('-') || domainPart.EndsWith('-'))
                return false;

            if (domainPart.Contains(".."))
                return false;

            foreach (char c in domainPart)
            {
                if (!AllowedDomainChars.Contains(c))
                    return false;
            }

            var parts = domainPart.Split('.');
            if (parts.Length < 2)
                return false;

            var tld = parts[^1];
            if (tld.Length < 2)
                return false;

            foreach (char c in tld)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            return true;
        }

        public static string ValidateWithDetails(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                return "Email не может быть пустым";
            }

            email = email.Trim();

            if (email.Length < 5)
            {
                return "Email слишком короткий (минимум 5 символов)";
            }

            if (email.Length > 254)
            {
                return"Email слишком длинный (максимум 254 символа)";
            }

            if (!email.Contains('@'))
            {
                return "Email должен содержать символ @";
            }

            if (email.Contains(' '))
            {
                return "Email не должен содержать пробелы";
            }

            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                return "Email должен содержать ровно один символ @";
            }

            var localPart = parts[0];
            var domainPart = parts[1];

            if (string.IsNullOrEmpty(localPart))
            {
                return "Локальная часть (до @) не может быть пустой";
            }

            if (localPart.Length > 64)
            {
                return "Локальная часть не может быть длиннее 64 символов";
            }

            if (localPart.StartsWith('.') || localPart.EndsWith('.'))
            {
                return "Локальная часть не может начинаться или заканчиваться на точку";
            }

            if (localPart.Contains(".."))
            {
                return "Локальная часть не может содержать две точки подряд";
            }

            var invalidLocalChars = localPart.Where(c => !AllowedLocalChars.Contains(c)).ToList();
            if (invalidLocalChars.Any())
            {
                return $"Локальная часть содержит недопустимые символы: {string.Join(", ", invalidLocalChars)}";
            }

            if (string.IsNullOrEmpty(domainPart))
            {
                return "Доменная часть (после @) не может быть пустой";
            }

            if (domainPart.Length > 253)
            {
                return "Доменная часть не может быть длиннее 253 символов";
            }

            if (domainPart.StartsWith('.') || domainPart.EndsWith('.') ||
                domainPart.StartsWith('-') || domainPart.EndsWith('-'))
            {
                return "Доменная часть не может начинаться или заканчиваться на точку или дефис";
            }

            if (domainPart.Contains(".."))
            {
                return "Доменная часть не может содержать две точки подряд";
            }

            var invalidDomainChars = domainPart.Where(c => !AllowedDomainChars.Contains(c)).ToList();
            if (invalidDomainChars.Any())
            {
                return $"Доменная часть содержит недопустимые символы: {string.Join(", ", invalidDomainChars)}";
            }

            var domainParts = domainPart.Split('.');
            if (domainParts.Length < 2)
            {
                return "Домен должен содержать как минимум одну точку (например, example.com)";
            }

            var tld = domainParts[^1];
            if (tld.Length < 2)
            {
                return "Доменная зона (последняя часть) должна содержать минимум 2 символа";
            }

            if (!tld.All(char.IsLetter))
            {
                return "Доменная зона должна содержать только буквы";
            }

            if (!EmailRegex.IsMatch(email))
            {
                return "Email не соответствует формату";
            }

            return string.Empty;
        }
    }
}
