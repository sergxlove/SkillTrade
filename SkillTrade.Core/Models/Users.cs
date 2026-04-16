using SkillTrade.Core.Abstractions;
using SkillTrade.Core.Infrastructures;
using SkillTrade.Core.Services;

namespace SkillTrade.Core.Models
{
    public class Users
    {
        public Guid Id { get; }
        public string Login { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public string HashPassword { get; } = string.Empty;
        public string Role { get; } = string.Empty;
        public decimal Balance { get; }
        public DateTime CreatedAt { get; }
        public static IPasswordHasherService PasswordHasherService { get; set; }
            = new PasswordHasherService();

        private Users(Guid id, string login, string name, string hashPassword, string role,
            decimal balance, DateTime createdAt, IPasswordHasherService passwordHasher)
        {
            Id = id;
            Login = login;
            Name = name;
            HashPassword = hashPassword;
            Role = role;
            Balance = balance;
            CreatedAt = createdAt;
            PasswordHasherService = passwordHasher;
        }

        private Users(Guid id, string login, string name, string hashPassword, string role,
            decimal balance, DateTime createdAt)
        {
            Id = id;
            Login = login;
            Name = name;
            HashPassword = hashPassword;
            Role = role;
            Balance = balance;
            CreatedAt = createdAt;
        }

        public static bool VerifyPassword(string password, string hashPassword)
        {
            return PasswordHasherService.VerifyBCrypt(password, hashPassword);
        }

        public static ResultModel<Users> Create(Guid id, string login, string name, string password,
            string role, decimal balance, DateTime createdAt)
        {
            if (id == Guid.Empty)
                return ResultModel<Users>.Failure("Поле Id не должно быть пустым");
            if (string.IsNullOrWhiteSpace(login))
                return ResultModel<Users>.Failure("Поле Login не должно быть пустым");
            if (login.Length < 3)
                return ResultModel<Users>.Failure("Поле Login должно содержать минимум 3 символа");
            if (login.Length > 50)
                return ResultModel<Users>.Failure("Поле Login не должно превышать 50 символов");
            if (string.IsNullOrWhiteSpace(name))
                return ResultModel<Users>.Failure("Поле Name не должно быть пустым");
            if (name.Length < 2)
                return ResultModel<Users>.Failure("Поле Name должно содержать минимум 2 символа");
            if (name.Length > 100)
                return ResultModel<Users>.Failure("Поле Name не должно превышать 100 символов");
            if (string.IsNullOrWhiteSpace(password))
                return ResultModel<Users>.Failure("Поле Password не должно быть пустым");
            if (password.Length < 6)
                return ResultModel<Users>.Failure("Поле Password должно содержать минимум 6 символов");
            if (password.Length > 100)
                return ResultModel<Users>.Failure("Поле Password не должно превышать 100 символов");
            if (password == password.ToLower())
                return ResultModel<Users>.Failure("Поле Password должно содержать хотя бы одну заглавную букву");
            if (!password.Any(char.IsDigit))
                return ResultModel<Users>.Failure("Поле Password должно содержать хотя бы одну цифру");
            if (string.IsNullOrWhiteSpace(role))
                return ResultModel<Users>.Failure("Поле Role не должно быть пустым");
            string[] allowedRoles = new[] { "admin", "actor", "student" };
            if (!allowedRoles.Contains(role.ToLower()))
                return ResultModel<Users>.Failure("Поле Role должно быть одной из следующих ролей: admin, actor, student");
            if (balance < 0)
                return ResultModel<Users>.Failure("Поле Balance не должно быть отрицательным");
            if (createdAt > DateTime.UtcNow)
                return ResultModel<Users>.Failure("Поле CreatedAt не может быть в будущем");
            var hashedPassword = PasswordHasherService.HashBCrypt(password);
            return ResultModel<Users>.Success(new Users(id, login, name, hashedPassword, role, balance,
                createdAt));
        }
    }
}
