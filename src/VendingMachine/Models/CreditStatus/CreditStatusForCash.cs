using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    class CreditStatusForCash : ICreditStatus
    {
        private readonly float _credit;
        private readonly float _articlePrice;

        private CreditStatusForCash()
        {
        }

        public CreditStatusForCash(float credit, float articlePrice)
        {
            _credit = credit;
            _articlePrice = articlePrice;
        }

        public bool HasEnoughCredit => 
            _credit >= _articlePrice;

        public string Message =>
             HasEnoughCredit ? string.Empty : $"Need {_articlePrice - _credit} more.";
    }
}
