using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    class OrderByCash : Order
    {
        public OrderByCash(int chosenArticle, IDrinkMachine drinkMachine, ICreditStatus creditStatus) : base(chosenArticle, drinkMachine, creditStatus)
        {
        }

        public override bool DispenseArticle()
        {
            if (!CreditStatus.HasEnoughCredit)
            {
                DrinkMachine.DisplayMessage(CreditStatus.Message);
                return false;
            }

            var article = DrinkMachine.ArticleRepository.GetArticle(ChosenArticle);

            if (article.Stock == 0)
            {
                DrinkMachine.DisplayMessage($"No { article.Name} left.");
                return false;
            }
            
            DrinkMachine.Credit -= article.Price;
            DrinkMachine.ArticleRepository.DecrementStock(ChosenArticle);
            DrinkMachine.DisplayMessage($"Giving { article.Name} out.");
            DrinkMachine.DisplayMessage($"Giving { DrinkMachine.Credit} out in change.");
            DrinkMachine.Credit = 0;
            return true;
        }
    }
}
