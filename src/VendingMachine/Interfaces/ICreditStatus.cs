namespace VendingMachine.Interfaces
{
    interface ICreditStatus
    {
        bool HasEnoughCredit { get; }
        string Message { get; }
    }
}