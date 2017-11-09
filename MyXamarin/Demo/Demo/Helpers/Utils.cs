using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.Helpers
{
    /// <summary>
    /// Utils
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Check permissions
        /// </summary>
        /// <param name="permission">Permission</param>
        /// <returns>Return the result</returns>
        public static async Task<bool> CheckPermissions(Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            var request = false;

            if (status == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var title = $"{permission} Permission";
                    var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    var positive = "Settings";
                    var negative = "Maybe Later";

                    var task = Application.Current?.MainPage?.DisplayAlert(title, question,
                        positive, negative);
                    if (task == null)
                    {
                        return false;
                    }

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }

                request = true;
            }

            if (request || status != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    var title = $"{permission} Permission";
                    var question = $"To use the plugin the {permission} permission is required.";
                    var positive = "Settings";
                    var negative = "Maybe Later";

                    var task = Application.Current?.MainPage?.DisplayAlert(title, question,
                        positive, negative);
                    if (task == null)
                    {
                        return false;
                    }

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }

            return true;
        }
    }
}