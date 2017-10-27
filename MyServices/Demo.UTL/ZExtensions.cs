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
 * Extension methods
 **************************************************************************************************************/
#endregion
#endregion

using System.Linq;

namespace Demo.UTL
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ZExtensions
    {
        #region -- Methods --

        /// <summary>
        /// Copy all properties data (skip null properties) from source to destination
        /// </summary>
        /// <typeparam name="T">The class</typeparam>
        /// <param name="fr">Source</param>
        /// <param name="to">Destination</param>
        public static void Kopy<T>(this T fr, T to)
        {
            var a = typeof(T).GetProperties();
            var b = a.Where(x => x.GetValue(fr, null) != null);

            foreach (var i in b)
            {
                if (i.Name == DefaultValue.Id)
                {
                    continue;
                }

                var c = i.GetValue(fr, null);
                i.SetValue(to, c, null);
            }
        }

        #endregion
    }
}