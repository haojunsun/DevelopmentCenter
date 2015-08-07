using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevelopmentCenter.Web.Helpers
{
    public class ControllerHelper : Controller
    {
        public ControllerHelper()
        {
        }

        public ContentResult CResult(int statusCode, string message)
        {
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            return Content(message);
        }


        public ActionResult JumpUrl(string page, string message)
        {
            return Content("<script>alert('" + message + "');window.location.href='" + Url.Action(page) + "';</script>");
        }
    }
}