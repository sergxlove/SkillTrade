using SkillTrade.Core.Models;

namespace SkillTrade.Tests.UnitTests
{
    [TestClass]
    public class UserCoursesTest
    {
        private Guid _validId;
        private Guid _validUserId;
        private Guid _validCourseId;
        private DateTime _validSubscribeTime;
        private const int ValidCurrentProgress = 5;
        private const int ValidTotalProgress = 20;

        [TestInitialize]
        public void Setup()
        {
            _validId = Guid.NewGuid();
            _validUserId = Guid.NewGuid();
            _validCourseId = Guid.NewGuid();
            _validSubscribeTime = DateTime.UtcNow;
        }

        [TestMethod]
        public void Create_WhenAllFieldsAreValid_ShouldReturnSuccess()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(_validId, result.Value.Id);
            Assert.AreEqual(_validUserId, result.Value.UserId);
            Assert.AreEqual(_validCourseId, result.Value.CourseId);
            Assert.AreEqual(ValidCurrentProgress, result.Value.CurrentProgress);
            Assert.AreEqual(ValidTotalProgress, result.Value.TotalProgress);
            Assert.AreEqual(_validSubscribeTime, result.Value.SubscribeTime);
        }

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                Guid.Empty, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenUserIdIsEmpty_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, Guid.Empty, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле UserId не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenCourseIdIsEmpty_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, Guid.Empty,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CourseId не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsNegative_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                -1, ValidTotalProgress, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CurrentProgress не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsGreaterThanTotalProgress_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidTotalProgress + 1, ValidTotalProgress, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CurrentProgress не может быть больше TotalProgress", result.Error);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsZero_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, 0, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsTooHigh_ShouldReturnFailure()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, 10001, _validSubscribeTime);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле TotalProgress не должно превышать 10000", result.Error);
        }

        [TestMethod]
        public void Create_WhenSubscribeTimeIsInFuture_ShouldReturnFailure()
        {
            var futureDate = DateTime.UtcNow.AddDays(1);
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, futureDate);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле SubscribeTime не может быть в будущем", result.Error);
        }

        [TestMethod]
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                0, 1, _validSubscribeTime);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, result.Value.CurrentProgress);
            Assert.AreEqual(1, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                10000, 10000, _validSubscribeTime);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10000, result.Value.CurrentProgress);
            Assert.AreEqual(10000, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsZero_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                0, ValidTotalProgress, _validSubscribeTime);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, result.Value.CurrentProgress);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressEqualsTotalProgress_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidTotalProgress, ValidTotalProgress, _validSubscribeTime);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ValidTotalProgress, result.Value.CurrentProgress);
        }
    }
}