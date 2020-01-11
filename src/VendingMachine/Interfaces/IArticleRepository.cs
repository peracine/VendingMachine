using VendingMachine.Models;
using System.Collections.Generic;

namespace VendingMachine.Interfaces
{
    interface IArticleRepository
    {
        Article GetArticle(int articleId);
        IEnumerable<Article> ListAricles();
        void DecrementStock(int articleId);
    }
}
