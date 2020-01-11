using VendingMachine.Interfaces;
using VendingMachine.Models;
using Moq;
using System.Collections.Generic;

namespace VendingMachine.Tests
{
    class ArticleRepositoryMock
    {
        public Mock<IArticleRepository> MockArticleRepository { get; }

        //Need a list with different stock
        private readonly List<Article> _articles = new List<Article>()
            {
                new Article(1, "Chimay", 20, 0 ),
                new Article(2, "Rochefort", 15, 1 ),
                new Article(3, "Westmalle", 15, 2 )
            };

        public ArticleRepositoryMock()
        {
            MockArticleRepository = SetArticleRepository();
        }

        private Mock<IArticleRepository> SetArticleRepository()
        {
            var articleRepository = new Mock<IArticleRepository>();
            articleRepository.Setup(ar => ar.ListAricles()).Returns(_articles);
            articleRepository.Setup(ar => ar.GetArticle(It.IsAny<int>())).Returns<int>(articleId => _articles.Find(a => a.Id == articleId));
            articleRepository.Setup(ar => ar.DecrementStock(It.IsAny<int>()));
            return articleRepository;
        }
    }
}