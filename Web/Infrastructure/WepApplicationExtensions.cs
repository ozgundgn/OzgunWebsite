using System.Reflection;

namespace Web.Infrastructure
{
    public static class WepApplicationExtensions
    {
        public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase endpoint)
        {
            var groupName = endpoint.GetType().Name;
            return app.MapGroup($"api/{groupName}")
                         .WithGroupName(groupName)
                         .WithTags(groupName)
                         .WithOpenApi();
        }
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var endpoints = assembly.DefinedTypes.Where(x => typeof(EndpointGroupBase).IsAssignableFrom(x) && x != typeof(EndpointGroupBase));

            // var endpointGroupTypes = assembly.GetExportedTypes()
            //.Where(t => t.IsSubclassOf(typeof(EndpointGroupBase)));

            if (endpoints != null && endpoints.Any())
            {
                foreach (var endpoint in endpoints)
                {
                    if (Activator.CreateInstance(endpoint) is EndpointGroupBase instance)
                    {
                        instance.Map(app);
                    }
                }
            }

            return app;
        }
    }
}
