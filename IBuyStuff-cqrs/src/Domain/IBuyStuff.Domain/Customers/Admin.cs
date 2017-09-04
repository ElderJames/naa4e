using System;

namespace IBuyStuff.Domain.Customers
{
    public class Admin
    {
        public static Admin CreateNew(string name)
        {
            var admin = new Admin {Name = name};
            return admin;
        }

        protected Admin()
        {
            Name = String.Empty;
        }

        public string Name { get; private set; }
        public string PasswordHash { get; private set; }
    }
}