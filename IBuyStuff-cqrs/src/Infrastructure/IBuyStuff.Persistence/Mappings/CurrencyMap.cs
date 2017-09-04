using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Persistence.Mappings
{
    public class CurrencyMap : ComplexTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            // Properties		
            Ignore(p => p.Name);

            Property(p => p.Symbol)
                .IsRequired()
                .HasColumnName("");
        }
    }
}
