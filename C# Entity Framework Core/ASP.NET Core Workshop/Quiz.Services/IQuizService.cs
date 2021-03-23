namespace Quiz.Services
{
    using Quiz.Services.Models;
    public interface IQuizService
    {
        void Add(string title);

        QuizViewModel GetQuizById(int quizId);
    }
}
