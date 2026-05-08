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
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                Guid.Empty, _validIdCourse, ValidTitle, ValidContent, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdCourseIsEmpty_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, Guid.Empty, ValidTitle, ValidContent, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле IdCourse не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsInvalid_ShouldReturnFailure()
        {
            var shortTitle = "ab";
            var result = Lessons.Create(
                _validId, _validIdCourse, shortTitle, ValidContent, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Title должно содержать минимум {Lessons.MIN_LENGTH_TITLE} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenContentIsEmpty_ShouldReturnFailure()
        {
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, "", _validCreatedAt);

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
        public void Create_WhenCreatedAtIsInFuture_ShouldReturnFailure()
        {
            var futureDate = DateTime.UtcNow.AddDays(1);
            var result = Lessons.Create(
                _validId, _validIdCourse, ValidTitle, ValidContent, futureDate);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CreatedAt не может быть в будущем", result.Error);
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
        public void Create_WhenTitleHasSpecialCharacters_ShouldSucceed()
        {
            var titleWithSpecialChars = "C# Async Await - Урок №1";
            var result = Lessons.Create(
                _validId, _validIdCourse, titleWithSpecialChars, ValidContent, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(titleWithSpecialChars, result.Value.Title);
        }
    }
}