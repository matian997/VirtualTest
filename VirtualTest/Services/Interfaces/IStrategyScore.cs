using VirtualTest.Domain;

namespace VirtualTest.Services.Interfaces
{
    interface IStrategyScore
    {
        double GetScoreByOpenTDB(int amountCorrectAnswers, int amountQuestions,  Difficulty difficulty, double timeConsumed);
    }
}
