using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Data;
using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var _articles = new List<Article>()
            {
                new Article(1, "Chimay", 20, 5 ),
                new Article(2, "Rochefort", 15, 3 ),
                new Article(3, "Westmalle", 15, 3 )
            };

            var drinkMachine = new DrinkMachine(new ArticleRepository(_articles));

            while (true)
            {
                Console.WriteLine("\n\nAvailable commands:");
                Console.WriteLine("insert (money) - Money put into money slot");
                Console.WriteLine($"order ({string.Join(',', _articles.Select(a => a.Name))}) - Order from machines buttons");
                Console.WriteLine($"sms order ({string.Join(',', _articles.Select(a => a.Name))}) - Order sent by sms");
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("-------");
                Console.WriteLine($"Inserted money: {drinkMachine.Credit}");
                Console.WriteLine("-------\n\n");
                CommandParser(Console.ReadLine().Trim(), drinkMachine);
            }
        }

        private static void CommandParser(string command, IDrinkMachine drinkMachine)
        {
            if (command.StartsWith("insert", StringComparison.OrdinalIgnoreCase))
            {
                float.TryParse(command.Split(' ')[1], out float cash);
                drinkMachine.Insert(cash);
            }
            else if (command.StartsWith("sms order", StringComparison.OrdinalIgnoreCase))
            {
                int? chosenArticle = ParseChosenArticle(command, drinkMachine.ArticleRepository.ListAricles());
                if (!chosenArticle.HasValue)
                {
                    drinkMachine.DisplayMessage("Article not found!");
                    return;
                }                    

                var order = new OrderBySMS(chosenArticle.Value, drinkMachine, new CreditStatusForSMS());
                drinkMachine.Order(order);
            }
            else if (command.StartsWith("order", StringComparison.OrdinalIgnoreCase))
            {
                int? chosenArticle = ParseChosenArticle(command, drinkMachine.ArticleRepository.ListAricles());
                if (!chosenArticle.HasValue)
                {
                    drinkMachine.DisplayMessage("Article not found!");
                    return;
                }

                var order = new OrderByCash(chosenArticle.Value, drinkMachine, new CreditStatusForCash(drinkMachine.Credit, drinkMachine.ArticleRepository.GetArticle(chosenArticle.Value).Price));
                drinkMachine.Order(order);
            }
            else if (command.StartsWith("recall", StringComparison.OrdinalIgnoreCase))
            {
                drinkMachine.Recall();
            }
        }

        private static int? ParseChosenArticle(string command, IEnumerable<Article> articles) =>
            articles.FirstOrDefault(i => command.Contains(i.Name, StringComparison.OrdinalIgnoreCase))?.Id ?? null;
    }
}
