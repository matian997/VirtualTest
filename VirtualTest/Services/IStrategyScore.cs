using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualTest.Services
{
    interface IStrategyScore
    {
        double GetScore(int amountCorrectAnswers, int amountQuestions,  difficulty, int timeConsumed);
    }
}
