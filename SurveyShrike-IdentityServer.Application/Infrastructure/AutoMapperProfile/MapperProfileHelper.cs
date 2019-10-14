using SurveyShrike_IdentityServer.Application.Interfaces.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:04:25 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Infrastructure.AutoMapperProfile
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }

    public static class MapperProfileHelper
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select new Map
                    {
                        Source = type.GetInterfaces().First().GetGenericArguments().First(),
                        Destination = type
                    }).ToList();

            return mapsFrom;
        }

        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}
