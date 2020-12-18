using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Xperters.Core.Dynamic
{
    public abstract class ExtensibleObject : DynamicObject
    {
        protected ExtensibleObject()
        {
            Members = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            var found = Members.TryGetValue(name, out result);

            return found && result != null;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Members[binder.Name] = value;
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return from entry in Members
                   where entry.Value != null
                   select entry.Key;
        }

        public T GetValue<T>(string key)
        {
            object value;
            
            var found = Members.TryGetValue(key, out value);
            if (found)
            {
                return (T)value;
            }

            return default(T);
        }

        public void SetValue(string key, object value)
        {
            Members[key] = value;
        }

        public dynamic Extensions()
        {
            return this;
        }
    
        protected IDictionary<string, object> Members { get; private set; }
    }
}