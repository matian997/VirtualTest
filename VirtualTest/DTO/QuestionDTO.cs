using System.Collections.Generic;

namespace VirtualTest.DTO
{
    public class QuestionDTO
    {
        public string Description { get; set; }
        public string CorrectAnswer { get; set; }
        public IEnumerable<string> IncorrectAnswers { get; set; }
    }
}
