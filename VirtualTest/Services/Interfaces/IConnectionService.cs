using System.Collections.Generic;
using VirtualTest.Domain;

namespace VirtualTest.Services.Interfaces
{
    interface IConnectionService
    {
        IEnumerable<Question> GetTestQuestions(int amount, string category, string dificulty);
        IEnumerable<Category> GetAllCategories();
    }
}
