using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Persistence.Mappings
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            HasKey(t => t.CustomerId);
            Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("Id");
            Property(t => t.FirstName)
                .IsRequired()
                .HasColumnName("FirstName");
            Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("LastName");
            Property(t => t.Email)
                .IsRequired()
                .HasColumnName("Email");

            // Properties			
            Property(t => t.Address.Street)
                .HasMaxLength(30)
                .IsOptional()
                .HasColumnName("Address_Street");
            Property(t => t.Address.City)
                .HasMaxLength(15)
                .IsOptional()
                .HasColumnName("Address_City");
            Property(t => t.Address.Number)
                .IsOptional()
                .HasColumnName("Address_Number");
            Property(t => t.Address.Zip)
                .IsOptional()
                .HasMaxLength(15)
                .HasColumnName("Address_Zip");
            Property(t => t.Payment.Number)
                .IsOptional();
            Property(t => t.Payment.Owner)
                .IsOptional();
            Property(t => t.Payment.Type)
                .IsOptional();
            Property(t => t.Payment.Expires.Month)
                .IsOptional();
            Property(t => t.Payment.Expires.Year)
                .IsOptional();

            // Table and relationships 
            ToTable("Customers");
            HasMany(c => c.Orders);
        }
    }

}