#if ANDROID
using Android;
using Android.Content.PM;
using AndroidX.Core.App;
using AndroidX.Core.Content;
#endif

namespace Finance.Classes.AppSettings.PermissionRequest
{
    public static class PermissionsRequests
    {
        public static void PermissionStorageRequest()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android && OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                #if ANDROID
                    var activity = Platform.CurrentActivity ?? throw new NullReferenceException("Current activity is null");
                    if (ContextCompat.CheckSelfPermission(activity, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
                    {
                        ActivityCompat.RequestPermissions(activity, new[] { Manifest.Permission.ReadExternalStorage }, 1);
                    }
                #endif
            }
        }
    }
}
