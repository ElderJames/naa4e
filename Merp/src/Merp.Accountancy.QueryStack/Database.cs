﻿using Merp.Accountancy.QueryStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.QueryStack
{
    public class Database : IDatabase, IDisposable
    {
        private AccountancyContext Context;

        public Database()
        {
            Context = new AccountancyContext();
            Context.Configuration.AutoDetectChangesEnabled = false;
        }

        public IQueryable<JobOrder> JobOrders
        {
            get
            {
                return Context.JobOrders;
            }
        }
        public IQueryable<IncomingInvoice> IncomingInvoices
        {
            get
            {
                return Context.IncomingInvoices;
            }
        }
        public IQueryable<OutgoingInvoice> OutgoingInvoices
        {
            get
            {
                return Context.OutgoingInvoices;
            }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}
