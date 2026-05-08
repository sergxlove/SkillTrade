using SkillTrade.Core.Models;

namespace SkillTrade.Tests.UnitTests
{
    [TestClass]
    public class CoursesTest
    {
        private Guid _validId;
        private Guid _validIdActor;
        private DateTime _validCreatedAt;
        private const string ValidTitle = "Полный курс по C#";
        private const string ValidDescription = "Это подробный курс по C# с нуля до профессионала, включающий все необходимые темы для разработки";
        private const string ValidLevel = "intermediate";
        private const decimal ValidPrice = 4990;
        private const int ValidLessonsCount = 50;
        private const int ValidDurationHours = 100;

        [TestInitialize]
        public void Setup()
        {
            _validId = Guid.NewGuid();
            _validIdActor = Guid.NewGuid();
            _validCreatedAt = DateTime.UtcNow;
        }

        [TestMethod]
        public void Create_WhenAllFieldsAreValid_ShouldReturnSuccess()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(_validId, result.Value.Id);
            Assert.AreEqual(_validIdActor, result.Value.IdActor);
            Assert.AreEqual(ValidTitle, result.Value.Title);
            Assert.AreEqual(ValidDescription, result.Value.Description);
            Assert.AreEqual(ValidLevel, result.Value.Level);
            Assert.AreEqual(ValidPrice, result.Value.Price);
            Assert.AreEqual(ValidLessonsCount, result.Value.LessonsCount);
            Assert.AreEqual(ValidDurationHours, result.Value.DurationTimeHours);
        }

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var result = Courses.Create(
                Guid.Empty, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdActorIsEmpty_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, Guid.Empty, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле IdActor не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsInvalid_ShouldReturnFailure()
        {
            var shortTitle = "ab";
            var result = Courses.Create(
                _validId, _validIdActor, shortTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Title должно содержать минимум {Courses.MIN_LENGTH_TITLE} символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenDescriptionIsInvalid_ShouldReturnFailure()
        {
            var shortDescription = "123456789";
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, shortDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Description должно содержать минимум {Courses.MIN_LENGTH_DESCRIPTION} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenLevelIsEmpty_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                "", ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Level не должно быть пустым", result.Error);
        }

        [TestMethod]
        [DataRow("beginner")]
        [DataRow("intermediate")]
        [DataRow("advanced")]
        [DataRow("expert")]
        [DataRow("all")]
        public void Create_WhenLevelIsValid_ShouldSucceed(string level)
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                level, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPriceIsNegative_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, -100, ValidLessonsCount, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Price не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenLessonsCountIsZero_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, 0, ValidDurationHours, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле LessonsCount должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenDurationTimeHoursIsZero_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, 0, _validCreatedAt);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле DurationTimeHours должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsInFuture_ShouldReturnFailure()
        {
            var futureDate = DateTime.UtcNow.AddDays(1);
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, futureDate);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CreatedAt не может быть в будущем", result.Error);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var minTitle = "abc";
            var minDescription = new string('a', Courses.MIN_LENGTH_DESCRIPTION);
            var result = Courses.Create(
                _validId, _validIdActor, minTitle, minDescription,
                "beginner", 0, 1, 1, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var maxTitle = new string('a', Courses.MAX_LENGTH_TITLE);
            var maxDescription = new string('a', Courses.MAX_LENGTH_DESCRIPTION);
            var result = Courses.Create(
                _validId, _validIdActor, maxTitle, maxDescription,
                "expert", Courses.MAX_PRICE, Courses.MAX_LESSONS_COUNT, Courses.MAX_DURATION_HOURS, _validCreatedAt);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}