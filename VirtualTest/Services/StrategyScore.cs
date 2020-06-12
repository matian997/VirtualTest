using VirtualTest.Domain;
using VirtualTest.Services.Interfaces;

namespace VirtualTest.Services
{
    public class StrategyScore : IStrategyScore
    {
        public double GetScoreByOpenTDB(int amountCorrectAnswers, int amountQuestions, Difficulty difficulty, int timeConsumed)
        {
            double timeFactor;
            double seconds = timeConsumed / amountQuestions;

            if (seconds < 5)
            {
                timeFactor = 5;
            }
            else if (seconds >= 5 && seconds <= 20)
            {
                timeFactor = 3;
            }
            else
            {
                timeFactor = 1;
            }

            return amountCorrectAnswers / amountQuestions * (int)difficulty * timeFactor;
        }
    }
}
