using SkillTrade.Core.Abstractions;
using System.Text;

namespace SkillTrade.Core.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        private string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string _digits = "1234567890";
        public string GenerateCode(int length, bool isDigits = true, bool isChars = true)
        {
            if (!isDigits && !isChars)
                return string.Empty;
            string chs = string.Empty;
            if (isDigits)
                chs += _digits;
            if (isChars)
                chs += _chars;
            StringBuilder sb = new();
            Random rnd = new();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chs[rnd.Next(0, chs.Length)]);
            }
            return sb.ToString();
        }
    }
}
