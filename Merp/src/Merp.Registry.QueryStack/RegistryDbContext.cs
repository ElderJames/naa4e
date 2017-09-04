﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merp.Registry.QueryStack.Model;

namespace Merp.Registry.QueryStack
{
    class RegistryDbContext : DbContext
    {
        public RegistryDbContext()
            : base("MerpReadModel")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(entity => entity.ToTable(string.Format("{0}_{1}", "Registry", entity.ClrType.Name)));
        }

        public DbSet<Party> Parties { get; set; }
    }
}
