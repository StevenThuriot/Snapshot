using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Snap
{
    static class InternalSnapshot<T>
    {
        private static readonly Lazy<FieldInfo[]> _fields = new Lazy<FieldInfo[]>(() => typeof(T).GetFields());

        public static dynamic Take(T instance, bool mapProperties, bool mapFields)
        {
            if (Equals(instance, null)) return null;

            IDictionary<string, object> expando = new ExpandoObject();

            if (mapFields)
                foreach (var field in _fields.Value)
                {
                    var value = Map(field.GetValue(instance), mapProperties, true);
                    expando.Add(field.Name, value);
                }

            if (mapProperties)
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof(T)))
                {
                    var value = Map(property.GetValue(instance), true, mapFields);
                    expando.Add(property.Name, value);
                }

            return expando;
        }

        private static object Map(object value, bool mapProperties, bool mapFields)
        {
            if (value == null)
                return null;

            var type = value.GetType();

            if (type.IsValueType)
                return value;

            dynamic dynamicValue = value;
            return Snapshot.Take(dynamicValue, mapProperties, mapFields);
        }
    }
}
