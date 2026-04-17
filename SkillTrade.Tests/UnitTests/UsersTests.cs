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

        #region Tests for Id

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var emptyId = Guid.Empty;
            var result = Users.Create(
                emptyId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdIsValid_ShouldNotReturnIdError()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Login

        [TestMethod]
        public void Create_WhenLoginIsEmpty_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, "", ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Login не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenLoginIsWhitespace_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, "   ", ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Login не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenLoginIsTooShort_ShouldReturnFailure()
        {
            var shortLogin = "ab";
            var result = Users.Create(
                _validId, shortLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Login должно содержать минимум {Users.MIN_LENGTH_LOGIN} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenLoginIsExactlyMinLength_ShouldSucceed()
        {
            var exactMinLogin = "abc";
            var result = Users.Create(
                _validId, exactMinLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(exactMinLogin, result.Value.Login);
        }

        [TestMethod]
        public void Create_WhenLoginIsTooLong_ShouldReturnFailure()
        {
            var longLogin = new string('a', Users.MAX_LENGTH_LOGIN + 1);
            var result = Users.Create(
                _validId, longLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Login не должно превышать {Users.MAX_LENGTH_LOGIN} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenLoginIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxLogin = new string('a', Users.MAX_LENGTH_LOGIN);
            var result = Users.Create(
                _validId, exactMaxLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenLoginHasValidCharacters_ShouldSucceed()
        {
            var loginWithUnderscore = "john_doe_123";
            var result = Users.Create(
                _validId, loginWithUnderscore, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Name

        [TestMethod]
        public void Create_WhenNameIsEmpty_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, ValidLogin, "", ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Name не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenNameIsWhitespace_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, ValidLogin, "   ", ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Name не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenNameIsTooShort_ShouldReturnFailure()
        {
            var shortName = "a";
            var result = Users.Create(
                _validId, ValidLogin, shortName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Name должно содержать минимум {Users.MIN_LENGTH_NAME} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenNameIsExactlyMinLength_ShouldSucceed()
        {
            var exactMinName = "ab";
            var result = Users.Create(
                _validId, ValidLogin, exactMinName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(exactMinName, result.Value.Name);
        }

        [TestMethod]
        public void Create_WhenNameIsTooLong_ShouldReturnFailure()
        {
            var longName = new string('a', Users.MAX_LENGTH_NAME + 1);
            var result = Users.Create(
                _validId, ValidLogin, longName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Name не должно превышать {Users.MAX_LENGTH_NAME} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenNameIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxName = new string('a', Users.MAX_LENGTH_NAME);
            var result = Users.Create(
                _validId, ValidLogin, exactMaxName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenNameHasSpaces_ShouldSucceed()
        {
            var nameWithSpaces = "John Doe Smith";
            var result = Users.Create(
                _validId, ValidLogin, nameWithSpaces, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Password

        [TestMethod]
        public void Create_WhenPasswordIsEmpty_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, "",
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Password не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenPasswordIsTooShort_ShouldReturnFailure()
        {
            var shortPassword = "abc";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, shortPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Password должно содержать минимум 6 символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenPasswordIsExactlyMinLengthWithRequirements_ShouldSucceed()
        {
            var exactMinPassword = "Pass12";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, exactMinPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPasswordIsTooLong_ShouldReturnFailure()
        {
            var longPassword = new string('a', Users.MAX_LENGTH_PASSWORD + 1);
            var result = Users.Create(
                _validId, ValidLogin, ValidName, longPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Password не должно превышать 100 символов", result.Error);
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
        public void Create_WhenPasswordHasUppercaseAndDigit_ShouldSucceed()
        {
            var validPassword = "Password123";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, validPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPasswordHasMultipleUppercaseAndDigits_ShouldSucceed()
        {
            var complexPassword = "MySecureP@ssw0rd123";
            var result = Users.Create(
                _validId, ValidLogin, ValidName, complexPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Test for Role

        [TestMethod]
        public void Create_WhenRoleIsEmpty_ShouldReturnFailure()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                "", ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Role не должно быть пустым", result.Error);
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
        [DataRow("ADMIN")]
        [DataRow("ACTOR")]
        [DataRow("STUDENT")]
        [DataRow("Admin")]
        [DataRow("Actor")]
        [DataRow("Student")]
        public void Create_WhenRoleIsValid_ShouldSucceed(string role)
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                role, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Balance

        [TestMethod]
        public void Create_WhenBalanceIsNegative_ShouldReturnFailure()
        {
            var negativeBalance = -100;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, negativeBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Balance не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenBalanceIsZero_ShouldSucceed()
        {
            var zeroBalance = 0;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, zeroBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(zeroBalance, result.Value.Balance);
        }

        [TestMethod]
        public void Create_WhenBalanceIsPositive_ShouldSucceed()
        {
            var positiveBalance = 1000.50m;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, positiveBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(positiveBalance, result.Value.Balance);
        }

        [TestMethod]
        public void Create_WhenBalanceHasDecimals_ShouldSucceed()
        {
            var balanceWithDecimals = 99.99m;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, balanceWithDecimals, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(balanceWithDecimals, result.Value.Balance);
        }

        [TestMethod]
        public void Create_WhenBalanceIsLargeDecimal_ShouldSucceed()
        {
            var largeBalance = 9999999999.99m;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, largeBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(largeBalance, result.Value.Balance);
        }

        #endregion

        #region Tests for CreatedAt

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
        public void Create_WhenCreatedAtIsNow_ShouldSucceed()
        {
            var now = DateTime.UtcNow;
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, now);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddDays(-30);
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, pastDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(pastDate, result.Value.CreatedAt);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsOneMillisecondInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddMilliseconds(-1);
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, pastDate);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Complex tests

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
            Assert.AreEqual(_validCreatedAt, result.Value.CreatedAt);
            Assert.IsNotNull(result.Value.HashPassword);
            Assert.AreNotEqual(ValidPassword, result.Value.HashPassword);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var minLogin = "abc";
            var minName = "ab";
            var minPassword = "Pass12";
            var result = Users.Create(
                _validId, minLogin, minName, minPassword,
                "student", 0, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(minLogin, result.Value.Login);
            Assert.AreEqual(minName, result.Value.Name);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var maxLogin = new string('a', Users.MAX_LENGTH_LOGIN);
            var maxName = new string('a', Users.MAX_LENGTH_NAME);
            var maxPassword = new string('a', Users.MAX_LENGTH_PASSWORD - 3) + "A1";
            var result = Users.Create(
                _validId, maxLogin, maxName, maxPassword,
                "admin", 1000000, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenMultipleFieldsAreInvalid_ShouldReturnFirstError()
        {
            var emptyId = Guid.Empty;
            var emptyLogin = "";
            var result = Users.Create(
                emptyId, emptyLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_ShouldHashPasswordCorrectly()
        {
            var result = Users.Create(
                _validId, ValidLogin, ValidName, ValidPassword,
                ValidRole, ValidBalance, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreNotEqual(ValidPassword, result.Value.HashPassword);
            Assert.IsTrue(Users.VerifyPassword(ValidPassword, result.Value.HashPassword));
        }

        #endregion

        #region Tests for VerifyPassword

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

        [TestMethod]
        public void VerifyPassword_WhenPasswordIsEmpty_ShouldReturnFalse()
        {
            var password = "TestPassword123";
            var emptyPassword = "";
            var hashPassword = Users.PasswordHasherService.HashBCrypt(password);
            var result = Users.VerifyPassword(emptyPassword, hashPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyPassword_WhenHashPasswordIsEmpty_ShouldReturnFalse()
        {
            var password = "TestPassword123";
            var emptyHash = "";
            var result = Users.VerifyPassword(password, emptyHash);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyPassword_WhenBothAreEmpty_ShouldReturnFalse()
        {
            var result = Users.VerifyPassword("", "");
            Assert.IsFalse(result);
        }

        #endregion
    }
}
