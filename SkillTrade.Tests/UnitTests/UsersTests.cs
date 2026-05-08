using SkillTrade.Core.Models;

namespace SkillTrade.Tests.UnitTests
{
    [TestClass]
    public class UsersTests
    {
        private Guid _validId;
        private DateTime _validCreatedAt;
        private const string ValidLogin = "john_doe";
        private const string ValidName = "John Doe";
        private const string ValidPassword = "Password123";
        private const string ValidRole = "student";
        private const decimal ValidBalance = 100;

        [TestInitialize]
        public void Setup()
        {
            _validId = Guid.NewGuid();
            _validCreatedAt = DateTime.UtcNow;
        }

        [TestMethod]
        public void Create_WhenAllFieldsAreValid_ShouldReturnSuccess()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(_validId, result.Value.Id);
            Assert.AreEqual(ValidLogin, result.Value.Login);
            Assert.AreEqual(ValidName, result.Value.Name);
            Assert.AreEqual(ValidRole.ToLower(), result.Value.Role.ToLower());
            Assert.AreEqual(ValidBalance, result.Value.Balance);
            Assert.AreNotEqual(ValidPassword, result.Value.HashPassword);
        }

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var result = Users.Create(
                Guid.Empty, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenLoginIsInvalid_ShouldReturnFailure()
        {
            var shortLogin = "ab";
            var result = Users.Create(
                _validId, shortLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Login должно содержать минимум {Users.MIN_LENGTH_LOGIN} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenNameIsInvalid_ShouldReturnFailure()
        {
            var shortName = "a";
            var result = Users.Create(
                _validId, ValidLogin, shortName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Name должно содержать минимум {Users.MIN_LENGTH_NAME} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenPasswordIsTooShort_ShouldReturnFailure()
        {
            var shortPassword = "abc";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, shortPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPasswordHasNoUppercase_ShouldReturnFailure()
        {
            var passwordNoUppercase = "password123";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, passwordNoUppercase,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Password должно содержать хотя бы одну заглавную букву", result.Error);
        }

        [TestMethod]
        public void Create_WhenPasswordHasNoDigit_ShouldReturnFailure()
        {
            var passwordNoDigit = "PasswordABC";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, passwordNoDigit,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Password должно содержать хотя бы одну цифру", result.Error);
        }

        [TestMethod]
        public void Create_WhenRoleIsInvalid_ShouldReturnFailure()
        {
            var invalidRole = "superadmin";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                invalidRole, ValidBalance, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Role должно быть одной из следующих ролей: admin, actor, student", result.Error);
        }

        [TestMethod]
        [DataRow("admin")]
        [DataRow("actor")]
        [DataRow("student")]
        public void Create_WhenRoleIsValid_ShouldSucceed(string role)
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                role, ValidBalance, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenBalanceIsNegative_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, -100, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Balance не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsInFuture_ShouldReturnFailure()
        {
            var futureDate = DateTime.UtcNow.AddDays(1);
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, futureDate);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CreatedAt не может быть в будущем", result.Error);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var result = Users.Create(
                _validId, "abc", "ab", "Pass12",
                "student", 0, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("abc", result.Value.Login);
            Assert.AreEqual("ab", result.Value.Name);
        }

        [TestMethod]
        public void Create_ShouldHashPasswordCorrectly()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(Users.VerifyPassword(ValidPassword, result.Value.HashPassword));
        }

        [TestMethod]
        public void VerifyPassword_WhenPasswordIsCorrect_ShouldReturnTrue()
        {
            var password = "TestPassword123";
            var hashPassword = Users.PasswordHasherService.HashBCrypt(password);

            var result = Users.VerifyPassword(password, hashPassword);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyPassword_WhenPasswordIsIncorrect_ShouldReturnFalse()
        {
            var password = "TestPassword123";
            var wrongPassword = "WrongPassword456";
            var hashPassword = Users.PasswordHasherService.HashBCrypt(password);

            var result = Users.VerifyPassword(wrongPassword, hashPassword);

            Assert.IsFalse(result);
        }
    }
}