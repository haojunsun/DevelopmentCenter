using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Configuration;
using Microsoft.Owin.Security;
using DevelopmentCenter.Infrastructure.Authentication;

namespace DevelopmentCenter.Infrastructure.Services
{
    public interface ISimpleAccountManager : IDependency
    {
        void Login(string userName);
        void Login(string userName, string userRole);
        bool LoginByPassword(string userName, string password);
        void Logout();
        IEnumerable<string> GetCurrentPermissions();
    }

    public class SimpleAccountManager : ISimpleAccountManager
    {
        private readonly Authentication.AuthenticationSection _secoAuthenticationSection;
        private readonly IAuthenticationManager _authentication;

        public SimpleAccountManager()
        {
            _secoAuthenticationSection = WebConfigurationManager.GetSection("Authentication") as Authentication.AuthenticationSection;
            _authentication = HttpContext.Current.Request.GetOwinContext().Authentication;
        }

        public void Login(string userName)
        {
            Login(userName, "Default");
        }

        public void Login(string userName, string userRole)
        {
            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userRole)
            }, "ApplicationCookie");

            _authentication.SignIn(identity);
        }

        public bool LoginByPassword(string userName, string password)
        {
            var userElement = GetUserElement(userName);

            if (userElement == null)
            {
                return false;
            }

            if (userElement.Password != password)
            {
                return false;
            }

            Login(userName);

            return true;
        }

        public void Logout()
        {
            _authentication.SignOut();
        }

        public IEnumerable<string> GetCurrentPermissions()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            var userElement = GetUserElement(userName);

            if (userElement == null)
            {
                return new String[0];
            }

            return userElement.Permissions.Split(',');
        }

        private UserElement GetUserElement(string userName)
        {
            //check for the specified name
            for (int i = 0; i < _secoAuthenticationSection.Users.Count; i++)
            {
                if (_secoAuthenticationSection.Users[i].Name == userName)
                {
                    return _secoAuthenticationSection.Users[i];
                }
            }
            //return null if nothing matched.
            return null;
        }
    }
}
