using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillTrade.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class addVerify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("42083811-578f-4f36-bf5f-4bc608c9cd2f"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("d3e58d7a-5298-4d4e-a4ee-1f869d35183b"));

            migrationBuilder.CreateTable(
                name: "VerifyOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuantityTry = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyOperations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationTimeHours", "IdActor", "LessonsCount", "Level", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("bea71c33-5797-4e9c-81f8-c4e5da922fcc"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Создание масштабируемых бэкенд-решений, Entity Framework, Docker, RabbitMQ.", 52, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "advanced", 0m, "C# ASP.NET Core: разработка API и микросервисы" },
                    { new Guid("c1441f27-bb6b-4061-bcd3-071231567096"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Погружение в современный frontend: хуки, контекст, RTK Query, тестирование.", 34, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "React + TypeScript: enterprise приложения" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerifyOperations_Email",
                table: "VerifyOperations",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerifyOperations");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("bea71c33-5797-4e9c-81f8-c4e5da922fcc"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c1441f27-bb6b-4061-bcd3-071231567096"));

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationTimeHours", "IdActor", "LessonsCount", "Level", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("42083811-578f-4f36-bf5f-4bc608c9cd2f"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Погружение в современный frontend: хуки, контекст, RTK Query, тестирование.", 34, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "middle", 0m, "React + TypeScript: enterprise приложения" },
                    { new Guid("d3e58d7a-5298-4d4e-a4ee-1f869d35183b"), new DateTime(2026, 5, 3, 19, 3, 30, 0, DateTimeKind.Utc), "Создание масштабируемых бэкенд-решений, Entity Framework, Docker, RabbitMQ.", 52, new Guid("2c84949a-ebe9-4418-aa21-1d10f739e564"), 3, "advanced", 0m, "C# ASP.NET Core: разработка API и микросервисы" }
                });
        }
    }
}
