using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres;
using SkillTrade.DataAccess.Postgres.Repositories;

namespace SkillTrade.Tests.IntegrationTests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private SqliteConnection _connection = null!;
        private SkillTradeDbContext _context = null!;
        private UsersRepository _repository = null!;
        private CancellationToken _cancellationToken;

        public UsersRepositoryTests()
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

            _repository = new UsersRepository(_context);
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
        public async Task CreateAndGetById_ShouldWorkCorrectly()
        {
            var user = CreateTestUser();
            var id = await _repository.CreateAsync(user, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Login, result.Login);
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Balance, result.Balance);
        }

        [TestMethod]
        public async Task GetByLoginAsync_CaseInsensitive_ShouldReturnUser()
        {
            var user = CreateTestUser(login: "TestUser@Example.com");
            await _repository.CreateAsync(user, _cancellationToken);
            var result = await _repository.GetByLoginAsync("testuser@example.com", _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual("TestUser@Example.com", result.Login);
        }

        [TestMethod]
        public async Task UpdateBalanceAsync_ShouldUpdateUserBalance()
        {
            var user = CreateTestUser(balance: 100m);
            var id = await _repository.CreateAsync(user, _cancellationToken);
            await _repository.UpdateBalanceAsync(id, 250.50m, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(250.50m, result.Balance);
        }

        [TestMethod]
        public async Task UpdatePasswordAsync_ShouldUpdateUserPassword()
        {
            var user = CreateTestUser();
            var id = await _repository.CreateAsync(user, _cancellationToken);
            var newPassword = "NewSecurePass789!";
            var newPasswordHash = Users.PasswordHasherService.HashBCrypt(newPassword);
            await _repository.UpdatePasswordAsync(id, newPasswordHash, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNotNull(result);
            Assert.IsTrue(Users.VerifyPassword(newPassword, result.HashPassword));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            var user = CreateTestUser();
            var id = await _repository.CreateAsync(user, _cancellationToken);
            await _repository.DeleteAsync(id, _cancellationToken);
            var result = await _repository.GetByIdAsync(id, _cancellationToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task VerifyAsync_ValidCredentials_ShouldReturnTrue()
        {
            var password = "SecurePass123!";
            var userResult = Users.Create(
                Guid.NewGuid(),
                "test@example.com",
                "Test User",
                password,
                "student",
                0m,
                DateTime.UtcNow);
            Assert.IsTrue(userResult.IsSuccess);
            await _repository.CreateAsync(userResult.Value, _cancellationToken);
            var isValid = await _repository.VerifyAsync("test@example.com", password, _cancellationToken);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public async Task GetRoleAndIdAsync_ShouldReturnCorrectData()
        {
            var user = CreateTestUser(login: "john@example.com", role: "admin");
            var id = await _repository.CreateAsync(user, _cancellationToken);
            var role = await _repository.GetRoleAsync("john@example.com", _cancellationToken);
            var userId = await _repository.GetIdAsync("john@example.com", _cancellationToken);
            Assert.AreEqual("admin", role);
            Assert.AreEqual(id, userId);
        }

        [TestMethod]
        public async Task GetPagedAsync_ShouldReturnCorrectPage()
        {
            for (int i = 1; i <= 5; i++)
            {
                var user = CreateTestUser(login: $"user{i}@example.com");
                await _repository.CreateAsync(user, _cancellationToken);
            }
            var page1 = await _repository.GetPagedAsync(1, 2, _cancellationToken);
            var page2 = await _repository.GetPagedAsync(2, 2, _cancellationToken);
            Assert.AreEqual(2, page1.Count());
            Assert.AreEqual(2, page2.Count());
            Assert.AreNotEqual(page1.First().Login, page2.First().Login);
        }

        [TestMethod]
        public async Task ExistsByLoginAsync_ShouldReturnTrueForExistingUser()
        {
            var user = CreateTestUser(login: "exists@example.com");
            await _repository.CreateAsync(user, _cancellationToken);
            var exists = await _repository.ExistsByLoginAsync("exists@example.com", _cancellationToken);
            Assert.IsTrue(exists);
        }

        private Users CreateTestUser(
            string login = "test@example.com",
            string name = "Test User",
            string role = "student",
            decimal balance = 0m,
            DateTime? createdAt = null)
        {
            var password = "TestPassword123!";
            var userResult = Users.Create(
                Guid.NewGuid(),
                login,
                name,
                password,
                role,
                balance,
                createdAt ?? DateTime.UtcNow);
            Assert.IsTrue(userResult.IsSuccess, $"Failed to create test user: {userResult.Error}");
            return userResult.Value;
        }
    }
}
