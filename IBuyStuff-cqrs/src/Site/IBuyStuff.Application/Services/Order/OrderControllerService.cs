using System;
using System.Data.Entity;
using System.Linq;
using IBuyStuff.Application.ViewModels.Orders;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Repositories;
using IBuyStuff.Domain.Services;
using IBuyStuff.Domain.Services.Impl;
using IBuyStuff.Persistence.Facade;
using IBuyStuff.Persistence.Repositories;
using IBuyStuff.QueryModel.Persistence;

namespace IBuyStuff.Application.Services.Order
{
    /*
     *   In this example (chapter 11), this class has been partly reformulated as a command processor. 
     *   To show the same application service done in two ways leading to event sourcing architecture, 
     *   we've deliberately opted for the same logic split in classic application service and command 
     *   processor.
     *   The command processor takes care of the checkout process (before and after payment).
     */ 

    public class OrderControllerService : IOrderControllerService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogService _catalogService;

        public OrderControllerService()
            : this(new OrderRepository(), new CatalogService())
        {
        }

        public OrderControllerService(IOrderRepository orderRepository, ICatalogService catalogService)
        {
            _orderRepository = orderRepository;
            _catalogService = catalogService;
        }

        // TO BE MODIFIED (LET)
        public OrderFoundViewModel RetrieveOrderForCustomer(int orderId)
        {
            //using (var db = new DomainModelFacade())
            //{
            //    try
            //    {
            //        // Need to load the entire graph of objects
            //        var order = (from o in db.Orders
            //            .Include("Items")
            //            .Include("Items.Product")
            //            where o.OrderId == orderId
            //            select o).Single();
            //        var oxx = order;
            //        return null;
            //    }
            //    catch (InvalidOperationException)
            //    {
            //        return new OrderFoundViewModel();
            //    }
            //}
            using (var db = new QueryModelDatabase())
            {
                var queryable = from o in db.Orders.Include(p => p.Items).Include("Details.Product")
                                where o.OrderId == orderId
                                select new OrderFoundViewModel
                                {
                                    Id = o.OrderId,
                                    State = o.State.ToString(),
                                    Total = o.Total,
                                    OrderDate = o.Date,
                                    Details = o.Items
                                };
                try
                {
                    var o = queryable.First();
                    return o;
                }
                catch (InvalidOperationException)
                {
                    return new OrderFoundViewModel();
                }
            }

        
            //var order = _orderRepository.FindById(orderId);
            //if (order is MissingOrder)
            //    return new OrderFoundViewModel();
            //return OrderFoundViewModel.CreateFromOrder(order);
        }

        public OrderFoundViewModel RetrieveLastOrderForCustomer(string customerId)
        {
            var order = _orderRepository.FindLastByCustomer(customerId);
            if (order is MissingOrder)
                return new OrderFoundViewModel();
            //return OrderFoundViewModel.CreateFromOrder(order);
            return new OrderFoundViewModel();
        }

        public ShoppingCartViewModel CreateShoppingCartForCustomer(string customerId)
        {
            var adHocCatalog = _catalogService.GetCustomerAdHocCatalog(customerId);
            var cart = ShoppingCart.CreateEmpty(adHocCatalog.Customer);
            return ShoppingCartViewModel.CreateEmpty(cart, adHocCatalog.Products);
        }

        public ShoppingCartViewModel AddProductToShoppingCart(ShoppingCartViewModel cart, int productId, int quantity)
        {
            var product = (from p in cart.Products where p.Id == productId select p).First();
            cart.OrderRequest.AddItem(quantity, product);
            return cart;
        }
    }
}