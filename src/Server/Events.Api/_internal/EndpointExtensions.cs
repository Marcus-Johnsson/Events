﻿namespace Events.Api._internal
{
    public static class EndpointExtensions
    {
        public static void MapEndpoints<T>(this IApplicationBuilder app)
        {
            MapEndpoints(app, typeof(T));
        }

        public static void MapEndpoints(this IApplicationBuilder app, Type T)
        {
            var endpointTypes = T.Assembly.DefinedTypes
                .Where(x => !x.IsAbstract
                            && !x.IsInterface
                            && typeof(IEndpoint).IsAssignableFrom(x));

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpoint.MapEndpoint))!
                    .Invoke(null, new object[] { app });
            }
        }
    }
}
