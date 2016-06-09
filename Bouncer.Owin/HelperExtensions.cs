using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer.Owin
{
    internal static class HelperExtensions
    {
        public static T Get<T>(this IDictionary<string, object> dictionary, string key)
        {
            object o;
            if (dictionary.TryGetValue(key, out o))
            {
                if (o is T) return (T)o;
            }
            return default(T);
        }
    }
}
