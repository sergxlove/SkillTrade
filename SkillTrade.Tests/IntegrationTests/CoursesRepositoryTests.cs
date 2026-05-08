using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres;
using SkillTrade.DataAccess.Postgres.Repositories;

namespace SkillTrade.Tests.IntegrationTests
{
    [TestClass]
    public class CoursesRepositoryTests
    {
        private SqliteConnection _connection = null!;
        private SkillTradeDbContext _context = null!;
        private CoursesRepository _repository = null!;
        private CancellationToken _cancellationToken;

        public CoursesRepositoryTests()
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

            _repository = new CoursesRepository(_context);
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
        public async Task CreateAsync_ShouldAddCourseToDatabase()
        {
            var course = CreateTestCourse();
            var id = await _repository.CreateAsync(course, _cancellationToken);
            var createdCourse = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(createdCourse);
            Assert.AreEqual(course.Title, createdCourse.Title);
            Assert.AreEqual(course.Price, createdCourse.Price);
            Assert.AreEqual(course.LessonsCount, createdCourse.LessonsCount);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectCourse()
        {
            var course = CreateTestCourse();
            var id = await _repository.CreateAsync(course, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(course.Title, result.Title);
            Assert.AreEqual(course.Description, result.Description);
            Assert.AreEqual(course.Level, result.Level);
            Assert.AreEqual(course.Price, result.Price);
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistentId_ShouldReturnNull()
        {
            var result = await _repository.GetByIdAsync(Guid.NewGuid(), _cancellationToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllCoursesOrderedByCreatedAtDesc()
        {
            var course1 = CreateTestCourse("Course 1", createdAt: DateTime.UtcNow.AddDays(-2));
            var course2 = CreateTestCourse("Course 2", createdAt: DateTime.UtcNow.AddDays(-1));
            var course3 = CreateTestCourse("Course 3", createdAt: DateTime.UtcNow);
            await _repository.CreateAsync(course1, _cancellationToken);
            await _repository.CreateAsync(course2, _cancellationToken);
            await _repository.CreateAsync(course3, _cancellationToken);
            var result = await _repository.GetAllAsync(_cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(3, resultList);
            Assert.AreEqual("Course 3", resultList[0].Title);
            Assert.AreEqual("Course 2", resultList[1].Title);
            Assert.AreEqual("Course 1", resultList[2].Title);
        }

        [TestMethod]
        public async Task GetByActorIdAsync_ShouldReturnCoursesForSpecificActor()
        {
            var actorId = Guid.NewGuid();
            var course1 = CreateTestCourse(actorId: actorId);
            var course2 = CreateTestCourse(actorId: actorId);
            var course3 = CreateTestCourse(actorId: Guid.NewGuid());
            await _repository.CreateAsync(course1, _cancellationToken);
            await _repository.CreateAsync(course2, _cancellationToken);
            await _repository.CreateAsync(course3, _cancellationToken);
            var result = await _repository.GetByActorIdAsync(actorId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(2, resultList);
            Assert.IsTrue(resultList.All(c => c.IdActor == actorId));
        }

        [TestMethod]
        public async Task GetByLevelAsync_ShouldReturnCoursesWithSpecificLevelCaseInsensitive()
        {
            var course1 = CreateTestCourse(level: "Beginner");
            var course2 = CreateTestCourse(level: "Intermediate");
            var course3 = CreateTestCourse(level: "BEGINNER");
            await _repository.CreateAsync(course1, _cancellationToken);
            await _repository.CreateAsync(course2, _cancellationToken);
            await _repository.CreateAsync(course3, _cancellationToken);
            var result = await _repository.GetByLevelAsync("beginner", _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(2, resultList);
            Assert.IsTrue(resultList.All(c => c.Level.ToLower() == "beginner"));
        }

        [TestMethod]
        public async Task SearchByTitleAsync_ShouldReturnMatchingCoursesCaseInsensitive()
        {
            var course1 = CreateTestCourse(title: "C# Programming Basics");
            var course2 = CreateTestCourse(title: "Advanced C# Techniques");
            var course3 = CreateTestCourse(title: "Python for Beginners");
            var course4 = CreateTestCourse(title: "c# web development");
            await _repository.CreateAsync(course1, _cancellationToken);
            await _repository.CreateAsync(course2, _cancellationToken);
            await _repository.CreateAsync(course3, _cancellationToken);
            await _repository.CreateAsync(course4, _cancellationToken);
            var result = await _repository.SearchByTitleAsync("c#", _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(3, resultList);
            Assert.IsTrue(resultList.All(c => c.Title.ToLower().Contains("c#")));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldModifyExistingCourse()
        {
            var course = CreateTestCourse();
            var id = await _repository.CreateAsync(course, _cancellationToken);
            var updatedCourse = Courses.Create(
                id, course.IdActor, "Updated Title", "Updated Description",
                "Advanced", 199.99m, 15, 20, course.CreatedAt).Value;
            var rowsAffected = await _repository.UpdateAsync(updatedCourse, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Title", result.Title);
            Assert.AreEqual("Updated Description", result.Description);
            Assert.AreEqual("Advanced", result.Level);
            Assert.AreEqual(199.99m, result.Price);
            Assert.AreEqual(15, result.LessonsCount);
            Assert.AreEqual(20.5, result.DurationTimeHours);
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentCourse_ShouldReturnZero()
        {
            var nonExistentCourse = CreateTestCourse();
            var rowsAffected = await _repository.UpdateAsync(nonExistentCourse, _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveCourseFromDatabase()
        {
            var course = CreateTestCourse();
            var id = await _repository.CreateAsync(course, _cancellationToken);
            var rowsAffected = await _repository.DeleteAsync(id, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var deletedCourse = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNull(deletedCourse);
        }

        [TestMethod]
        public async Task DeleteAsync_NonExistentCourse_ShouldReturnZero()
        {
            var rowsAffected = await _repository.DeleteAsync(Guid.NewGuid(), _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task ExistsAsync_ShouldReturnTrueForExistingCourse()
        {
            var course = CreateTestCourse();
            var id = await _repository.CreateAsync(course, _cancellationToken);
            var exists = await _repository.ExistsAsync(id, _cancellationToken);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task ExistsAsync_ShouldReturnFalseForNonExistingCourse()
        {
            var exists = await _repository.ExistsAsync(Guid.NewGuid(), _cancellationToken);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task GetPagedAsync_ShouldReturnCorrectPageWithOrdering()
        {
            for (int i = 1; i <= 10; i++)
            {
                var course = CreateTestCourse($"Course {i}", createdAt: DateTime.UtcNow.AddSeconds(i));
                await _repository.CreateAsync(course, _cancellationToken);
            }
            var page1 = await _repository.GetPagedAsync(1, 3, _cancellationToken);
            var page2 = await _repository.GetPagedAsync(2, 3, _cancellationToken);
            var page4 = await _repository.GetPagedAsync(4, 3, _cancellationToken);
            Assert.AreEqual(3, page1.Count());
            Assert.AreEqual(3, page2.Count());
            Assert.AreEqual(1, page4.Count());
            Assert.AreNotEqual(page1.First().Title, page2.First().Title);
            var page1Ids = page1.Select(c => c.Id);
            var page2Ids = page2.Select(c => c.Id);
            Assert.IsFalse(page1Ids.Intersect(page2Ids).Any());
        }

        [TestMethod]
        public async Task GetPagedAsync_WithInvalidPage_ShouldReturnEmptyOrCorrectResult()
        {
            var course = CreateTestCourse();
            await _repository.CreateAsync(course, _cancellationToken);
            var page0 = await _repository.GetPagedAsync(0, 10, _cancellationToken);
            var page999 = await _repository.GetPagedAsync(999, 10, _cancellationToken);
            Assert.AreEqual(1, page0.Count());
            Assert.AreEqual(0, page999.Count());
        }

        [TestMethod]
        public async Task CreateAndGet_ShouldHandleMultipleCoursesCorrectly()
        {
            var courses = new[]
            {
                CreateTestCourse("Course A", "Beginner", 49.99m),
                CreateTestCourse("Course B", "Intermediate", 99.99m),
                CreateTestCourse("Course C", "Advanced", 149.99m)
            };
            foreach (var course in courses)
            {
                await _repository.CreateAsync(course, _cancellationToken);
            }
            var allCourses = await _repository.GetAllAsync(_cancellationToken);
            Assert.AreEqual(3, allCourses.Count());
            var beginnerCourses = await _repository.GetByLevelAsync("beginner", _cancellationToken);
            Assert.AreEqual(1, beginnerCourses.Count());
            Assert.AreEqual(49.99m, beginnerCourses.First().Price);
        }

        private Courses CreateTestCourse(
            string title = "Test Course",
            string level = "Beginner",
            decimal price = 99.99m,
            Guid? actorId = null,
            DateTime? createdAt = null)
        {
            return Courses.Create(
                Guid.NewGuid(),
                actorId ?? Guid.NewGuid(),
                title,
                $"Test Description for {title}",
                level,
                price,
                10,
                15,
                createdAt ?? DateTime.UtcNow).Value;
        }
    }
}