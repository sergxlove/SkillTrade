using SkillTrade.Core.Models;

namespace SkillTrade.Tests.UnitTests
{
    [TestClass]
    public class LessonsTest
    {
        private Guid _validId;
        private Guid _validIdCourse;
        private DateTime _validCreatedAt;
        private const string ValidTitle = "Async/await в C#";
        private const string ValidContent = "Это подробный урок по асинхронному программированию в C# с примерами и объяснениями";

        [TestInitialize]
        public void Setup()
        {
            _validId = Guid.NewGuid();
            _validIdCourse = Guid.NewGuid();
            _validCreatedAt = DateTime.UtcNow;
        }

        #region Tests for Id

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var emptyId = Guid.Empty;
            var result = Lessons.Create(
                emptyId, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdIsValid_ShouldNotReturnIdError()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for IdCourse

        [TestMethod]
        public void Create_WhenIdCourseIsEmpty_ShouldReturnFailure()
        {
            var emptyIdCourse = Guid.Empty;
            var result = Lessons.Create(
                _validId, emptyIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле IdCourse не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdCourseIsValid_ShouldNotReturnIdCourseError()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Title

        [TestMethod]
        public void Create_WhenTitleIsEmpty_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, "", ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Title не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsWhitespace_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, "   ", ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Title не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsTooShort_ShouldReturnFailure()
        {
            var shortTitle = "ab";
            var result = Lessons.Create(
                _validId, _validIdCourse, shortTitle, ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Title должно содержать минимум {Lessons.MIN_LENGTH_TITLE} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsExactlyMinLength_ShouldSucceed()
        {
            var exactMinTitle = "abc";
            var result = Lessons.Create(
                _validId, _validIdCourse, exactMinTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(exactMinTitle, result.Value.Title);
        }

        [TestMethod]
        public void Create_WhenTitleIsTooLong_ShouldReturnFailure()
        {
            var longTitle = new string('a', Lessons.MAX_LENGTH_TITLE + 1);
            var result = Lessons.Create(
                _validId, _validIdCourse, longTitle, ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Title не должно превышать {Lessons.MAX_LENGTH_TITLE} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxTitle = new string('a', Lessons.MAX_LENGTH_TITLE);
            var result = Lessons.Create(
                _validId, _validIdCourse, exactMaxTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(exactMaxTitle, result.Value.Title);
        }

        [TestMethod]
        public void Create_WhenTitleHasSpecialCharacters_ShouldSucceed()
        {
            var titleWithSpecialChars = "C# Async Await - Урок №1";
            var result = Lessons.Create(
                _validId, _validIdCourse, titleWithSpecialChars, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(titleWithSpecialChars, result.Value.Title);
        }

        #endregion

        #region Tests for Content

        [TestMethod]
        public void Create_WhenContentIsEmpty_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, "", _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Content не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenContentIsWhitespace_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, "   ", _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Content не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenContentIsTooLong_ShouldReturnFailure()
        {
            var longContent = new string('a', Lessons.MAX_LENGTH_CONTENT + 1);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, longContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Content не должно превышать {Lessons.MAX_LENGTH_CONTENT} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenContentIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxContent = new string('a', Lessons.MAX_LENGTH_CONTENT);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, exactMaxContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(exactMaxContent, result.Value.Content);
        }

        [TestMethod]
        public void Create_WhenContentIsPlainText_ShouldSucceed()
        {
            var plainTextContent = "Это обычный текстовый урок без какой-либо разметки. Просто описание темы.";
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, plainTextContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(plainTextContent, result.Value.Content);
        }

        [TestMethod]
        public void Create_WhenContentHasMinimalLength_ShouldSucceed()
        {
            var minimalContent = "1234567890";
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, minimalContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for CreatedAt

        [TestMethod]
        public void Create_WhenCreatedAtIsInFuture_ShouldReturnFailure()
        {
            var futureDate = DateTime.UtcNow.AddDays(1);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, futureDate);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CreatedAt не может быть в будущем", result.Error);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsNow_ShouldSucceed()
        {
            var now = DateTime.UtcNow;
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, now);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddDays(-30);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, pastDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(pastDate, result.Value.CreatedAt);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsDefault_ShouldSucceed()
        {
            var defaultDate = default(DateTime);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, defaultDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(defaultDate, result.Value.CreatedAt);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsOneMillisecondInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddMilliseconds(-1);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, pastDate);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Complex tests

        [TestMethod]
        public void Create_WhenAllFieldsAreValid_ShouldReturnSuccess()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(_validId, result.Value.Id);
            Assert.AreEqual(_validIdCourse, result.Value.IdCourse);
            Assert.AreEqual(ValidTitle, result.Value.Title);
            Assert.AreEqual(ValidContent, result.Value.Content);
            Assert.AreEqual(_validCreatedAt, result.Value.CreatedAt);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var minTitle = "abc";
            var minContent = "1234567890";
            var result = Lessons.Create(
                _validId, _validIdCourse, minTitle, minContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(minTitle, result.Value.Title);
            Assert.AreEqual(minContent, result.Value.Content);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var maxTitle = new string('a', Lessons.MAX_LENGTH_TITLE);
            var maxContent = new string('a', Lessons.MAX_LENGTH_CONTENT);
            var result = Lessons.Create(
                _validId, _validIdCourse, maxTitle, maxContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(maxTitle, result.Value.Title);
            Assert.AreEqual(maxContent, result.Value.Content);
        }

        [TestMethod]
        public void Create_WhenMultipleFieldsAreInvalid_ShouldReturnFirstError()
        {
            var emptyId = Guid.Empty;
            var emptyTitle = "";
            var result = Lessons.Create(
                emptyId, _validIdCourse, emptyTitle, ValidContent, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_ShouldSetCorrectPropertyValues()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            var lesson = result.Value;
            Assert.AreEqual(_validId, lesson.Id);
            Assert.AreEqual(_validIdCourse, lesson.IdCourse);
            Assert.AreEqual(ValidTitle, lesson.Title);
            Assert.AreEqual(ValidContent, lesson.Content);
            Assert.AreEqual(_validCreatedAt, lesson.CreatedAt);
        }

        #endregion
    }
}
