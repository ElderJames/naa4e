using System;
using System.Collections.Generic;
using IBuyStuff.QueryModel.Orders;
using IBuyStuff.QueryModel.Shared;

namespace IBuyStuff.QueryModel.Customers
{
    public class Customer 
    {
        public static Customer CreateNew(Gender gender, string name, string firstname, string lastname, string email)
        {
            var customer = new Customer
            {
                CustomerId = name, 
                Address = Address.Create(), 
                Payment = InvalidCreditCard.Instance,
                Email = email,
                FirstName = firstname,
                LastName = lastname,
                Gender = gender
            };
            return customer;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        protected Customer()
        {
        }

        #endregion

        /// <summary>
        /// Id of the customer. Used to log in, it's checked for uniqueness. 
        /// </summary>
        public string CustomerId { get; private set; }

        /// <summary>
        /// Password hash
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// First name. 
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Last name. 
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gender: either Male or Female
        /// </summary>
        public Gender Gender { get; private set; }

        /// <summary>
        /// URL of the avatar
        /// </summary>
        public string Avatar { get; private set; }

        /// <summary>
        /// Postal address of the customer  
        /// At the very minimum, you might want to use an Address object.
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// Payment details for the customer (whatever that means)
        /// At the very minimum, you might want to use a CreditCard object.
        /// </summary>
        public CreditCard Payment { get; private set; }

        /// <summary>
        /// List of related orders
        /// </summary>
        public ICollection<Order> Orders { get; private set; }
        

        #region Behavior

        /// <summary>
        /// Title for the customer (Mr, Mrs, etc)
        /// </summary>
        /// <returns>string</returns>
        public String GetTitle()
        {
            switch (Gender)
            {
                case Gender.Female:
                    return "Mrs";
                case Gender.Male:
                    return "Mr.";
                default:
                    return "";
            }
        }

        #endregion

        public override string ToString()
        {
            var title = GetTitle();
            return String.Format("{0} {1} {2}", title, FirstName, LastName);
        }

        #region Identity Management
        //public static bool operator ==(Customer c1, Customer c2)
        //{
        //    // Both null or same instance
        //    if (ReferenceEquals(c1, c2))
        //        return true;

        //    // Return false if one is null, but not both 
        //    if (((object)c1 == null) || ((object)c2 == null))
        //        return false;

        //    return c1.Equals(c2);
        //}
        //public static bool operator !=(Customer c1, Customer c2)
        //{
        //    return !(c1 == c2); 
        //}

        //public override bool Equals(object obj)
        //{
        //    if (this == (Customer)obj)
        //        return true;
        //    if (obj == null || GetType() != obj.GetType())
        //        return false;
        //    var other = (Customer)obj;

        //    // Your identity logic goes here.  
        //    // You may refactor this code to the method of an entity interface 
        //    return CustomerId == other.CustomerId;
        //}

        //public override int GetHashCode()
        //{
        //    return CustomerId.GetHashCode();
        //}
        #endregion
    }
}
