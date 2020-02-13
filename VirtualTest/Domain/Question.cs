using System.Collections.Generic;

namespace VirtualTest.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CorrectAnswer { get; set; }
        public bool Result { get; set; }
        public IEnumerable<string> IncorrectAnswers { get; set; }
    }
}
