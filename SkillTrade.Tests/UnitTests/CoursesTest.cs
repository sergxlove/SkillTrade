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

        #region Tests for Id

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var emptyId = Guid.Empty;
            var result = Courses.Create(
                emptyId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdIsValid_ShouldNotReturnIdError()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreNotEqual("Поле Id не должно быть пустым", result.Error);
        }

        #endregion

        #region Tests for IdActor

        [TestMethod]
        public void Create_WhenIdActorIsEmpty_ShouldReturnFailure()
        {
            var emptyIdActor = Guid.Empty;
            var result = Courses.Create(
                _validId, emptyIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле IdActor не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdActorIsValid_ShouldNotReturnIdActorError()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }
        #endregion

        #region Tests for Title

        [TestMethod]
        public void Create_WhenTitleIsEmpty_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, "", ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Title не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsWhitespace_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, "   ", ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Title не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsTooShort_ShouldReturnFailure()
        {
            var shortTitle = "ab";
            var result = Courses.Create(
                _validId, _validIdActor, shortTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Title должно содержать минимум 3 символа", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsExactlyMinLength_ShouldSucceed()
        {
            var exactMinTitle = "abc";
            var result = Courses.Create(
                _validId, _validIdActor, exactMinTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenTitleIsTooLong_ShouldReturnFailure()
        {
            var longTitle = new string('a', Courses.MAX_LENGTH_TITLE + 1);
            var result = Courses.Create(
                _validId, _validIdActor, longTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Title не должно превышать {Courses.MAX_LENGTH_TITLE} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenTitleIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxTitle = new string('a', Courses.MAX_LENGTH_TITLE);
            var result = Courses.Create(
                _validId, _validIdActor, exactMaxTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Description

        [TestMethod]
        public void Create_WhenDescriptionIsEmpty_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, "",
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Description не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenDescriptionIsTooShort_ShouldReturnFailure()
        {
            var shortDescription = "123456789";
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, shortDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Description должно содержать минимум {Courses.MIN_LENGTH_DESCRIPTION} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenDescriptionIsExactlyMinLength_ShouldSucceed()
        {
            var exactMinDescription = new string('a', Courses.MIN_LENGTH_DESCRIPTION);
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, exactMinDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenDescriptionIsTooLong_ShouldReturnFailure()
        {
            var longDescription = new string('a', Courses.MAX_LENGTH_DESCRIPTION + 1);
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, longDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Description не должно превышать {Courses.MAX_LENGTH_DESCRIPTION} символов", result.Error);
        }

        [TestMethod]
        public void Create_WhenDescriptionIsExactlyMaxLength_ShouldSucceed()
        {
            var exactMaxDescription = new string('a', Courses.MAX_LENGTH_DESCRIPTION);
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, exactMaxDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for Level

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
        public void Create_WhenLevelIsWhitespace_ShouldReturnFailure()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                "   ", ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
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

        #endregion

        #region Тесты для Price

        [TestMethod]
        public void Create_WhenPriceIsNegative_ShouldReturnFailure()
        {
            var negativePrice = -100;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, negativePrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Price не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenPriceIsZero_ShouldSucceed()
        {
            var zeroPrice = 0;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, zeroPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPriceIsTooHigh_ShouldReturnFailure()
        {
            var highPrice = Courses.MAX_PRICE + 1;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, highPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле Price не должно превышать {Courses.MAX_PRICE}", result.Error);
        }

        [TestMethod]
        public void Create_WhenPriceIsExactlyMax_ShouldSucceed()
        {
            var maxPrice = Courses.MAX_PRICE;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, maxPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenPriceHasDecimals_ShouldSucceed()
        {
            var priceWithDecimals = 4990.99m;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, priceWithDecimals, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(priceWithDecimals, result.Value.Price);
        }

        #endregion

        #region Tests for LessonsCount

        [TestMethod]
        public void Create_WhenLessonsCountIsZero_ShouldReturnFailure()
        {
            var zeroLessons = 0;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, zeroLessons, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле LessonsCount должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenLessonsCountIsNegative_ShouldReturnFailure()
        {
            var negativeLessons = -5;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, negativeLessons, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле LessonsCount должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenLessonsCountIsTooHigh_ShouldReturnFailure()
        {
            var highLessonsCount = Courses.MAX_LESSONS_COUNT + 1;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, highLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле LessonsCount не должно превышать {Courses.MAX_LESSONS_COUNT}", result.Error);
        }

        [TestMethod]
        public void Create_WhenLessonsCountIsExactlyMax_ShouldSucceed()
        {
            var maxLessonsCount = Courses.MAX_LESSONS_COUNT;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, maxLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(maxLessonsCount, result.Value.LessonsCount);
        }

        #endregion

        #region Тесты для DurationTimeHours

        [TestMethod]
        public void Create_WhenDurationTimeHoursIsZero_ShouldReturnFailure()
        {
            var zeroDuration = 0;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, zeroDuration, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле DurationTimeHours должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenDurationTimeHoursIsNegative_ShouldReturnFailure()
        {
            var negativeDuration = -10;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, negativeDuration, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле DurationTimeHours должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenDurationTimeHoursIsTooHigh_ShouldReturnFailure()
        {
            var highDuration = Courses.MAX_DURATION_HOURS + 1;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, highDuration, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Поле DurationTimeHours не должно превышать {Courses.MAX_DURATION_HOURS} часов", result.Error);
        }

        [TestMethod]
        public void Create_WhenDurationTimeHoursIsExactlyMax_ShouldSucceed()
        {
            var maxDuration = Courses.MAX_DURATION_HOURS;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, maxDuration, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(maxDuration, result.Value.DurationTimeHours);
        }

        #endregion

        #region Tests for CreatedAt

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
        public void Create_WhenCreatedAtIsNow_ShouldSucceed()
        {
            var now = DateTime.UtcNow;
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, now);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenCreatedAtIsInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddDays(-30);
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, pastDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(pastDate, result.Value.CreatedAt);
        }

        #endregion

        #region Complex tests

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
            Assert.AreEqual(_validCreatedAt, result.Value.CreatedAt);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var minTitle = "abc";
            var minDescription = new string('a', Courses.MIN_LENGTH_DESCRIPTION);
            var minPrice = 0;
            var minLessonsCount = 1;
            var minDurationHours = 1;
            var result = Courses.Create(
                _validId, _validIdActor, minTitle, minDescription,
                "beginner", minPrice, minLessonsCount, minDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var maxTitle = new string('a', Courses.MAX_LENGTH_TITLE);
            var maxDescription = new string('a', Courses.MAX_LENGTH_DESCRIPTION);
            var maxPrice = Courses.MAX_PRICE;
            var maxLessonsCount = Courses.MAX_LESSONS_COUNT;
            var maxDurationHours = Courses.MAX_DURATION_HOURS;
            var result = Courses.Create(
                _validId, _validIdActor, maxTitle, maxDescription,
                "expert", maxPrice, maxLessonsCount, maxDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenMultipleFieldsAreInvalid_ShouldReturnFirstError()
        {
            var emptyId = Guid.Empty;
            var emptyTitle = "";
            var result = Courses.Create(
                emptyId, _validIdActor, emptyTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_ShouldSetCorrectPropertyValues()
        {
            var result = Courses.Create(
                _validId, _validIdActor, ValidTitle, ValidDescription,
                ValidLevel, ValidPrice, ValidLessonsCount, ValidDurationHours, _validCreatedAt);
            Assert.IsTrue(result.IsSuccess);
            var course = result.Value;
            Assert.AreEqual(_validId, course.Id);
            Assert.AreEqual(_validIdActor, course.IdActor);
            Assert.AreEqual(ValidTitle, course.Title);
            Assert.AreEqual(ValidDescription, course.Description);
            Assert.AreEqual(ValidLevel, course.Level);
            Assert.AreEqual(ValidPrice, course.Price);
            Assert.AreEqual(ValidLessonsCount, course.LessonsCount);
            Assert.AreEqual(ValidDurationHours, course.DurationTimeHours);
            Assert.AreEqual(_validCreatedAt, course.CreatedAt);
        }

        #endregion
    }
}
