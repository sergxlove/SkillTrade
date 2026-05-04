using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillTrade.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdActor = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LessonsCount = table.Column<int>(type: "integer", nullable: false),
                    DurationTimeHours = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCourse = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "character varying(100000)", maxLength: 100000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentProgress = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    TotalProgress = table.Column<int>(type: "integer", nullable: false),
                    SubscribeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HashPassword = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationTimeHours", "IdActor", "LessonsCount", "Level", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("42083811-578f-4f36-bf5f-4bc608c9cd2f"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Погружение в современный frontend: хуки, контекст, RTK Query, тестирование.", 34, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "React + TypeScript: enterprise приложения" },
                    { new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "NumPy, Pandas, визуализация, основы машинного обучения. Итоговый проект — анализ данных.", 28, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "beginner", 0m, "Python для Data Science и AI" },
                    { new Guid("d3e58d7a-5298-4d4e-a4ee-1f869d35183b"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Создание масштабируемых бэкенд-решений, Entity Framework, Docker, RabbitMQ.", 52, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "advanced", 0m, "C# ASP.NET Core: разработка API и микросервисы" },
                    { new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Освой React, Node.js, MongoDB и создавай современные веб-приложения. Более 40 часов контента и 5 живых проектов.", 46, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "Fullstack JavaScript: от новичка до PRO" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "CreatedAt", "IdCourse", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "<h1>Введение в JavaScript</h1><p>JavaScript — это язык программирования, который используется для создания интерактивных веб-страниц. Он позволяет добавлять динамическое поведение на сайты.</p><h2>Что такое JavaScript?</h2><p>JavaScript был создан в 1995 году Бренданом Эйхом. Сегодня это один из самых популярных языков программирования в мире.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Введение в JavaScript" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "<h1>Переменные и типы данных</h1><p>Переменные используются для хранения данных. В JavaScript есть три способа объявления переменных: var, let и const.</p><h2>Типы данных</h2><ul><li><strong>String</strong> — строки</li><li><strong>Number</strong> — числа</li><li><strong>Boolean</strong> — true/false</li></ul>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Переменные и типы данных" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "<h1>Функции в JavaScript</h1><p>Функции — это блоки кода, которые можно вызывать многократно. Они помогают избежать дублирования кода.</p><h2>Объявление функции</h2><pre><code>function greet(name) { return 'Привет, ' + name; }</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Функции и область видимости" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "<h1>Введение в Python</h1><p>Python — это высокоуровневый язык программирования, который отличается простотой и читаемостью кода. Он широко используется в науке о данных, машинном обучении и веб-разработке.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Введение в Python" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "<h1>Pandas: работа с данными</h1><p>Библиотека Pandas предоставляет мощные инструменты для анализа и обработки данных. Основные структуры: Series и DataFrame.</p><h2>Пример кода</h2><pre><code>import pandas as pd\ndf = pd.read_csv('data.csv')\nprint(df.head())</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Pandas для анализа данных" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "<h1>Визуализация данных</h1><p>Matplotlib и Seaborn — основные библиотеки для создания графиков и диаграмм в Python.</p><h2>Пример графика</h2><pre><code>import matplotlib.pyplot as plt\nplt.plot([1,2,3], [4,5,6])\nplt.show()</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Визуализация данных" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "<h1>Введение в C# и .NET</h1><p>C# — современный объектно-ориентированный язык программирования, разработанный Microsoft. .NET — это платформа для разработки приложений.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "Введение в C# и .NET" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "<h1>Создание Web API</h1><p>ASP.NET Core позволяет быстро создавать REST API. Контроллеры, маршрутизация, привязка моделей.</p><h2>Пример контроллера</h2><pre><code>[ApiController]\n[Route('api/[controller]')]\npublic class UsersController : ControllerBase\n{\n    [HttpGet]\n    public IActionResult Get() { return Ok(); }\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "ASP.NET Core Web API" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "<h1>Entity Framework Core</h1><p>ORM для работы с базами данных. Подход Code First, миграции, LINQ-запросы.</p><h2>Пример модели</h2><pre><code>public class User\n{\n    public int Id { get; set; }\n    public string Name { get; set; }\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "Entity Framework Core" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "<h1>Введение в React</h1><p>React — библиотека для создания пользовательских интерфейсов. Компонентный подход, JSX, виртуальный DOM.</p><h2>Пример компонента</h2><pre><code>function Welcome() {\n    return &lt;h1&gt;Hello, React!&lt;/h1&gt;;\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "Введение в React" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "<h1>TypeScript для React</h1><p>Типизация пропсов, состояния, хуков. Интерфейсы и типы.</p><h2>Пример с типами</h2><pre><code>interface Props {\n    name: string;\n    age?: number;\n}\n\nconst User: React.FC&lt;Props&gt; = ({ name, age }) => { ... }</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "TypeScript в React" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "<h1>Redux Toolkit</h1><p>Управление глобальным состоянием приложения. Срезы (slices), экшены, редюсеры.</p><h2>Пример slice</h2><pre><code>const counterSlice = createSlice({\n    name: 'counter',\n    initialState: 0,\n    reducers: {\n        increment: state => state + 1\n    }\n});</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "Управление состоянием: Redux Toolkit" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "HashPassword", "Login", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "$2a$11$KkcWYSsG4a.G3Ax9vpK7f.VpR.AD8rp1P/tOBhMUityHntW366ZH.", "actor123", "actor", "actor" },
                    { new Guid("5d03765b-47d6-4f5b-9bd8-3d41bd2f62a3"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "$2a$11$TPVCYIUVQQ1H5DTLLjcRDOBGzPe4cAJ/rod66GzgGhrSEOFSz2k/O", "admin123", "admin", "admin" },
                    { new Guid("f07bbc78-67d1-452e-8bad-21fc146b56e3"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "$2a$11$TqnnLCGo8zSb2z9hVzpwSetLTFxczO1ASG6f6Mn7ap7VVi4MPztbO", "student123", "student", "student" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IdActor",
                table: "Courses",
                column: "IdActor");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Level",
                table: "Courses",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Title",
                table: "Courses",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdCourse",
                table: "Lessons",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Title",
                table: "Lessons",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId_CourseId",
                table: "UserCourses",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
