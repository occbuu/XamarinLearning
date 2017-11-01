using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        #region -- String image --

        /// <summary>
        /// Save base 64 string image to image
        /// </summary>
        /// <param name="fullPath">Full output path</param>
        /// <param name="img64">Base 64 string image</param>
        /// <returns>File name</returns>
        public static string SaveImage(string fullPath, string img64)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if (string.IsNullOrEmpty(img64))
            {
                return string.Empty;
            }

            var bytes = Convert.FromBase64String(img64);
            var tmp = new Bitmap(new MemoryStream(bytes));
            var file = Guid.NewGuid() + FileExtension.Jpg;
            var res = Path.Combine(fullPath, file);

            using (var i = tmp)
            {
                i.Save(res, ImageFormat.Jpeg);
            }

            return res;
        }

        /// <summary>
        /// Convert image to base 64 string image without format image
        /// </summary>
        /// <param name="fullPath">Full input path</param>
        /// <param name="fileName">File name</param>
        /// <returns>Return the base 64 string image</returns>
        public static string GetBase64(string fullPath, string fileName)
        {
            try
            {
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                var path = Path.Combine(fullPath, fileName);
                var res = GetBase64(path);

                return res;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert image to base 64 string image
        /// </summary>
        /// <param name="fileName">Full path with file name</param>
        /// <returns>Return the base 64 string image</returns>
        public static string StringImage(string fileName)
        {
            var a = GetBase64(fileName);

            if (!string.IsNullOrEmpty(a))
            {
                var b = string.Format(StringFormat.ImageBase64, a);
                return b;
            }

            return a;
        }

        /// <summary>
        /// Convert to base64 string from file name
        /// </summary>
        /// <param name="fileName">Full path with file name</param>
        /// <returns>Return the base64 string</returns>
        private static string GetBase64(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return string.Empty;
                }

                var a = File.ReadAllBytes(fileName);
                var b = Convert.ToBase64String(a);

                return b;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #endregion

        #region -- Fields --

        /// <summary>
        /// All instances
        /// </summary>
        private static Dictionary<string, object> _instances;

        #endregion
    }
}