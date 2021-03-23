namespace Quiz.Models
{
    using System.Collections.Generic;

    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
            this.UserAnswers = new HashSet<UserAnswer>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
