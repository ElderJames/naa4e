using IBuyStuff.Application.ViewModels;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Application.InputModels.Order
{
    public class CheckoutInputModel : ViewModelBase
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set; }
        public CreditCardType CardType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}