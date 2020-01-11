using System;
using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    class DrinkMachine : IDrinkMachine
    {
        public float Credit { get; set; }
        public IArticleRepository ArticleRepository { get; set; }

        public DrinkMachine(IArticleRepository articleRepository)
        {
            ArticleRepository = articleRepository;
        }

        private DrinkMachine()
        {
        }

        public void Insert(float cash)
        {
            Credit += cash;
            DisplayMessage($"Adding {cash} to credit.");
        }

        public bool Order(Order order) =>
            order.DispenseArticle();

        public void Recall()
        {
            DisplayMessage($"Returning {Credit} to customer.");
            Credit = 0;
        }

        public void DisplayMessage(string message) =>
            Console.WriteLine(message);
    }
}
