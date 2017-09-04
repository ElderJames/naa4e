using System;

namespace IBuyStuff.Domain.Shared
{
    public sealed class Money
    {
        public static Money Zero = new Money(Currency.Default, 0);
        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Value = amount;
        }

        #region Added to please the O/RM

        /// <summary>
        /// Used by the O/RM to materialize objects
        /// </summary>
        private Money()
        {
        }

        #endregion


        public Currency Currency { get; private set; }
        public decimal Value { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}{1}", Currency.Symbol, Value);
        }

        #region Operators
        public static Money operator +(Money c1, Money c2)
        {
            if (c1.Currency != c2.Currency)
                throw new ArgumentException("+ operator, c1, c2");
            return new Money(c1.Currency, c1.Value + c2.Value);
        }
        public static Money operator -(Money c1, Money c2)
        {
            if (c1.Currency != c2.Currency)
                throw new ArgumentException("- operator, c1, c2");
            return new Money(c1.Currency, c1.Value - c2.Value);
        }
        public static Money operator +(Money c1, decimal d)
        {
            return new Money(c1.Currency, c1.Value + d);
        }

        public static Money operator *(Money c1, int n)
        {
            return new Money(c1.Currency, c1.Value * n);
        }

        // Operators are NOT commutative by default
        public static Money operator *(int n, Money c1)
        {
            return new Money(c1.Currency, c1.Value * n);
        }
        #endregion

        #region Equality
        public static bool operator ==(Money c1, Money c2)
        {
            // Both null or same instance
            if (ReferenceEquals(c1, c2))
                return true;

            // Return false if one is null, but not both 
            if (((object)c1 == null) || ((object)c2 == null))
                return false;

            return c1.Equals(c2);
        }
        public static bool operator !=(Money c1, Money c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            if (this == (Money)obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Money)obj;
            return Currency == other.Currency && Value == other.Value;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        #endregion
    }
}