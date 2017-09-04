using System.Data.Entity.ModelConfiguration;
using IBuyStuff.QueryModel.Orders;

namespace IBuyStuff.QueryModel.Persistence.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            HasKey(t => t.OrderId);

            // Table and relationships 
            ToTable("Orders");
            //HasRequired(o => o.Buyer);
            HasMany(o => o.Items);
        }
    }
}