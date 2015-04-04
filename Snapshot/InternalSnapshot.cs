using System;
using System.Collections.Generic;
using System.Reflection;

namespace Snap
{
    static class InternalSnapshot<T>
    {
        private static readonly Lazy<MemberInfo[]> _members = new Lazy<MemberInfo[]>(() => typeof(T).GetMembers(BindingFlags.Instance | BindingFlags.Public));

        public static dynamic Take(T instance, bool mapProperties, bool mapFields, bool convertEnumsToString)
        {
            if (Equals(instance, null)) return null;

            var dictionary = new Dictionary<string, object>();

            foreach (var member in _members.Value)
            {
                if (mapFields)
                {
                    var field = member as FieldInfo;
                    if (field != null)
                    {
                        var value = Map(field.GetValue(instance), mapProperties, true, convertEnumsToString);
                        dictionary.Add(field.Name, value);
                        continue;
                    }
                }

                if (mapProperties)
                {
                    var property = member as PropertyInfo;
                    if (property != null)
                    {
#if NET40
                        var propertyValue = property.GetValue(instance, null);
//#elif NET45
#else
                        var propertyValue = property.GetValue(instance);
#endif

                        var value = Map(propertyValue, true, mapFields, convertEnumsToString);
                        dictionary.Add(property.Name, value);
                    }
                }
            }

            return new DynamicDictionary(dictionary);
        }

        private static object Map(object value, bool mapProperties, bool mapFields, bool convertEnumsToString)
        {
            if (value == null)
                return null;

            var type = value.GetType();

            if (convertEnumsToString && type.IsEnum)
                return value.ToString();

            if (type.IsValueType)
                return value;

            if (type == typeof (string))
                return value;

            dynamic dynamicValue = value;
            return Snapshot.Take(dynamicValue, mapProperties, mapFields, convertEnumsToString);
        }
    }
}
