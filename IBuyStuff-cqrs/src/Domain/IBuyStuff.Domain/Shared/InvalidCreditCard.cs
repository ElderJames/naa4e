using System;

namespace IBuyStuff.Domain.Shared
{
    public class InvalidCreditCard : CreditCard
    {
        public static InvalidCreditCard Instance = new InvalidCreditCard();
    }
}