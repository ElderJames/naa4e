using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Persistence.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            HasKey(t => t.OrderId);

            // Table and relationships 
            ToTable("Orders");
            HasRequired(o => o.Buyer);
            HasMany(o => o.Items);
        }
    }
}