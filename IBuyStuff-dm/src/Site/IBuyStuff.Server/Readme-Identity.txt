ASP.NET Identity 2.0

    Requires 4.5.1
    Nuget
        Install-Package Microsoft.AspNet.Identity.EntityFramework –Version 2.0.0
        Install-Package Microsoft.AspNet.Identity.Core -Version 2.0.0
        Install-Package Microsoft.AspNet.Identity.OWIN -Version 2.0.0
    Extra Nuget
        Install Identity package for each social network you want to support.
        Set up startup class (using OwinAttribute or AppSettings)
	ChallengeResult helper class


	BaseAuthenticationController
	IdentityExtensions to pass to ClaimsIdentity
	IdentityHelpers to access OWIN authentication and add claims (app-specific)
	ClaimsIdentityExtensions to easily access app-specific claims