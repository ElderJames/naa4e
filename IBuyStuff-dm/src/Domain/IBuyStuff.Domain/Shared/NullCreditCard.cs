using System;

namespace IBuyStuff.Domain.Shared
{
    public class NullCreditCard : CreditCard
    {
        public static NullCreditCard Instance = new NullCreditCard();
    }
}