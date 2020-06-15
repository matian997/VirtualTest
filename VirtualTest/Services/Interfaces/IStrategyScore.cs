using VirtualTest.Domain;

namespace VirtualTest.Services.Interfaces
{
    interface IStrategyScore
    {
        double GetScore(int amountCorrectAnswers, int amountQuestions,  Difficulty difficulty, double timeConsumed);
    }
}
