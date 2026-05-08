using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres;
using SkillTrade.DataAccess.Postgres.Repositories;

namespace SkillTrade.Tests.IntegrationTests
{
    [TestClass]
    public class UserCoursesRepositoryTests
    {
        private SqliteConnection _connection = null!;
        private SkillTradeDbContext _context = null!;
        private UserCoursesRepository _repository = null!;
        private CancellationToken _cancellationToken;

        public UserCoursesRepositoryTests()
        {
            _cancellationToken = CancellationToken.None;
        }

        [TestInitialize]
        public async Task Setup()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            await _connection.OpenAsync();
            var options = new DbContextOptionsBuilder<SkillTradeDbContext>()
                .UseSqlite(_connection)
                .Options;
            _context = new SkillTradeDbContext(options);
            await _context.Database.EnsureCreatedAsync();
            _repository = new UserCoursesRepository(_context);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldAddUserCourseToDatabase()
        {
            var userCourse = CreateTestUserCourse();
            var id = await _repository.CreateAsync(userCourse, _cancellationToken);
            var createdUserCourse = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(createdUserCourse);
            Assert.AreEqual(userCourse.UserId, createdUserCourse.UserId);
            Assert.AreEqual(userCourse.CourseId, createdUserCourse.CourseId);
            Assert.AreEqual(userCourse.CurrentProgress, createdUserCourse.CurrentProgress);
            Assert.AreEqual(userCourse.TotalProgress, createdUserCourse.TotalProgress);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectUserCourse()
        {
            var userCourse = CreateTestUserCourse();
            var id = await _repository.CreateAsync(userCourse, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(userCourse.UserId, result.UserId);
            Assert.AreEqual(userCourse.CourseId, result.CourseId);
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistentId_ShouldReturnNull()
        {
            var result = await _repository.GetByIdAsync(Guid.NewGuid(), _cancellationToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByUserAndCourseAsync_ShouldReturnCorrectRecord()
        {
            var userId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var userCourse = CreateTestUserCourse(userId: userId, courseId: courseId);
            await _repository.CreateAsync(userCourse, _cancellationToken);
            var result = await _repository.GetByUserAndCourseAsync(userId, courseId, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(courseId, result.CourseId);
        }

        [TestMethod]
        public async Task GetByUserAndCourseAsync_NonExistent_ShouldReturnNull()
        {
            var result = await _repository.GetByUserAndCourseAsync(Guid.NewGuid(), Guid.NewGuid(), _cancellationToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ShouldReturnAllUserCourses()
        {
            var userId = Guid.NewGuid();
            var anotherUserId = Guid.NewGuid();
            var userCourse1 = CreateTestUserCourse(userId: userId, courseId: Guid.NewGuid());
            var userCourse2 = CreateTestUserCourse(userId: userId, courseId: Guid.NewGuid());
            var userCourse3 = CreateTestUserCourse(userId: anotherUserId, courseId: Guid.NewGuid());
            await _repository.CreateAsync(userCourse1, _cancellationToken);
            await _repository.CreateAsync(userCourse2, _cancellationToken);
            await _repository.CreateAsync(userCourse3, _cancellationToken);
            var result = await _repository.GetByUserIdAsync(userId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(2, resultList);
            Assert.IsTrue(resultList.All(uc => uc.UserId == userId));
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ShouldReturnOrderedBySubscribeTimeDesc()
        {
            var userId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var userCourse1 = CreateTestUserCourse(userId: userId, subscribeTime: now.AddDays(-2));
            var userCourse2 = CreateTestUserCourse(userId: userId, subscribeTime: now.AddDays(-1));
            var userCourse3 = CreateTestUserCourse(userId: userId, subscribeTime: now);
            await _repository.CreateAsync(userCourse1, _cancellationToken);
            await _repository.CreateAsync(userCourse2, _cancellationToken);
            await _repository.CreateAsync(userCourse3, _cancellationToken);
            var result = await _repository.GetByUserIdAsync(userId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(3, resultList);
            Assert.AreEqual(userCourse3.SubscribeTime, resultList[0].SubscribeTime); // Самый новый первый
            Assert.AreEqual(userCourse2.SubscribeTime, resultList[1].SubscribeTime);
            Assert.AreEqual(userCourse1.SubscribeTime, resultList[2].SubscribeTime);
        }

        [TestMethod]
        public async Task GetByCourseIdAsync_ShouldReturnAllCourseSubscriptions()
        {
            var courseId = Guid.NewGuid();
            var anotherCourseId = Guid.NewGuid();
            var subscription1 = CreateTestUserCourse(courseId: courseId, userId: Guid.NewGuid());
            var subscription2 = CreateTestUserCourse(courseId: courseId, userId: Guid.NewGuid());
            var subscription3 = CreateTestUserCourse(courseId: anotherCourseId, userId: Guid.NewGuid());
            await _repository.CreateAsync(subscription1, _cancellationToken);
            await _repository.CreateAsync(subscription2, _cancellationToken);
            await _repository.CreateAsync(subscription3, _cancellationToken);
            var result = await _repository.GetByCourseIdAsync(courseId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(2, resultList);
            Assert.IsTrue(resultList.All(uc => uc.CourseId == courseId));
        }

        [TestMethod]
        public async Task GetActiveSubscriptionsByUserIdAsync_ShouldReturnOnlyActiveSubscriptions()
        {
            var userId = Guid.NewGuid();
            var activeSubscription = CreateTestUserCourse(
                userId: userId,
                currentProgress: 5,
                totalProgress: 10);
            var completedSubscription = CreateTestUserCourse(
                userId: userId,
                currentProgress: 10,
                totalProgress: 10);
            var anotherUserSubscription = CreateTestUserCourse(
                userId: Guid.NewGuid(),
                currentProgress: 3,
                totalProgress: 10);
            await _repository.CreateAsync(activeSubscription, _cancellationToken);
            await _repository.CreateAsync(completedSubscription, _cancellationToken);
            await _repository.CreateAsync(anotherUserSubscription, _cancellationToken);
            var result = await _repository.GetActiveSubscriptionsByUserIdAsync(userId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(1, resultList);
            Assert.AreEqual(activeSubscription.Id, resultList[0].Id);
            Assert.IsLessThan(resultList[0].TotalProgress, resultList[0].CurrentProgress);
        }

        [TestMethod]
        public async Task UpdateProgressAsync_ShouldUpdateCurrentProgress()
        {
            var userCourse = CreateTestUserCourse(currentProgress: 0, totalProgress: 10);
            var id = await _repository.CreateAsync(userCourse, _cancellationToken);
            int newProgress = 7;
            var rowsAffected = await _repository.UpdateProgressAsync(id, newProgress, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var updated = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(updated);
            Assert.AreEqual(newProgress, updated.CurrentProgress);
            Assert.AreEqual(10, updated.TotalProgress); 
        }

        [TestMethod]
        public async Task UpdateProgressAsync_NonExistentRecord_ShouldReturnZero()
        {
            var rowsAffected = await _repository.UpdateProgressAsync(Guid.NewGuid(), 5, _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task GetProgressAsync_ShouldReturnCurrentProgress()
        {
            var courseId = Guid.NewGuid();
            var userCourse = CreateTestUserCourse(courseId: courseId, currentProgress: 8);
            await _repository.CreateAsync(userCourse, _cancellationToken);
            var progress = await _repository.GetProgressAsync(courseId, _cancellationToken);
            Assert.AreEqual(8, progress);
        }

        [TestMethod]
        public async Task GetProgressAsync_NonExistentCourse_ShouldReturnZero()
        {
            var progress = await _repository.GetProgressAsync(Guid.NewGuid(), _cancellationToken);
            Assert.AreEqual(0, progress);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveUserCourse()
        {
            var userCourse = CreateTestUserCourse();
            var id = await _repository.CreateAsync(userCourse, _cancellationToken);
            var rowsAffected = await _repository.DeleteAsync(id, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var deleted = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async Task DeleteAsync_NonExistent_ShouldReturnZero()
        {
            var rowsAffected = await _repository.DeleteAsync(Guid.NewGuid(), _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task IsUserSubscribedAsync_ShouldReturnTrueForActiveSubscription()
        {
            var userId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var userCourse = CreateTestUserCourse(userId: userId, courseId: courseId);
            await _repository.CreateAsync(userCourse, _cancellationToken);
            var isSubscribed = await _repository.IsUserSubscribedAsync(userId, courseId, _cancellationToken);
            Assert.IsTrue(isSubscribed);
        }

        [TestMethod]
        public async Task IsUserSubscribedAsync_ShouldReturnFalseForNonExistent()
        {
            var isSubscribed = await _repository.IsUserSubscribedAsync(Guid.NewGuid(), Guid.NewGuid(), _cancellationToken);
            Assert.IsFalse(isSubscribed);
        }

        [TestMethod]
        public async Task GetSubscribersCountAsync_ShouldReturnCorrectCount()
        {
            var courseId = Guid.NewGuid();
            for (int i = 0; i < 5; i++)
            {
                var subscription = CreateTestUserCourse(courseId: courseId);
                await _repository.CreateAsync(subscription, _cancellationToken);
            }
            var otherSubscription = CreateTestUserCourse(courseId: Guid.NewGuid());
            await _repository.CreateAsync(otherSubscription, _cancellationToken);
            var count = await _repository.GetSubscribersCountAsync(courseId, _cancellationToken);
            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public async Task CountStartedCoursesAsync_ShouldReturnCorrectCount()
        {
            var userId = Guid.NewGuid();
            for (int i = 0; i < 3; i++)
            {
                var userCourse = CreateTestUserCourse(userId: userId);
                await _repository.CreateAsync(userCourse, _cancellationToken);
            }
            var otherUserCourse = CreateTestUserCourse(userId: Guid.NewGuid());
            await _repository.CreateAsync(otherUserCourse, _cancellationToken);
            var count = await _repository.CountStartedCoursesAsync(userId, _cancellationToken);
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public async Task CountEndedCoursesAsync_ShouldReturnOnlyCompletedCourses()
        {
            var userId = Guid.NewGuid();
            var completedCourse = CreateTestUserCourse(userId: userId, currentProgress: 10, totalProgress: 10);
            var anotherCompletedCourse = CreateTestUserCourse(userId: userId, currentProgress: 5, totalProgress: 5);
            var inProgressCourse = CreateTestUserCourse(userId: userId, currentProgress: 3, totalProgress: 10);
            await _repository.CreateAsync(completedCourse, _cancellationToken);
            await _repository.CreateAsync(anotherCompletedCourse, _cancellationToken);
            await _repository.CreateAsync(inProgressCourse, _cancellationToken);
            var count = await _repository.CountEndedCoursesAsync(userId, _cancellationToken);
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public async Task CreateDuplicateUserCourse_ShouldSucceed()
        {
            var userId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var userCourse1 = CreateTestUserCourse(userId: userId, courseId: courseId);
            var userCourse2 = CreateTestUserCourse(userId: userId, courseId: courseId);
            var id1 = await _repository.CreateAsync(userCourse1, _cancellationToken);
            var id2 = await _repository.CreateAsync(userCourse2, _cancellationToken);
            Assert.AreNotEqual(id1, id2);
            var subscriptions = await _repository.GetByUserIdAsync(userId, _cancellationToken);
            Assert.AreEqual(2, subscriptions.Count());
        }

        [TestMethod]
        public async Task ComplexScenario_UserProgressTracking()
        {
            var userId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var subscription = CreateTestUserCourse(userId: userId, courseId: courseId, currentProgress: 0, totalProgress: 20);
            var id = await _repository.CreateAsync(subscription, _cancellationToken);
            Assert.IsTrue(await _repository.IsUserSubscribedAsync(userId, courseId, _cancellationToken));
            var initialProgress = await _repository.GetProgressAsync(courseId, _cancellationToken);
            Assert.AreEqual(0, initialProgress);
            await _repository.UpdateProgressAsync(id, 5, _cancellationToken);
            await _repository.UpdateProgressAsync(id, 10, _cancellationToken);
            await _repository.UpdateProgressAsync(id, 15, _cancellationToken);
            var finalProgress = await _repository.GetProgressAsync(courseId, _cancellationToken);
            Assert.AreEqual(15, finalProgress);
            var startedCount = await _repository.CountStartedCoursesAsync(userId, _cancellationToken);
            Assert.AreEqual(1, startedCount);
            var endedCount = await _repository.CountEndedCoursesAsync(userId, _cancellationToken);
            Assert.AreEqual(0, endedCount);
            await _repository.UpdateProgressAsync(id, 20, _cancellationToken);
            var activeSubscriptions = await _repository.GetActiveSubscriptionsByUserIdAsync(userId, _cancellationToken);
            Assert.AreEqual(0, activeSubscriptions.Count());
            endedCount = await _repository.CountEndedCoursesAsync(userId, _cancellationToken);
            Assert.AreEqual(1, endedCount);
        }

        private UserCourses CreateTestUserCourse(
            Guid? userId = null,
            Guid? courseId = null,
            int currentProgress = 0,
            int totalProgress = 10,
            DateTime? subscribeTime = null)
        {
            return UserCourses.Create(
                Guid.NewGuid(),
                userId ?? Guid.NewGuid(),
                courseId ?? Guid.NewGuid(),
                currentProgress,
                totalProgress,
                subscribeTime ?? DateTime.UtcNow).Value;
        }
    }
}
