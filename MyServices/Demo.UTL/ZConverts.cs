#region Information
/*
 * Author       : Zng Tfy
 * Email        : nvt87x@gmail.com
 * Phone        : +84 1645 515 010
 * ------------------------------- *
 * Create       : 26/10/2017 16:42
 * Update       : 26/10/2017 16:42
 * Checklist    : 1.0
 * Status       : OK
 */
#region License
/**************************************************************************************************************
 * Copyright © 2012-2017 SKG™ all rights reserved
 **************************************************************************************************************/
#endregion
#region Description
/**************************************************************************************************************
 * Convert data type
 **************************************************************************************************************/
#endregion
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.UTL
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
        public static T Xingleton<T>(string prefix = SpecialString.Blank)
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

        #endregion

        #region -- Fields --

        /// <summary>
        /// All instances
        /// </summary>
        private static Dictionary<string, object> _instances;

        #endregion
    }
}