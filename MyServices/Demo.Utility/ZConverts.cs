using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Utility
{
    /// <summary>
    /// Convert data type
    /// </summary>
    public static class ZConverts
    {
        #region -- Methods --

        /// <summary>
        /// Creates an instance of the specified type using that type's default constructor
        /// </summary>
        /// <typeparam name="T">Class type</typeparam>
        /// <param name="prefix">Prefix of key</param>
        /// <returns>Return the result</returns>
        public static T Xingleton<T>(string prefix = "")
        {
            var k = prefix + typeof(T).Name;

            if (_instances == null)
            {
                _instances = new Dictionary<string, object>();
            }

            var exists = _instances.Keys.Contains(k);
            if (!exists)
            {
                var o = Activator.CreateInstance(typeof(T), true);
                _instances.Add(k, o);
            }

            var res = (T)_instances[k];
            return res;
        }

        /// <summary>
        /// Get value if a property exist in object
        /// </summary>
        /// <param name="o">Object</param>
        /// <param name="p">Property name</param>
        /// <returns>Return the result</returns>
        public static object GetPropertyValue(this object o, string p)
        {
            object res = null;

            var tmp = o.GetType().GetProperty(p);
            if (tmp != null)
            {
                res = tmp.GetValue(o);
            }

            return res;
        }

        /// <summary>
        /// Set value if a property exist in object
        /// </summary>
        /// <param name="o">Object</param>
        /// <param name="p">Property name</param>
        /// <param name="value">Value need to set</param>
        public static void SetPropertyValue(this object o, string p, object value)
        {
            var tmp = o.GetType().GetProperty(p);
            if (tmp != null)
            {
                tmp.SetValue(o, value, null);
            }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// All instances
        /// </summary>
        private static Dictionary<string, object> _instances;

        #endregion
    }
}