using System;
using System.Collections.Generic;
using System.Security.Claims;
using IBuyStuff.Domain.Shared;
using Microsoft.AspNet.Identity;

namespace IBuyStuff.Server.Common.Identity
{
    public class IdentityHelpers
    {
        public const string IBuyStuff_Avatar = "urn:ibuystuff:avatar";
        public static ClaimsIdentity Create(string name, string email, Gender gender, string avatar = null)
        {
            avatar = avatar ?? String.Format("~/content/images/main/{0}.png", gender);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(IBuyStuff_Avatar, avatar)
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }


    }
}