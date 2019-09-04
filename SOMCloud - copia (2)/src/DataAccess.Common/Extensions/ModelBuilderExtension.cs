using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAccess.Common
{
    public static class ModelBuilderExtension
    {
        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration));
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
            {
                config.Map(modelBuilder);
            }
        }

        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly.GetTypes().Where(x => !x.IsInterface
                                                 && !x.IsAbstract
                                                 && x.GetInterfaces().Any(y => y != null && y == mappingInterface));
        }
    }
}
