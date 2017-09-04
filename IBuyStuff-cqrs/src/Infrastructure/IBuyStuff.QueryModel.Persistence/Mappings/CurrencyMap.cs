﻿using System.Data.Entity.ModelConfiguration;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Persistence.Mappings
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
