using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillTrade.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class DefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationTimeHours", "IdActor", "LessonsCount", "Level", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("961c8891-ff88-49c8-aa05-7073c780780d"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "Погружение в современный frontend: хуки, контекст, RTK Query, тестирование.", 34, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "React + TypeScript: enterprise приложения" },
                    { new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "NumPy, Pandas, визуализация, основы машинного обучения. Итоговый проект — анализ данных.", 28, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "beginner", 0m, "Python для Data Science и AI" },
                    { new Guid("cb66dda9-afd7-4b27-84b5-9512e5653907"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "Создание масштабируемых бэкенд-решений, Entity Framework, Docker, RabbitMQ.", 52, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "advanced", 0m, "C# ASP.NET Core: разработка API и микросервисы" },
                    { new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "Освой React, Node.js, MongoDB и создавай современные веб-приложения. Более 40 часов контента и 5 живых проектов.", 46, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "Fullstack JavaScript: от новичка до PRO" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "CreatedAt", "IdCourse", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "<h1>Введение в JavaScript</h1><p>JavaScript — это язык программирования, который используется для создания интерактивных веб-страниц. Он позволяет добавлять динамическое поведение на сайты.</p><h2>Что такое JavaScript?</h2><p>JavaScript был создан в 1995 году Бренданом Эйхом. Сегодня это один из самых популярных языков программирования в мире.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Введение в JavaScript" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "<h1>Переменные и типы данных</h1><p>Переменные используются для хранения данных. В JavaScript есть три способа объявления переменных: var, let и const.</p><h2>Типы данных</h2><ul><li><strong>String</strong> — строки</li><li><strong>Number</strong> — числа</li><li><strong>Boolean</strong> — true/false</li></ul>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Переменные и типы данных" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "<h1>Функции в JavaScript</h1><p>Функции — это блоки кода, которые можно вызывать многократно. Они помогают избежать дублирования кода.</p><h2>Объявление функции</h2><pre><code>function greet(name) { return 'Привет, ' + name; }</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"), "Функции и область видимости" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "<h1>Введение в Python</h1><p>Python — это высокоуровневый язык программирования, который отличается простотой и читаемостью кода. Он широко используется в науке о данных, машинном обучении и веб-разработке.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Введение в Python" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "<h1>Pandas: работа с данными</h1><p>Библиотека Pandas предоставляет мощные инструменты для анализа и обработки данных. Основные структуры: Series и DataFrame.</p><h2>Пример кода</h2><pre><code>import pandas as pd\ndf = pd.read_csv('data.csv')\nprint(df.head())</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Pandas для анализа данных" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "<h1>Визуализация данных</h1><p>Matplotlib и Seaborn — основные библиотеки для создания графиков и диаграмм в Python.</p><h2>Пример графика</h2><pre><code>import matplotlib.pyplot as plt\nplt.plot([1,2,3], [4,5,6])\nplt.show()</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"), "Визуализация данных" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "<h1>Введение в C# и .NET</h1><p>C# — современный объектно-ориентированный язык программирования, разработанный Microsoft. .NET — это платформа для разработки приложений.</p>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "Введение в C# и .NET" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "<h1>Создание Web API</h1><p>ASP.NET Core позволяет быстро создавать REST API. Контроллеры, маршрутизация, привязка моделей.</p><h2>Пример контроллера</h2><pre><code>[ApiController]\n[Route('api/[controller]')]\npublic class UsersController : ControllerBase\n{\n    [HttpGet]\n    public IActionResult Get() { return Ok(); }\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "ASP.NET Core Web API" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "<h1>Entity Framework Core</h1><p>ORM для работы с базами данных. Подход Code First, миграции, LINQ-запросы.</p><h2>Пример модели</h2><pre><code>public class User\n{\n    public int Id { get; set; }\n    public string Name { get; set; }\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"), "Entity Framework Core" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "<h1>Введение в React</h1><p>React — библиотека для создания пользовательских интерфейсов. Компонентный подход, JSX, виртуальный DOM.</p><h2>Пример компонента</h2><pre><code>function Welcome() {\n    return &lt;h1&gt;Hello, React!&lt;/h1&gt;;\n}</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "Введение в React" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "<h1>TypeScript для React</h1><p>Типизация пропсов, состояния, хуков. Интерфейсы и типы.</p><h2>Пример с типами</h2><pre><code>interface Props {\n    name: string;\n    age?: number;\n}\n\nconst User: React.FC&lt;Props&gt; = ({ name, age }) => { ... }</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "TypeScript в React" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "<h1>Redux Toolkit</h1><p>Управление глобальным состоянием приложения. Срезы (slices), экшены, редюсеры.</p><h2>Пример slice</h2><pre><code>const counterSlice = createSlice({\n    name: 'counter',\n    initialState: 0,\n    reducers: {\n        increment: state => state + 1\n    }\n});</code></pre>", new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"), "Управление состоянием: Redux Toolkit" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "HashPassword", "Login", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "$2a$11$KkcWYSsG4a.G3Ax9vpK7f.VpR.AD8rp1P/tOBhMUityHntW366ZH.", "actor123", "actor", "actor" },
                    { new Guid("5d03765b-47d6-4f5b-9bd8-3d41bd2f62a3"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "$2a$11$TPVCYIUVQQ1H5DTLLjcRDOBGzPe4cAJ/rod66GzgGhrSEOFSz2k/O", "admin123", "admin", "admin" },
                    { new Guid("f07bbc78-67d1-452e-8bad-21fc146b56e3"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Unspecified), "$2a$11$TqnnLCGo8zSb2z9hVzpwSetLTFxczO1ASG6f6Mn7ap7VVi4MPztbO", "student123", "student", "student" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("961c8891-ff88-49c8-aa05-7073c780780d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("cb66dda9-afd7-4b27-84b5-9512e5653907"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d03765b-47d6-4f5b-9bd8-3d41bd2f62a3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f07bbc78-67d1-452e-8bad-21fc146b56e3"));
        }
    }
}
