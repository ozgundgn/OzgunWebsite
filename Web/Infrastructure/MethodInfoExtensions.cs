using Ardalis.GuardClauses;
using System.Reflection;

namespace GioWebsite.Web.Infrastructure
{
    public static class MethodInfoExtensions
    {
        public static bool IsAnonymous(this MethodInfo method)
        {
            var invalidChars = new[] { '<', '>' };
            return method.Name.Any(x => invalidChars.Contains(x));
        }

        public static void AnonymousMethod(this IGuardClause guardClass, Delegate input)
        {
            if (input.Method.IsAnonymous())
                throw new ArgumentException("The endpoint name must be specified when using anonymous handlers.");
        }
    }
}
