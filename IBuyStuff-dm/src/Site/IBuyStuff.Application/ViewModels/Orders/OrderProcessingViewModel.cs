using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IBuyStuff.Application.ViewModels.Orders
{
    public class OrderProcessingViewModel : ViewModelBase
    {
        public OrderProcessingViewModel()
        {
            Messages = new Collection<string>();
        }

        public ICollection<string> Messages { get; private set; }
        public bool Denied { get; set; }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
