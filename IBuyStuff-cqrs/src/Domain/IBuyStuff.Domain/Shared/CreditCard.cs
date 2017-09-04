using System;

namespace IBuyStuff.Domain.Shared
{
    public class CreditCard
    {
        public static CreditCard Create(CreditCardType type, string number, string owner, ExpiryDate expiration)
        {
            var card = new CreditCard {Type = type, Owner = owner, Number = number, Expires = expiration};
            return card;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        protected CreditCard()
        {
            Type = CreditCardType.Unknown;
            Expires = ExpiryDate.Unknown; 
            Number = String.Empty;
            Owner = String.Empty;
        }

        #endregion

        public CreditCardType Type { get; private set; }
        public ExpiryDate Expires { get; private set; }
        public string Owner { get; private set; }
        public string Number { get; private set; }

        #region Equality
        public static bool operator ==(CreditCard c1, CreditCard c2)
        {
            // Both null or same instance
            if (ReferenceEquals(c1, c2))
                return true;

            // Return false if one is null, but not both 
            if (((object)c1 == null) || ((object)c2 == null))
                return false;

            return c1.Equals(c2);
        }
        public static bool operator !=(CreditCard c1, CreditCard c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            if (this == (CreditCard)obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreditCard)obj;
            return string.Equals(Number, other.Number, StringComparison.InvariantCultureIgnoreCase) &&
                   string.Equals(Owner, other.Owner, StringComparison.InvariantCultureIgnoreCase);
        }
        public override int GetHashCode()
        {
            const int hashIndex = 307;
            var result = (Number != null ? Number.GetHashCode() : 0);
            result = (result * hashIndex) ^ (Owner != null ? Owner.GetHashCode() : 0);
            return result;
        }
        #endregion
    }

}