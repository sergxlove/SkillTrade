using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class LessonsConfiguration : IEntityTypeConfiguration<LessonsEntity>
    {
        public void Configure(EntityTypeBuilder<LessonsEntity> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .IsRequired();
            builder.Property(l => l.IdCourse)
                .IsRequired();
            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(Core.Models.Lessons.MAX_LENGTH_TITLE);
            builder.Property(l => l.Content)
                .IsRequired()
                .HasMaxLength(Core.Models.Lessons.MAX_LENGTH_CONTENT);
            builder.Property(l => l.CreatedAt)
                .IsRequired();
            builder.HasIndex(l => l.IdCourse);
            builder.HasIndex(l => l.Title);
            builder.HasData(
                 new LessonsEntity
                 {
                     Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                     IdCourse = Guid.Parse("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"),
                     Title = "Введение в JavaScript",
                     Content = "<h1>Введение в JavaScript</h1><p>JavaScript — это язык программирования, который используется для создания интерактивных веб-страниц. Он позволяет добавлять динамическое поведение на сайты.</p><h2>Что такое JavaScript?</h2><p>JavaScript был создан в 1995 году Бренданом Эйхом. Сегодня это один из самых популярных языков программирования в мире.</p>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                     IdCourse = Guid.Parse("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"),
                     Title = "Переменные и типы данных",
                     Content = "<h1>Переменные и типы данных</h1><p>Переменные используются для хранения данных. В JavaScript есть три способа объявления переменных: var, let и const.</p><h2>Типы данных</h2><ul><li><strong>String</strong> — строки</li><li><strong>Number</strong> — числа</li><li><strong>Boolean</strong> — true/false</li></ul>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                     IdCourse = Guid.Parse("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"),
                     Title = "Функции и область видимости",
                     Content = "<h1>Функции в JavaScript</h1><p>Функции — это блоки кода, которые можно вызывать многократно. Они помогают избежать дублирования кода.</p><h2>Объявление функции</h2><pre><code>function greet(name) { return 'Привет, ' + name; }</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                     IdCourse = Guid.Parse("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"),
                     Title = "Введение в Python",
                     Content = "<h1>Введение в Python</h1><p>Python — это высокоуровневый язык программирования, который отличается простотой и читаемостью кода. Он широко используется в науке о данных, машинном обучении и веб-разработке.</p>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                     IdCourse = Guid.Parse("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"),
                     Title = "Pandas для анализа данных",
                     Content = "<h1>Pandas: работа с данными</h1><p>Библиотека Pandas предоставляет мощные инструменты для анализа и обработки данных. Основные структуры: Series и DataFrame.</p><h2>Пример кода</h2><pre><code>import pandas as pd\ndf = pd.read_csv('data.csv')\nprint(df.head())</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                     IdCourse = Guid.Parse("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"),
                     Title = "Визуализация данных",
                     Content = "<h1>Визуализация данных</h1><p>Matplotlib и Seaborn — основные библиотеки для создания графиков и диаграмм в Python.</p><h2>Пример графика</h2><pre><code>import matplotlib.pyplot as plt\nplt.plot([1,2,3], [4,5,6])\nplt.show()</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                     IdCourse = Guid.Parse("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"),
                     Title = "Введение в C# и .NET",
                     Content = "<h1>Введение в C# и .NET</h1><p>C# — современный объектно-ориентированный язык программирования, разработанный Microsoft. .NET — это платформа для разработки приложений.</p>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                     IdCourse = Guid.Parse("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"),
                     Title = "ASP.NET Core Web API",
                     Content = "<h1>Создание Web API</h1><p>ASP.NET Core позволяет быстро создавать REST API. Контроллеры, маршрутизация, привязка моделей.</p><h2>Пример контроллера</h2><pre><code>[ApiController]\n[Route('api/[controller]')]\npublic class UsersController : ControllerBase\n{\n    [HttpGet]\n    public IActionResult Get() { return Ok(); }\n}</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                     IdCourse = Guid.Parse("b1c2d3e4-f5a6-7b8c-9d0e-1f2a3b4c5d6e"),
                     Title = "Entity Framework Core",
                     Content = "<h1>Entity Framework Core</h1><p>ORM для работы с базами данных. Подход Code First, миграции, LINQ-запросы.</p><h2>Пример модели</h2><pre><code>public class User\n{\n    public int Id { get; set; }\n    public string Name { get; set; }\n}</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                     IdCourse = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"),
                     Title = "Введение в React",
                     Content = "<h1>Введение в React</h1><p>React — библиотека для создания пользовательских интерфейсов. Компонентный подход, JSX, виртуальный DOM.</p><h2>Пример компонента</h2><pre><code>function Welcome() {\n    return &lt;h1&gt;Hello, React!&lt;/h1&gt;;\n}</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                     IdCourse = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"),
                     Title = "TypeScript в React",
                     Content = "<h1>TypeScript для React</h1><p>Типизация пропсов, состояния, хуков. Интерфейсы и типы.</p><h2>Пример с типами</h2><pre><code>interface Props {\n    name: string;\n    age?: number;\n}\n\nconst User: React.FC&lt;Props&gt; = ({ name, age }) => { ... }</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 },
                 new LessonsEntity
                 {
                     Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                     IdCourse = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f90"),
                     Title = "Управление состоянием: Redux Toolkit",
                     Content = "<h1>Redux Toolkit</h1><p>Управление глобальным состоянием приложения. Срезы (slices), экшены, редюсеры.</p><h2>Пример slice</h2><pre><code>const counterSlice = createSlice({\n    name: 'counter',\n    initialState: 0,\n    reducers: {\n        increment: state => state + 1\n    }\n});</code></pre>",
                     CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                 });
        }
    }
}
