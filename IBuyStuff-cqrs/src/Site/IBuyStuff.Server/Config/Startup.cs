using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using IBuyStuff.Application.Utils;
using IBuyStuff.Server.Common.Misc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;
using Owin;

namespace IBuyStuff.Server.Config
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/SignIn")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure social authentication: Twitter
            var naaeTwitterKey = ConfigurationManager.AppSettings["naa4e:twitter:key"];
            var naaeTwitterSecret = ConfigurationManager.AppSettings["naa4e:twitter:sec"];
            if (!Globals.IsAnyNullOrEmpty(naaeTwitterKey, naaeTwitterSecret))
            {
                var twitterOptions = new TwitterAuthenticationOptions
                {
                    ConsumerKey = naaeTwitterKey,
                    ConsumerSecret = naaeTwitterSecret,
                    Provider = new TwitterAuthenticationProvider()
                    {
                        OnAuthenticated = (context) =>
                        {
                            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstoken", context.AccessToken));
                            context.Identity.AddClaim(new Claim("urn:tokens:twitter:accesstokensecret", context.AccessTokenSecret));
                            return Task.FromResult(0);
                        }
                    }
                };
                app.UseTwitterAuthentication(twitterOptions);
            }

            // Configure social authentication: Facebook
            var naaeFbKey = ConfigurationManager.AppSettings["naa4e:fb:key"];
            var naaeFbSecret = ConfigurationManager.AppSettings["naa4e:fb:sec"];
            if (!Globals.IsAnyNullOrEmpty(naaeFbKey, naaeFbSecret))
            {
                var fbOptions = new FacebookAuthenticationOptions
                {
                    AppId = naaeFbKey,
                    AppSecret = naaeFbSecret,
                    Provider = new FacebookAuthenticationProvider
                    {
                        OnAuthenticated = (context) =>
                        {
                            // Gives you email, ID (to get profile pic), full name, and more
                            context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken));  
                            context.Identity.AddClaim(new Claim("urn:facebook:email", context.Email));
                            return Task.FromResult(0);
                        }
                    }
                };
                fbOptions.Scope.Add("email");
                app.UseFacebookAuthentication(fbOptions);
            }
        }
    }
}