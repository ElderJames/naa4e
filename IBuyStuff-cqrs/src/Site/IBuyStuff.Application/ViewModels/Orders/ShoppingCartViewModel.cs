using System.Collections.Generic;
using System.Collections.ObjectModel;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Products;

namespace IBuyStuff.Application.ViewModels.Orders
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        private ShoppingCartViewModel()
        {
            Title = "New order";
            EnableEditOnShoppingCart = true;
            Products = new Collection<Product>();
        }

        public static ShoppingCartViewModel CreateEmpty(ShoppingCart cart, ICollection<Product> products)
        {
            var model = new ShoppingCartViewModel {OrderRequest = cart, Products = products};
            return model;
        }
       
        public ICollection<Product> Products { get; private set; }
        public ShoppingCart OrderRequest { get; private set; }
        public bool EnableEditOnShoppingCart { get; set; }
    }
}