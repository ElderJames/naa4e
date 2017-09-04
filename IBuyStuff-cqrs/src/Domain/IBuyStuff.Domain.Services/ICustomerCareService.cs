namespace IBuyStuff.Domain.Services
{
    public interface ICustomerCareService
    {
        /// <summary>
        /// The method accesses some internal records to return a Boolean answer to the 
        /// question whether the customer is good payer.
        /// </summary>
        /// <returns>True if successful; False otherwise</returns>
        bool CustomerHasPositivePaymentHistory();

        /// <summary>
        /// The method updates internal archives that determine the points on
        /// the customer's fidelity card and its gold/silver status.
        /// </summary>
        /// <returns></returns>
        bool UpdateFidelityCard();
    }
}