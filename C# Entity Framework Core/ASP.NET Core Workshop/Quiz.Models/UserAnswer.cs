﻿namespace Quiz.Models
{
    using Microsoft.AspNetCore.Identity;

    public class UserAnswer
    {
        public int Id { get; set; }

        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }

    }
}
