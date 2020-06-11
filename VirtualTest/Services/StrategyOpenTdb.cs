using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTest.Services
{
    public class StrategyOpenTdb : IStrategyOpenTdb
    {
        public double GetScore(int amountCorrectAnswers, int amountQuestions, Dificultad difficulty, int timeConsumed)
        {
            double timeFactor;
            double difficultyFactor = 0;
            double seconds = (double)timeConsumed / (double)amountQuestions;

            switch (difficulty)
            {
                case Dificultad.Bajo:
                    difficultyFactor = 1;
                    break;
                case Dificultad.Medio:
                    difficultyFactor = 3;
                    break;
                case Dificultad.Dificil:
                    difficultyFactor = 5;
                    break;
            }

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
            return (double)amountCorrectAnswers / (double)amountQuestions * difficultyFactor * timeFactor;
        }
    }
}
