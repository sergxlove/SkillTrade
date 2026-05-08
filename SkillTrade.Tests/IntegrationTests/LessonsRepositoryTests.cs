using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres;
using SkillTrade.DataAccess.Postgres.Repositories;

namespace SkillTrade.Tests.IntegrationTests
{
    [TestClass]
    public class LessonsRepositoryTests
    {
        private SqliteConnection _connection = null!;
        private SkillTradeDbContext _context = null!;
        private LessonsRepository _repository = null!;
        private CancellationToken _cancellationToken;

        public LessonsRepositoryTests()
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

            _repository = new LessonsRepository(_context);
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
        public async Task CreateAsync_ShouldAddLessonToDatabase()
        {
            var lesson = CreateTestLesson();
            var id = await _repository.CreateAsync(lesson, _cancellationToken);
            var createdLesson = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(createdLesson);
            Assert.AreEqual(lesson.Title, createdLesson.Title);
            Assert.AreEqual(lesson.Content, createdLesson.Content);
            Assert.AreEqual(lesson.IdCourse, createdLesson.IdCourse);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnCorrectLesson()
        {
            var lesson = CreateTestLesson();
            var id = await _repository.CreateAsync(lesson, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(lesson.Title, result.Title);
            Assert.AreEqual(lesson.Content, result.Content);
            Assert.AreEqual(lesson.IdCourse, result.IdCourse);
        }

        [TestMethod]
        public async Task GetByIdAsync_NonExistentId_ShouldReturnNull()
        {
            var result = await _repository.GetByIdAsync(Guid.NewGuid(), _cancellationToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByCourseIdAsync_ShouldReturnLessonsForSpecificCourse()
        {
            var courseId = Guid.NewGuid();
            var anotherCourseId = Guid.NewGuid();
            var lesson1 = CreateTestLesson(courseId: courseId, title: "Lesson 1");
            var lesson2 = CreateTestLesson(courseId: courseId, title: "Lesson 2");
            var lesson3 = CreateTestLesson(courseId: anotherCourseId, title: "Lesson 3");
            await _repository.CreateAsync(lesson1, _cancellationToken);
            await _repository.CreateAsync(lesson2, _cancellationToken);
            await _repository.CreateAsync(lesson3, _cancellationToken);
            var result = await _repository.GetByCourseIdAsync(courseId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(2, resultList);
            Assert.IsTrue(resultList.All(l => l.IdCourse == courseId));
            Assert.IsTrue(resultList.Any(l => l.Title == "Lesson 1"));
            Assert.IsTrue(resultList.Any(l => l.Title == "Lesson 2"));
        }

        [TestMethod]
        public async Task GetByCourseIdAsync_ShouldReturnLessonsOrderedByCreatedAtAsc()
        {
            var courseId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var lesson1 = CreateTestLesson(courseId: courseId, title: "First Lesson", createdAt: now.AddMinutes(-10));
            var lesson2 = CreateTestLesson(courseId: courseId, title: "Second Lesson", createdAt: now.AddMinutes(-5));
            var lesson3 = CreateTestLesson(courseId: courseId, title: "Third Lesson", createdAt: now);
            await _repository.CreateAsync(lesson1, _cancellationToken);
            await _repository.CreateAsync(lesson2, _cancellationToken);
            await _repository.CreateAsync(lesson3, _cancellationToken);
            var result = await _repository.GetByCourseIdAsync(courseId, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(3, resultList);
            Assert.AreEqual("First Lesson", resultList[0].Title);
            Assert.AreEqual("Second Lesson", resultList[1].Title);
            Assert.AreEqual("Third Lesson", resultList[2].Title);
        }

        [TestMethod]
        public async Task GetByCourseIdPagedAsync_ShouldReturnCorrectPage()
        {
            var courseId = Guid.NewGuid();
            for (int i = 1; i <= 10; i++)
            {
                var lesson = CreateTestLesson(courseId: courseId, title: $"Lesson {i}", createdAt: DateTime.UtcNow.AddSeconds(i));
                await _repository.CreateAsync(lesson, _cancellationToken);
            }
            var page1 = await _repository.GetByCourseIdPagedAsync(courseId, 1, 3, _cancellationToken);
            var page2 = await _repository.GetByCourseIdPagedAsync(courseId, 2, 3, _cancellationToken);
            var page4 = await _repository.GetByCourseIdPagedAsync(courseId, 4, 3, _cancellationToken);
            Assert.AreEqual(3, page1.Count());
            Assert.AreEqual(3, page2.Count());
            Assert.AreEqual(1, page4.Count()); 
            var page1Ids = page1.Select(l => l.Id);
            var page2Ids = page2.Select(l => l.Id);
            Assert.IsFalse(page1Ids.Intersect(page2Ids).Any());
        }

        [TestMethod]
        public async Task GetByCourseIdPagedAsync_ShouldReturnEmptyForInvalidPage()
        {
            var courseId = Guid.NewGuid();
            var lesson = CreateTestLesson(courseId: courseId);
            await _repository.CreateAsync(lesson, _cancellationToken);
            var pageNegative = await _repository.GetByCourseIdPagedAsync(courseId, -1, 10, _cancellationToken);
            var pageZero = await _repository.GetByCourseIdPagedAsync(courseId, 0, 10, _cancellationToken);
            var pageTooHigh = await _repository.GetByCourseIdPagedAsync(courseId, 100, 10, _cancellationToken);
            Assert.IsNotNull(pageNegative);
            Assert.IsNotNull(pageZero);
            Assert.IsNotNull(pageTooHigh);
            Assert.AreEqual(0, pageTooHigh.Count());
        }

        [TestMethod]
        public async Task GetByCourseIdPagedAsync_ShouldReturnOrderedLessons()
        {
            var courseId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var lesson1 = CreateTestLesson(courseId: courseId, title: "Z Lesson", createdAt: now.AddMinutes(-30));
            var lesson2 = CreateTestLesson(courseId: courseId, title: "A Lesson", createdAt: now.AddMinutes(-20));
            var lesson3 = CreateTestLesson(courseId: courseId, title: "M Lesson", createdAt: now.AddMinutes(-10));
            await _repository.CreateAsync(lesson1, _cancellationToken);
            await _repository.CreateAsync(lesson2, _cancellationToken);
            await _repository.CreateAsync(lesson3, _cancellationToken);
            var result = await _repository.GetByCourseIdPagedAsync(courseId, 1, 10, _cancellationToken);
            var resultList = result.ToList();
            Assert.HasCount(3, resultList);
            Assert.AreEqual("Z Lesson", resultList[0].Title);
            Assert.AreEqual("A Lesson", resultList[1].Title);
            Assert.AreEqual("M Lesson", resultList[2].Title);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldModifyExistingLesson()
        {
            var lesson = CreateTestLesson();
            var id = await _repository.CreateAsync(lesson, _cancellationToken);
            var updatedLesson = Lessons.Create(
                id, lesson.IdCourse, "Updated Title", "Updated Content", lesson.CreatedAt).Value;
            var rowsAffected = await _repository.UpdateAsync(updatedLesson, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Title", result.Title);
            Assert.AreEqual("Updated Content", result.Content);
            Assert.AreEqual(lesson.IdCourse, result.IdCourse);
        }

        [TestMethod]
        public async Task UpdateAsync_NonExistentLesson_ShouldReturnZero()
        {
            var nonExistentLesson = CreateTestLesson();
            var rowsAffected = await _repository.UpdateAsync(nonExistentLesson, _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveLessonFromDatabase()
        {
            var lesson = CreateTestLesson();
            var id = await _repository.CreateAsync(lesson, _cancellationToken);
            var rowsAffected = await _repository.DeleteAsync(id, _cancellationToken);
            Assert.AreEqual(1, rowsAffected);
            var deletedLesson = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNull(deletedLesson);
        }

        [TestMethod]
        public async Task DeleteAsync_NonExistentLesson_ShouldReturnZero()
        {
            var rowsAffected = await _repository.DeleteAsync(Guid.NewGuid(), _cancellationToken);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public async Task GetLessonsCountByCourseIdAsync_ShouldReturnCorrectCount()
        {
            var courseId1 = Guid.NewGuid();
            var courseId2 = Guid.NewGuid();
            for (int i = 0; i < 5; i++)
            {
                var lesson = CreateTestLesson(courseId: courseId1);
                await _repository.CreateAsync(lesson, _cancellationToken);
            }
            for (int i = 0; i < 3; i++)
            {
                var lesson = CreateTestLesson(courseId: courseId2);
                await _repository.CreateAsync(lesson, _cancellationToken);
            }
            var count1 = await _repository.GetLessonsCountByCourseIdAsync(courseId1, _cancellationToken);
            var count2 = await _repository.GetLessonsCountByCourseIdAsync(courseId2, _cancellationToken);
            var countNonExistent = await _repository.GetLessonsCountByCourseIdAsync(Guid.NewGuid(), _cancellationToken);
            Assert.AreEqual(5, count1);
            Assert.AreEqual(3, count2);
            Assert.AreEqual(0, countNonExistent);
        }

        [TestMethod]
        public async Task ExistsAsync_ShouldReturnTrueForExistingLesson()
        {
            var lesson = CreateTestLesson();
            var id = await _repository.CreateAsync(lesson, _cancellationToken);
            var exists = await _repository.ExistsAsync(id, _cancellationToken);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task ExistsAsync_ShouldReturnFalseForNonExistingLesson()
        {
            var exists = await _repository.ExistsAsync(Guid.NewGuid(), _cancellationToken);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task CreateMultipleLessonsForCourse_ShouldAllBeRetrievable()
        {
            var courseId = Guid.NewGuid();
            var lessons = new[]
            {
                CreateTestLesson(courseId: courseId, title: "Introduction", content: "Welcome to the course"),
                CreateTestLesson(courseId: courseId, title: "Chapter 1", content: "Basic concepts"),
                CreateTestLesson(courseId: courseId, title: "Chapter 2", content: "Advanced topics")
            };
            foreach (var lesson in lessons)
            {
                await _repository.CreateAsync(lesson, _cancellationToken);
            }
            var allLessons = await _repository.GetByCourseIdAsync(courseId, _cancellationToken);
            Assert.AreEqual(3, allLessons.Count());
            var count = await _repository.GetLessonsCountByCourseIdAsync(courseId, _cancellationToken);
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public async Task UpdateLesson_ShouldOnlyModifyTitleAndContent()
        {
            var originalLesson = CreateTestLesson(title: "Original Title", content: "Original Content");
            var id = await _repository.CreateAsync(originalLesson, _cancellationToken);
            var updatedLesson = Lessons.Create(
                id,
                Guid.NewGuid(),
                "New Title",
                "New Content",
                originalLesson.CreatedAt).Value;
            await _repository.UpdateAsync(updatedLesson, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual("New Title", result.Title);
            Assert.AreEqual("New Content", result.Content);
            Assert.AreEqual(originalLesson.IdCourse, result.IdCourse);
        }

        private Lessons CreateTestLesson(
            string title = "Test Lesson",
            string content = "Test Content",
            Guid? courseId = null,
            DateTime? createdAt = null)
        {
            return Lessons.Create(
                Guid.NewGuid(),
                courseId ?? Guid.NewGuid(),
                title,
                content,
                createdAt ?? DateTime.UtcNow).Value;
        }
    }
}
