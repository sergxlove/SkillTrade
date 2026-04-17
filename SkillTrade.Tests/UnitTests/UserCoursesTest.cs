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

        #region Tests for Id

        [TestMethod]
        public void Create_WhenIdIsEmpty_ShouldReturnFailure()
        {
            var emptyId = Guid.Empty;
            var result = UserCourses.Create(
                emptyId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenIdIsValid_ShouldNotReturnIdError()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for UserId

        [TestMethod]
        public void Create_WhenUserIdIsEmpty_ShouldReturnFailure()
        {
            var emptyUserId = Guid.Empty;
            var result = UserCourses.Create(
                _validId, emptyUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле UserId не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenUserIdIsValid_ShouldNotReturnUserIdError()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for CourseId

        [TestMethod]
        public void Create_WhenCourseIdIsEmpty_ShouldReturnFailure()
        {
            var emptyCourseId = Guid.Empty;
            var result = UserCourses.Create(
                _validId, _validUserId, emptyCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CourseId не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_WhenCourseIdIsValid_ShouldNotReturnCourseIdError()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Tests for CurrentProgress

        [TestMethod]
        public void Create_WhenCurrentProgressIsNegative_ShouldReturnFailure()
        {
            var negativeProgress = -1;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                negativeProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CurrentProgress не должно быть отрицательным", result.Error);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsZero_ShouldSucceed()
        {
            var zeroProgress = 0;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                zeroProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(zeroProgress, result.Value.CurrentProgress);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsEqualToTotalProgress_ShouldSucceed()
        {
            var completedProgress = ValidTotalProgress;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                completedProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(completedProgress, result.Value.CurrentProgress);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsGreaterThanTotalProgress_ShouldReturnFailure()
        {
            var tooHighProgress = ValidTotalProgress + 1;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                tooHighProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле CurrentProgress не может быть больше TotalProgress", result.Error);
        }

        [TestMethod]
        public void Create_WhenCurrentProgressIsValid_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                10, 20, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(10, result.Value.CurrentProgress);
        }

        #endregion

        #region Tests for TotalProgress

        [TestMethod]
        public void Create_WhenTotalProgressIsZero_ShouldReturnFailure()
        {
            var zeroTotalProgress = 0;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, zeroTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле TotalProgress должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsNegative_ShouldReturnFailure()
        {
            var negativeTotalProgress = -5;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, negativeTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле TotalProgress должно быть больше 0", result.Error);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsOne_ShouldSucceed()
        {
            var minTotalProgress = 1;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                0, minTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(minTotalProgress, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsTooHigh_ShouldReturnFailure()
        {
            var tooHighTotalProgress = 10001;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, tooHighTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле TotalProgress не должно превышать 10000", result.Error);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsExactlyMax_ShouldSucceed()
        {
            var maxTotalProgress = 10000;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                5000, maxTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(maxTotalProgress, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WhenTotalProgressIsValid_ShouldSucceed()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ValidTotalProgress, result.Value.TotalProgress);
        }

        #endregion

        #region Tests for SubscribeTime

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
        public void Create_WhenSubscribeTimeIsNow_ShouldSucceed()
        {
            var now = DateTime.UtcNow;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, now);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenSubscribeTimeIsInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddDays(-30);
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, pastDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(pastDate, result.Value.SubscribeTime);
        }

        [TestMethod]
        public void Create_WhenSubscribeTimeIsDefault_ShouldSucceed()
        {
            var defaultDate = default(DateTime);
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, defaultDate);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(defaultDate, result.Value.SubscribeTime);
        }

        [TestMethod]
        public void Create_WhenSubscribeTimeIsOneMillisecondInPast_ShouldSucceed()
        {
            var pastDate = DateTime.UtcNow.AddMilliseconds(-1);
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                ValidCurrentProgress, ValidTotalProgress, pastDate);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion

        #region Complex test

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
        public void Create_WithMinimalValidValues_ShouldSucceed()
        {
            var minTotalProgress = 1;
            var minCurrentProgress = 0;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                minCurrentProgress, minTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(minCurrentProgress, result.Value.CurrentProgress);
            Assert.AreEqual(minTotalProgress, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WithMaxValidValues_ShouldSucceed()
        {
            var maxTotalProgress = 10000;
            var maxCurrentProgress = 10000;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                maxCurrentProgress, maxTotalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(maxCurrentProgress, result.Value.CurrentProgress);
            Assert.AreEqual(maxTotalProgress, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WhenCourseIsFullyCompleted_ShouldSucceed()
        {
            var completedProgress = 50;
            var totalProgress = 50;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                completedProgress, totalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(completedProgress, result.Value.CurrentProgress);
            Assert.AreEqual(totalProgress, result.Value.TotalProgress);
        }

        [TestMethod]
        public void Create_WhenCourseHasNotStarted_ShouldSucceed()
        {
            var zeroProgress = 0;
            var totalProgress = 100;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                zeroProgress, totalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(zeroProgress, result.Value.CurrentProgress);
        }

        [TestMethod]
        public void Create_WhenMultipleFieldsAreInvalid_ShouldReturnFirstError()
        {
            var emptyId = Guid.Empty;
            var negativeProgress = -1;
            var result = UserCourses.Create(
                emptyId, _validUserId, _validCourseId,
                negativeProgress, ValidTotalProgress, _validSubscribeTime);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Поле Id не должно быть пустым", result.Error);
        }

        [TestMethod]
        public void Create_ShouldSetCorrectPropertyValues()
        {
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                7, 25, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            var userCourse = result.Value;
            Assert.AreEqual(_validId, userCourse.Id);
            Assert.AreEqual(_validUserId, userCourse.UserId);
            Assert.AreEqual(_validCourseId, userCourse.CourseId);
            Assert.AreEqual(7, userCourse.CurrentProgress);
            Assert.AreEqual(25, userCourse.TotalProgress);
            Assert.AreEqual(_validSubscribeTime, userCourse.SubscribeTime);
        }

        #endregion

        #region Tests for progres

        [TestMethod]
        public void Create_WhenProgressIsHalfOfTotal_ShouldSucceed()
        {
            var halfProgress = 50;
            var totalProgress = 100;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                halfProgress, totalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(50, result.Value.CurrentProgress);
        }

        [TestMethod]
        public void Create_WhenProgressIsQuarterOfTotal_ShouldSucceed()
        {
            var quarterProgress = 25;
            var totalProgress = 100;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                quarterProgress, totalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Create_WhenProgressIsThreeQuartersOfTotal_ShouldSucceed()
        {
            var threeQuartersProgress = 75;
            var totalProgress = 100;
            var result = UserCourses.Create(
                _validId, _validUserId, _validCourseId,
                threeQuartersProgress, totalProgress, _validSubscribeTime);
            Assert.IsTrue(result.IsSuccess);
        }

        #endregion
    }
}
