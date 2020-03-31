using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;


namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@gmail.com"),
                new Claim("Vouch.Says", "Trusted Source"),
            };
            var LicenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Jen Jenkinson"),
                new Claim("DrivingLicense", "Dodgy but cool"),
            };

            var IdentityClaim = new ClaimsIdentity(Claims,"Vouched Identity");
            var IdentityLicenseClaims = new ClaimsIdentity(LicenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] {IdentityClaim, IdentityLicenseClaims});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}