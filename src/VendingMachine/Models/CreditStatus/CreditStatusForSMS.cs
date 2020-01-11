using VendingMachine.Interfaces;

namespace VendingMachine.Models
{
    class CreditStatusForSMS : ICreditStatus
    {
        public CreditStatusForSMS()
        {
        }

        public bool HasEnoughCredit => 
            true;

        public string Message =>
             string.Empty;
    }
}
