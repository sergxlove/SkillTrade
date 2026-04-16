using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class Lessons
    {
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
            if (title.Length < 3)
                return ResultModel<Lessons>.Failure("Поле Title должно содержать минимум 3 символа");
            if (title.Length > 200)
                return ResultModel<Lessons>.Failure("Поле Title не должно превышать 200 символов");
            if (string.IsNullOrWhiteSpace(content))
                return ResultModel<Lessons>.Failure("Поле Content не должно быть пустым");
            if (content.Length < 10)
                return ResultModel<Lessons>.Failure("Поле Content должно содержать минимум 10 символов");
            if (content.Length > 100000)
                return ResultModel<Lessons>.Failure("Поле Content не должно превышать 100000 символов");
            if (createAt > DateTime.UtcNow)
                return ResultModel<Lessons>.Failure("Поле CreatedAt не может быть в будущем");
            return ResultModel<Lessons>.Success(new Lessons(id, idCourse, title, content, createAt));
        }
    }
}
