using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace CeoHelper.Web.Attributes
{
    public class AuthenticatedConstraint : Attribute, IActionConstraint
    {
        public int Order => 1;
        public bool Accept(ActionConstraintContext context)
        {
            if (context.RouteContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }
    }
}
