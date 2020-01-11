using System.Linq;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using Xunit;

namespace VendingMachine.Tests
{
    public class CreditStatusTests
    {
        private readonly IArticleRepository _articleRepository;
        private readonly Article _article;

        public CreditStatusTests()
        {
            _articleRepository = new ArticleRepositoryMock().MockArticleRepository.Object;
            _article = _articleRepository.ListAricles().First(a => a.Price > 0);
        }

        [Fact]
        public void CreditStatusForCash_HasNotEnoughCredit_returns_false()
        {
            var result = new CreditStatusForCash(0, _article.Price);
            Assert.False(result.HasEnoughCredit);
            Assert.False(string.IsNullOrEmpty(result.Message));
        }

        [Fact]
        public void CreditStatusForCash_HasEnoughCredit_returns_true()
        {
            var result = new CreditStatusForCash(float.MaxValue, _article.Price);
            Assert.True(result.HasEnoughCredit);
        }

        [Fact]
        public void CreditStatusForSMS_returns_true()
        {
            var result = new CreditStatusForSMS();
            Assert.True(result.HasEnoughCredit);
        }
    }
}