using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    abstract class Order
    {
        protected int ChosenArticle { get; }
        protected IDrinkMachine DrinkMachine { get; }
        protected ICreditStatus CreditStatus { get; }

        public Order(int chosenArticle, IDrinkMachine drinkMachine, ICreditStatus creditStatus)
        {
            ChosenArticle = chosenArticle;
            DrinkMachine = drinkMachine;
            CreditStatus = creditStatus;
        }

        public abstract bool DispenseArticle();
    }
}
