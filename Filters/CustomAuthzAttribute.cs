using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVCIdentityDemoApp.Models;

namespace MVCIdentityDemoApp.Filters
{
    public class CustomAuthzAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorizeCore = base.AuthorizeCore(httpContext);
            if (authorizeCore)
            {
                string userName = httpContext.User.Identity.Name;

                ApplicationUserManager userManager = httpContext.GetOwinContext().Get<ApplicationUserManager>();
                ApplicationUser applicationUser = userManager.FindByName(userName);
                return applicationUser.EmailConfirmed;
            }

            return false;
        }
    }
}