using System;
using System.Collections.Generic;
using VirtualTest.Domain;

namespace VirtualTest.Services.Interfaces
{
    interface IConnectionService : IDisposable
    {
        IEnumerable<Question> GetTestQuestions(int amount, int category, string dificulty);
        IEnumerable<Category> GetAllCategories();
    }
}
