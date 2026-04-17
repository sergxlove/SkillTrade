using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class Lessons
    {
        public const int MIN_LENGTH_TITLE = 3;
        public const int MAX_LENGTH_TITLE = 200;
        public const int MAX_LENGTH_CONTENT = 100000;
        public Guid Id { get; }
        public Guid IdCourse { get; }
        public string Title { get; } = string.Empty;
        public string Content { get; } = string.Empty;
        public DateTime CreatedAt { get; }

        private Lessons(Guid id, Guid idCourse, string title, string content, DateTime createAt)
        {
            Id = id;
            IdCourse = idCourse;
            Title = title;
            Content = content;
            CreatedAt = createAt;
        }

        public static ResultModel<Lessons> Create(Guid id, Guid idCourse, string title, string content,
            DateTime createAt)
        {
            if (id == Guid.Empty)
                return ResultModel<Lessons>.Failure("Поле Id не должно быть пустым");
            if (idCourse == Guid.Empty)
                return ResultModel<Lessons>.Failure("Поле IdCourse не должно быть пустым");
            if (string.IsNullOrWhiteSpace(title))
                return ResultModel<Lessons>.Failure("Поле Title не должно быть пустым");
            if (title.Length < MIN_LENGTH_TITLE)
                return ResultModel<Lessons>.Failure($"Поле Title должно содержать минимум {MIN_LENGTH_TITLE} символа");
            if (title.Length > MAX_LENGTH_TITLE)
                return ResultModel<Lessons>.Failure($"Поле Title не должно превышать {MAX_LENGTH_TITLE} символов");
            if (string.IsNullOrWhiteSpace(content))
                return ResultModel<Lessons>.Failure("Поле Content не должно быть пустым");
            if (content.Length > MAX_LENGTH_CONTENT)
                return ResultModel<Lessons>.Failure($"Поле Content не должно превышать {MAX_LENGTH_CONTENT} символов");
            if (createAt > DateTime.UtcNow)
                return ResultModel<Lessons>.Failure("Поле CreatedAt не может быть в будущем");
            return ResultModel<Lessons>.Success(new Lessons(id, idCourse, title, content, createAt));
        }
    }
}
