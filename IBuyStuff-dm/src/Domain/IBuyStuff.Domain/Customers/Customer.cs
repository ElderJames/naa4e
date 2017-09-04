using System;
using System.Collections.Generic;
using IBuyStuff.Domain.Orders;
using IBuyStuff.Domain.Shared;

namespace IBuyStuff.Domain.Customers
{
    public class Customer : IAggregateRoot
    {
        public static Customer CreateNew(Gender gender, string name, string firstname, string lastname, string email)
        {
            var customer = new Customer
            {
                CustomerId = name, 
                Address = Address.Create(), 
                Payment = NullCreditCard.Instance,
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
        /// Set the full postal address of the customer.
        /// </summary>
        /// <param name="address">New address</param>
        /// <returns>this instance</returns>
        public Customer SetAddress(Address address)
        {
            if (address != null)
                Address = address;

            return this;
        }

        /// <summary>
        /// Set the password hash for the customer
        /// </summary>
        /// <param name="hash">Hash of a password to save</param>
        /// <returns>this object</returns>
        public Customer SetPasswordHash(string hash)
        {
            PasswordHash = hash;
            return this;
        }

        /// <summary>
        /// Set the payment details of the customer (e.g., credit card number)
        /// </summary>
        /// <param name="card">Credit card used for payment</param>
        /// <returns>this instance</returns>
        public Customer SetPaymentDetails(CreditCard card)
        {
            if (card != null)
                Payment = card;

            return this;
        }

        /// <summary>
        /// Set the avatar
        /// </summary>
        /// <param name="url">URL for the customer picture</param>
        /// <returns>this object</returns>
        public Customer SetAvatar(string url)
        {
            Avatar = url;
            return this;
        }

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
        public static bool operator ==(Customer c1, Customer c2)
        {
            // Both null or same instance
            if (ReferenceEquals(c1, c2))
                return true;

            // Return false if one is null, but not both 
            if (((object)c1 == null) || ((object)c2 == null))
                return false;

            return c1.Equals(c2);
        }
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2); 
        }

        public override bool Equals(object obj)
        {
            if (this == (Customer)obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (Customer)obj;

            // Your identity logic goes here.  
            // You may refactor this code to the method of an entity interface 
            return CustomerId == other.CustomerId;
        }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
        #endregion
    }
}
