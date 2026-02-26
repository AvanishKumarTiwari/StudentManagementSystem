using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManagementSystem.Filters
{
    // Simple demo authorization filter that checks a demo cookie 'AuthRole'.
    // If the cookie is missing, it redirects to Admin/AdminLogin unless the
    // current action is AdminLogin or AdminRegister (login/register pages).
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DemoAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var http = context.HttpContext;
            var cookies = http.Request.Cookies;
            var role = cookies.ContainsKey("AuthRole") ? cookies["AuthRole"] : null;

            // allow unauthenticated access to login/register actions
            var route = context.RouteData.Values;
            var action = (route.ContainsKey("action") ? route["action"]?.ToString() : string.Empty) ?? string.Empty;
            var controller = (route.ContainsKey("controller") ? route["controller"]?.ToString() : string.Empty) ?? string.Empty;

            var allowed = string.Equals(controller, "Admin", StringComparison.OrdinalIgnoreCase)
                          && (string.Equals(action, "AdminLogin", StringComparison.OrdinalIgnoreCase)
                              || string.Equals(action, "AdminRegister", StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(role) && !allowed)
            {
                // redirect to admin login
                context.Result = new RedirectToActionResult("AdminLogin", "Admin", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
