using VendingMachine.Models;

namespace VendingMachine.Interfaces
{
    interface IDrinkMachine
    {
        float Credit { get; set; }
        IArticleRepository ArticleRepository { get; set; }
        bool Order(Order order);
        void DisplayMessage(string message);
        void Recall();
        void Insert(float cash);
    }
}
