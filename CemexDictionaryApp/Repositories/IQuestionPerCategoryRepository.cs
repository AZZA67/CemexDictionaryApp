using CemexDictionaryApp.Models;

namespace CemexDictionaryApp.Repositories
{
    public interface IQuestionPerCategoryRepository
    {
        int Insert(QuestionPerCategory _questionPerCategory);
    }
}