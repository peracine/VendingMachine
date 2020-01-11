using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    class OrderBySMS : Order
    {
        public OrderBySMS(int chosenArticle, IDrinkMachine drinkMachine, ICreditStatus creditStatus) : base(chosenArticle, drinkMachine, creditStatus)
        {
        }

        public override bool DispenseArticle()
        {
            var article = DrinkMachine.ArticleRepository.GetArticle(ChosenArticle);
            if (article == null)
            {
                return false;
            }

            if (article.Stock == 0)
            {
                DrinkMachine.DisplayMessage($"No { article.Name} left.");
                return false;
            }

            DrinkMachine.ArticleRepository.DecrementStock(ChosenArticle);
            DrinkMachine.DisplayMessage($"Giving { article.Name} out.");
            return true;
        }
    }
}
