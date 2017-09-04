using System;
using System.Linq;

namespace IBuyStuff.Application.Utils
{
    public class Globals
    {
        public static Boolean IsAnyNullOrEmpty(params String[] tokens)
        {
            return tokens.Any(String.IsNullOrWhiteSpace);
        } 
    }
}