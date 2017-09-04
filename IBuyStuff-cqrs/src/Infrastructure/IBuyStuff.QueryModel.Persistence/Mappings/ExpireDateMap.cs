using System.Data.Entity.ModelConfiguration;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Persistence.Mappings
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
