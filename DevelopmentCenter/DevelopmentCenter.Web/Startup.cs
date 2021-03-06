﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using DevelopmentCenter.Web.App_Start;
[assembly: OwinStartup(typeof(DevelopmentCenter.Web.Startup))]
namespace DevelopmentCenter.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyConfig.RegisterDependencies(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Admin/Account/Login")
            });
        }
    }
}
