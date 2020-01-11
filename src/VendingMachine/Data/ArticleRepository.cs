using VendingMachine.Interfaces;
using VendingMachine.Models;
using System.Collections.Generic;

namespace VendingMachine.Data
{
    class ArticleRepository : IArticleRepository
    {
        private readonly List<Article> _aricles;
        private ArticleRepository()
        {
        }

        public ArticleRepository(List<Article> aricles)
        {
            _aricles = aricles;
        }

        public Article GetArticle(int articleId) =>
            _aricles.Find(a => a.Id == articleId);

        public void DecrementStock(int articleId)
        {
            var article = _aricles.Find(i => i.Id == articleId);
            if (article == null || article?.Stock == 0)
                return;

            _aricles.Find(i => i.Id == articleId).Stock -= 1;
        }

        public IEnumerable<Article> ListAricles() =>
            _aricles;
    }
}
