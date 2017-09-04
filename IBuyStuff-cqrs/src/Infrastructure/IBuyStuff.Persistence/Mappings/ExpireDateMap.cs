using System.Data.Entity.ModelConfiguration;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Persistence.Mappings
{
    public class ExpiryDateMap : ComplexTypeConfiguration<ExpiryDate>
    {
        public ExpiryDateMap()
        {
            // Properties		
            Ignore(p => p.When);
        }
    }
}
