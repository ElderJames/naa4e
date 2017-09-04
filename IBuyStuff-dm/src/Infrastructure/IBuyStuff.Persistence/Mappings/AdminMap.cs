using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Customers;

namespace IBuyStuff.Persistence.Mappings
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            HasKey(t => t.Name);
            Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name");

            // Table and relationships 
            ToTable("Admins");
        }
    }

}