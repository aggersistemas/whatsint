using System.Globalization;
using System.Reflection;

namespace WhatsInt.Interface.Helpers
{
    public static class MapperHelper
    {
        public static T Map<T>(object? source)
        {
            if (source == null)
                return default!;

            var instanceToPopulate = (T)Activator.CreateInstance(typeof(T));

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                var value = source.GetPropValue(propertyInfo.Name);
                
                if (value == null)
                    continue;

                propertyInfo.SetValue(instanceToPopulate, propertyInfo.PropertyType.BaseType == typeof(Enum) ? value 
                    : Convert.ChangeType(value, propertyInfo.PropertyType, CultureInfo.InvariantCulture), null);
            }

            return instanceToPopulate;
        }

        public static object GetPropValue(this object src, string propName)
        {
            var prop = src.GetType().GetProperty(propName);

            return prop?.GetValue(src, null) ?? "";
        }
    }
}
