using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Customers;
using IBuyStuff.Domain.Orders;

namespace IBuyStuff.Persistence.Mappings
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            // Primary Key
            HasKey(t => t.Id);
            Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id");

            // Properties			
            Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity");

            // Table and relationships 
            ToTable("OrderItems");       
            HasRequired(o => o.Order);
            HasRequired(o => o.Product);
        }
    }
}
