using System.Collections.Generic;
using System.Dynamic;

namespace Snap
{
    class DynamicDictionary : DynamicObject
    {
        private readonly IDictionary<string, object> _dictionary;

        public DynamicDictionary(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }
    }
}