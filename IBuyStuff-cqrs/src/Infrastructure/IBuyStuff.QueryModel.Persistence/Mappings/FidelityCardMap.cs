using System.Data.Entity.ModelConfiguration;

namespace IBuyStuff.QueryModel.Persistence.Mappings
{
    public class FidelityCardMap : EntityTypeConfiguration<FidelityCard>
    {
        public FidelityCardMap()
        {
            // Properties	
            HasKey(c => c.Number);
            Property(c => c.Number)
                .IsRequired()
                .HasColumnName("Number");
            ToTable("FidelityCards");
            HasRequired(c => c.Owner);
        }
    }
}
