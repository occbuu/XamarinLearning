using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Google;
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

        #region -- PushSharp --

        /// <summary>
        /// Google notification
        /// </summary>
        /// <param name="id">Sender identify</param>
        /// <param name="key">Server key</param>
        /// <param name="package">Package name</param>
        /// <param name="tokens">List device tokens</param>
        /// <param name="title">Title message</param>
        /// <param name="text">Content message</param>
        /// <param name="module">Module name</param>
        public static void GoogleNotification(string id, string key, string package,
            List<string> tokens, string title, string text, string module)
        {
            /*deviceTokens.Clear();
            deviceTokens.Add("fj726Vx9JF4:APA91bGjnVRSvqS2cN2jIDRk-XRCjYbbpTlRNSFethDBJkxYJTqkhtOUbLlkDe3VPnt6sJQiHrCuq5nzFOjm2-s3is_Dzm4Z2OoX0L7wGvz12uREbZ16UDGe5Rxct70TnEmivvr_o0fG");
            serverKey = "AIzaSyBSN2QYqNMaGw9tY2V9fjmemNm1QTDniuU";
            packageName = "hello.com";*/

            var config = new GcmConfiguration(id, key, package);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Failed: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={e.Message}");
                        }
                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("GCM Notification Sent!");
            };

            // Start the broker
            gcmBroker.Start();

            var pushContent = "{ \"title\" : \"" + title + "\", \"text\" : \"" + text + "\", \"sound\" : \"true\" , \"Module\" : \"" + module + "\" ,\"additionalData\": {\"google.message_id\": \"0:1488356994305684%163a31bc163a31bc\",\"coldstart\": false,\"collapse_key\": \"com.hearti.walley\",\"foreground\": true } }";
            //pushContent = "{ \"title\" : \"" + title + "\", \"text\" : \"" + text + "\", \"Module\" : \"" + module + "\", \"sound\" : \"true\" }";

            var noti = new GcmNotification()
            {
                RegistrationIds = tokens,
                Notification = JObject.Parse(pushContent)
            };

            gcmBroker.QueueNotification(noti);

            // Stop the broker, wait for it to finish
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
        }

        /// <summary>
        /// Apple notification
        /// </summary>
        /// <param name="file">Full path file name</param>
        /// <param name="pass">Password of file</param>
        /// <param name="tokens">List device tokens</param>
        /// <param name="title">Title message</param>
        /// <param name="text">Content message</param>
        /// <param name="module">Module name</param>
        public static void AppleNotification(string file, string pass, List<string> tokens,
            string title, string text, string module)
        {
            /*var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            file = @"bin\Certificates\aps_development.cer";
            file = Path.Combine(baseDir, file);
            pass = "Pass1234";*/

            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, file, pass);

            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Console.WriteLine($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) =>
            {
                Console.WriteLine("Apple Notification Sent!");
            };

            // Start the broker
            apnsBroker.Start();

            var pushContent = "{ \"title\" : \"" + title + "\", \"text\" : \"" + text + "\", \"sound\" : \"true\" , \"Module\" : \"" + module + "\" ,\"additionalData\": {\"google.message_id\": \"0:1488356994305684%163a31bc163a31bc\",\"coldstart\": false,\"collapse_key\": \"com.hearti.walley\",\"foreground\": true } }";
            //pushContent = "{ \"title\" : \"" + title + "\", \"text\" : \"" + text + "\", \"Module\" : \"" + module + "\", \"sound\" : \"true\" }";

            foreach (var deviceToken in tokens)
            {
                // Queue a notification to send
                apnsBroker.QueueNotification(new ApnsNotification
                {
                    DeviceToken = deviceToken,
                    Payload = JObject.Parse(pushContent)
                });
            }

            // Stop the broker, wait for it to finish
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();
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