using System.Linq;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using Xunit;

namespace VendingMachine.Tests
{
    public class OrderTests
    {
        private readonly IDrinkMachine _drinkMachine;
        private readonly IArticleRepository _articleRepository;

        public OrderTests()
        {
            _articleRepository = new ArticleRepositoryMock().MockArticleRepository.Object;
            _drinkMachine = new DrinkMachine(_articleRepository) { Credit = float.MaxValue };
        }

        [Fact]
        public void OrderByCash_StockEmpty_returns_false()
        {
            int chosenArticleId = _articleRepository.ListAricles().First(a => a.Stock == 0).Id;
            var order = new OrderByCash(chosenArticleId, _drinkMachine, new CreditStatusForCash(_drinkMachine.Credit, _drinkMachine.ArticleRepository.GetArticle(chosenArticleId).Price));

            var result = _drinkMachine.Order(order);

            Assert.False(result);
        }

        [Fact]
        public void OrderByCash_ArticleAvailable_returns_true()
        {
            int chosenArticleId = _articleRepository.ListAricles().First(a => a.Stock > 0).Id;
            var order = new OrderByCash(chosenArticleId, _drinkMachine, new CreditStatusForCash(_drinkMachine.Credit, _drinkMachine.ArticleRepository.GetArticle(chosenArticleId).Price));

            var result = _drinkMachine.Order(order);

            Assert.True(result);
        }

        [Fact]
        public void OrderBySMS_UnexistingArticle_returns_false()
        {
            var order = new OrderBySMS(666, _drinkMachine, new CreditStatusForSMS());
            var result = _drinkMachine.Order(order);
            Assert.False(result);
        }

        [Fact]
        public void OrderBySMS_StockEmpty_returns_false()
        {
            int articleId = _articleRepository.ListAricles().First(a => a.Stock == 0).Id;
            var order = new OrderBySMS(articleId, _drinkMachine, new CreditStatusForSMS());

            var result = _drinkMachine.Order(order);

            Assert.False(result);
        }

        [Fact]
        public void OrderBySMS_ArticleAvailable_returns_true()
        {
            int articleId = _articleRepository.ListAricles().First(a => a.Stock > 0).Id;
            var order = new OrderBySMS(articleId, _drinkMachine, new CreditStatusForSMS());

            var result = _drinkMachine.Order(order);

            Assert.True(result);
        }
    }
}
