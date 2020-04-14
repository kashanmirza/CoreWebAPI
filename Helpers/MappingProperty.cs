using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI.Helpers
{


    public static class MappingProperty
    {
        public static void Map<TDest, TSource>(TDest destination, TSource source)
            where TSource : class
            where TDest : class
        {
            var destProperties = destination.GetType().GetProperties();

            var sourceProperties = source.GetType().GetProperties();

            var copyProperties = sourceProperties.Join(destProperties, x => x.Name, y => y.Name, (x, y) => x);
            foreach (var sourceProperty in copyProperties)
            {
                var prop = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                prop.SetValue(destination, sourceProperty.GetValue(source));
            }
        }
    }
}
