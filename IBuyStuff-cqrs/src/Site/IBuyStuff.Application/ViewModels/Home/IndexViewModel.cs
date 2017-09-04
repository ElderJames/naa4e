using System.Collections;
using System.Collections.Generic;
using IBuyStuff.Domain.Products;

namespace IBuyStuff.Application.ViewModels.Home
{
    public class IndexViewModel : ViewModelBase
    {
        public ICollection<Product> Featured { get; set; }
        public int SubscriberCount { get; set; }
    }
}