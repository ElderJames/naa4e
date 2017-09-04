using System;

namespace IBuyStuff.Application.ViewModels
{
    public class SocialInterface
    {
        public static SocialInterface NewForDino()
        {
            var social = new SocialInterface
            {
                Twitter = "http://twitter.com/despos",
                Facebook = "",
                Wordpress = "http://software2cents.wordpress.com",
                LinkedIn = "http://it.linkedin.com/pub/dino-esposito/4/221/9a3/",
                Google = "http://plus.google.com/108569971473006651006"
            };
            return social;
        }
        public static SocialInterface NewForAndrea()
        {
            var social = new SocialInterface
            {
                Twitter = "http://twitter.com/andysal74",
                Facebook = "http://www.facebook.com/ManagedDesigns",
                Wordpress = "",
                LinkedIn = "http://it.linkedin.com/in/andysal",
                Google = "http://plus.google.com/+AndreaSaltarello"
            }; 
            return social;
        }

        protected SocialInterface()
        {
            Wordpress = String.Empty;
        }

        public String Twitter { get; set; }
        public String Facebook { get; set; }
        public String Wordpress { get; set; }
        public String LinkedIn { get; set; }
        public String Google { get; set; }
    }
}
