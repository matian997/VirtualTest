using System.Collections.Generic;

namespace VirtualTest.DTO
{
    public class QuestionDTO
    {
        public string Name { get; set; }
        public string CorrectAnswer { get; set; }
        public IEnumerable<string> IncorrectAnswers { get; set; }
    }
}
