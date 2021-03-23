﻿namespace Quiz.Models
{
    using System.Collections.Generic;
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}