using System;
using UnityEngine;
using UnityEngine.Android;

namespace UltimateHelper
{
    public static class AndroidUtils
    {
        /// <summary>
        /// Kiểm tra xem một quyền Android cụ thể đã được cấp hay chưa.
        /// </summary>
        /// <param name="permission">Quyền Android cần kiểm tra (ví dụ: "android.permission.CAMERA").</param>
        /// <returns>True nếu quyền đã được cấp, ngược lại là false.</returns>
        public static bool IsPermissionGranted(string permission)
        {
            
            #if UNITY_ANDROID && !UNITY_EDITOR
        return Permission.HasUserAuthorizedPermission(permission);
            #else
            return
                    true; // Giả định rằng quyền đã được cấp trong môi trường Editor hoặc trên các nền tảng không phải Android.
            #endif
        }

        /// <summary>
        /// Yêu cầu một quyền Android cụ thể.
        /// </summary>
        /// <param name="permission">Quyền Android cần yêu cầu (ví dụ: "android.permission.CAMERA").</param>
        /// <param name="onGranted">Hàm callback được gọi khi quyền được cấp.</param>
        /// <param name="onDenied">Hàm callback được gọi khi quyền bị từ chối.</param>
        public static void ExecuteIfHasPermission(string permission, Action onGranted = null, Action onDenied = null)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            if (IsPermissionGranted(permission))
            {
                onGranted?.Invoke();
            }
            else
            {
                RequestPermission(permission, onGranted, onDenied);
            }
            #else
            onGranted?.Invoke();
            #endif
        }

        public static void RequestPermission(string permission, Action onGranted = null, Action onDenied = null)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            Permission.RequestUserPermission(permission, GetCallbacks(onGranted, onDenied,onDenied));
            #else
            if (IsPermissionGranted(permission))
            {
                onGranted?.Invoke();
            }
            else
            {
                onDenied?.Invoke();
            }
            #endif
        }
        
        private static PermissionCallbacks GetCallbacks(Action onGranted = null, Action onDenied = null,Action onDeniedAndDontAskAgain = null)
        {
            PermissionCallbacks callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += (_) => onGranted?.Invoke();
            callbacks.PermissionDenied += (_) => onDenied?.Invoke();
            callbacks.PermissionDeniedAndDontAskAgain += (_) => onDeniedAndDontAskAgain?.Invoke();
            return callbacks;
        }


        /// <summary>
        /// Yêu cầu nhiều quyền Android.
        /// </summary>
        /// <param name="permissions">Danh sách các quyền Android cần yêu cầu.</param>
        /// <param name="onAllGranted">Hàm callback được gọi khi tất cả các quyền được cấp.</param>
        /// <param name="onSomeDenied">Hàm callback được gọi khi một hoặc nhiều quyền bị từ chối.</param>
        public static void ExecuteIfHasPermissions(string[] permissions, Action onAllGranted = null,
                                                   Action   onSomeDenied = null)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            bool allGranted = true;

            foreach (string permission in permissions)
            {
                if (!IsPermissionGranted(permission))
                {
                    allGranted = false;
                    break;
                }
            }

            if (allGranted)
            {
                onAllGranted?.Invoke();
            }
            else
            {
                RequestMultiplePermissionsAsync(permissions, onAllGranted, onSomeDenied);
            }
            #else
            onAllGranted?.Invoke();
            #endif
        }

        private static void RequestMultiplePermissionsAsync(string[] permissions, Action onAllGranted,
                                                                  Action   onSomeDenied)
        {
            foreach (string permission in permissions)
            {
                if (IsPermissionGranted(permission)) continue;
                RequestPermission(permission, null, null);
            }

            bool allGranted = true;

            foreach (string permission in permissions)
            {
                if (IsPermissionGranted(permission))
                {
                    continue;
                }

                allGranted = false;

                break;
            }

            if (allGranted)
            {
                onAllGranted?.Invoke();
            }
            else
            {
                onSomeDenied?.Invoke();
            }
        }

        /// <summary>
        /// Kiểm tra phiên bản API Android hiện tại.
        /// </summary>
        /// <returns>Phiên bản API Android dưới dạng số nguyên.</returns>
        public static int GetAndroidAPILevel()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
            #else
            return -1; // Trả về -1 nếu không chạy trên Android.
            #endif
        }

        /// <summary>
        /// Kiểm tra xem một define symbol có tồn tại hay không.
        /// </summary>
        /// <param name="symbol">Tên của define symbol.</param>
        /// <returns>True nếu symbol tồn tại, ngược lại là false.</returns>
        public static bool IsDefineSymbolPresent(string symbol)
        {
            #if UNITY_EDITOR
            var defines =
                    UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(UnityEditor.BuildTargetGroup.Android);

            return defines.Contains(symbol);
            #else
                return false; // Chỉ hỗ trợ kiểm tra trong Unity Editor.
            #endif
        }

        /// <summary>
        /// Thêm một define symbol vào cấu hình build.
        /// </summary>
        /// <param name="symbol">Tên của define symbol cần thêm.</param>
        public static void AddDefineSymbol(string symbol)
        {
            #if UNITY_EDITOR
            var buildTargetGroup = UnityEditor.BuildTargetGroup.Android;
            var defines          = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            if (!defines.Contains(symbol))
            {
                defines += ";" + symbol;
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
            }
            #endif
        }

        /// <summary>
        /// Xóa một define symbol khỏi cấu hình build.
        /// </summary>
        /// <param name="symbol">Tên của define symbol cần xóa.</param>
        public static void RemoveDefineSymbol(string symbol)
        {
            #if UNITY_EDITOR
            var buildTargetGroup = UnityEditor.BuildTargetGroup.Android;
            var defines          = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            if (defines.Contains(symbol))
            {
                defines = defines.Replace(symbol, "").Replace(";;", ";").Trim(';');
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
            }
            #endif
        }
        
        /// <summary>
        /// Kiểm tra xem ứng dụng đang chạy trên Android hay không.
        /// </summary>
        public static bool IsAndroid()
        {
            return Application.platform == RuntimePlatform.Android;
        }
        
        public static string GetPermissionErrorMessage(string permission) => $"{permission} runtime permission missing in AndroidManifest.xml or user did not grant the permission.";

        #region Flashlight
        
        /// <summary>
        /// Bật hoặc tắt đèn flash của thiết bị.
        /// </summary>
        /// <param name="enable">True để bật, False để tắt.</param>
        public static void SetFlashlight(bool enable)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                using (var cameraManager = new AndroidJavaClass("android.hardware.camera2.CameraManager"))
                using (var unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var context = unityActivity.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    string cameraId = cameraManager.Call<string[]>("getCameraIdList")[0];
                    cameraManager.Call("setTorchMode", cameraId, enable);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"SetFlashlight failed: {e.Message}");
            }
            #else
            Debug.LogWarning("SetFlashlight is not supported outside Android runtime.");
            #endif
        }


        #endregion

        #region Vibration
        /// <summary>
        /// Kích hoạt rung trên thiết bị trong một khoảng thời gian nhất định.
        /// </summary>
        /// <param name="milliseconds">Thời gian rung tính bằng mili-giây.</param>
        public static void Vibrate(int milliseconds)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                using (var unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (var context = unityActivity.GetStatic<AndroidJavaObject>("currentActivity"))
                using (var vibrator = context.Call<AndroidJavaObject>("getSystemService", "vibrator"))
                {
                    if (vibrator.Call<bool>("hasVibrator"))
                    {
                        vibrator.Call("vibrate", milliseconds);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Vibrate failed: {e.Message}");
            }
            #else
            Handheld.Vibrate();
            #endif
        }


        #endregion
        
        #region permissions

        public const string ACCEPT_HANDOVER = "android.permission.ACCEPT_HANDOVER";


        public const string ACCESS_CHECKIN_PROPERTIES = "android.permission.ACCESS_CHECKIN_PROPERTIES";


        public const string ACCESS_COARSE_LOCATION = "android.permission.ACCESS_COARSE_LOCATION";


        public const string ACCESS_FINE_LOCATION = "android.permission.ACCESS_FINE_LOCATION";


        public const string ACCESS_LOCATION_EXTRA_COMMANDS = "android.permission.ACCESS_LOCATION_EXTRA_COMMANDS";


        public const string ACCESS_NETWORK_STATE = "android.permission.ACCESS_NETWORK_STATE";


        public const string ACCESS_NOTIFICATION_POLICY = "android.permission.ACCESS_NOTIFICATION_POLICY";


        public const string ACCESS_WIFI_STATE = "android.permission.ACCESS_WIFI_STATE";


        public const string ACCOUNT_MANAGER = "android.permission.ACCOUNT_MANAGER";


        public const string ADD_VOICEMAIL = "com.android.voicemail.permission.ADD_VOICEMAIL";


        public const string ANSWER_PHONE_CALLS = "android.permission.ANSWER_PHONE_CALLS";


        public const string BATTERY_STATS = "android.permission.BATTERY_STATS";


        public const string BIND_ACCESSIBILITY_SERVICE = "android.permission.BIND_ACCESSIBILITY_SERVICE";


        public const string BIND_APPWIDGET = "android.permission.BIND_APPWIDGET";


        public const string BIND_AUTOFILL_SERVICE = "android.permission.BIND_AUTOFILL_SERVICE";

        [Obsolete]
        public const string BIND_CARRIER_MESSAGING_SERVICE = "android.permission.BIND_CARRIER_MESSAGING_SERVICE";


        public const string BIND_CARRIER_SERVICES = "android.permission.BIND_CARRIER_SERVICES";


        public const string BIND_CHOOSER_TARGET_SERVICE = "android.permission.BIND_CHOOSER_TARGET_SERVICE";


        public const string BIND_CONDITION_PROVIDER_SERVICE = "android.permission.BIND_CONDITION_PROVIDER_SERVICE";


        public const string BIND_DEVICE_ADMIN = "android.permission.BIND_DEVICE_ADMIN";


        public const string BIND_DREAM_SERVICE = "android.permission.BIND_DREAM_SERVICE";


        public const string BIND_INCALL_SERVICE = "android.permission.BIND_INCALL_SERVICE";


        public const string BIND_INPUT_METHOD = "android.permission.BIND_INPUT_METHOD";


        public const string BIND_MIDI_DEVICE_SERVICE = "android.permission.BIND_MIDI_DEVICE_SERVICE";


        public const string BIND_NFC_SERVICE = "android.permission.BIND_NFC_SERVICE";


        public const string BIND_NOTIFICATION_LISTENER_SERVICE =
                "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE";

        public const string POST_NOTIFICATIONS = "android.permission.POST_NOTIFICATIONS";
        
        public const string SCHEDULE_EXACT_ALARM  = "android.permission.SCHEDULE_EXACT_ALARM ";

        public const string BIND_PRINT_SERVICE = "android.permission.BIND_PRINT_SERVICE";


        public const string BIND_QUICK_SETTINGS_TILE = "android.permission.BIND_QUICK_SETTINGS_TILE";


        public const string BIND_REMOTEVIEWS = "android.permission.BIND_REMOTEVIEWS";


        public const string BIND_SCREENING_SERVICE = "android.permission.BIND_SCREENING_SERVICE";


        public const string BIND_TELECOM_CONNECTION_SERVICE =
                "android.permission.BIND_TELECOM_CONNECTION_SERVICE";


        public const string BIND_TEXT_SERVICE = "android.permission.BIND_TEXT_SERVICE";


        public const string BIND_TV_INPUT = "android.permission.BIND_TV_INPUT";


        public const string BIND_VISUAL_VOICEMAIL_SERVICE = "android.permission.BIND_VISUAL_VOICEMAIL_SERVICE";


        public const string BIND_VOICE_INTERACTION = "android.permission.BIND_VOICE_INTERACTION";


        public const string BIND_VPN_SERVICE = "android.permission.BIND_VPN_SERVICE";


        public const string BIND_VR_LISTENER_SERVICE = "android.permission.BIND_VR_LISTENER_SERVICE";


        public const string BIND_WALLPAPER = "android.permission.BIND_WALLPAPER";


        public const string BLUETOOTH = "android.permission.BLUETOOTH";


        public const string BLUETOOTH_ADMIN = "android.permission.BLUETOOTH_ADMIN";


        public const string BLUETOOTH_PRIVILEGED = "android.permission.BLUETOOTH_PRIVILEGED";


        public const string BODY_SENSORS = "android.permission.BODY_SENSORS";


        public const string BROADCAST_PACKAGE_REMOVED = "android.permission.BROADCAST_PACKAGE_REMOVED";


        public const string BROADCAST_SMS = "android.permission.BROADCAST_SMS";


        public const string BROADCAST_STICKY = "android.permission.BROADCAST_STICKY";


        public const string BROADCAST_WAP_PUSH = "android.permission.BROADCAST_WAP_PUSH";


        public const string CALL_PHONE = "android.permission.CALL_PHONE";


        public const string CALL_PRIVILEGED = "android.permission.CALL_PRIVILEGED";


        public const string CAMERA = "android.permission.CAMERA";


        public const string CAPTURE_AUDIO_OUTPUT = "android.permission.CAPTURE_AUDIO_OUTPUT";


        public const string CAPTURE_SECURE_VIDEO_OUTPUT = "android.permission.CAPTURE_SECURE_VIDEO_OUTPUT";


        public const string CAPTURE_VIDEO_OUTPUT = "android.permission.CAPTURE_VIDEO_OUTPUT";


        public const string CHANGE_COMPONENT_ENABLED_STATE =
                "android.permission.CHANGE_COMPONENT_ENABLED_STATE";


        public const string CHANGE_CONFIGURATION = "android.permission.CHANGE_CONFIGURATION";


        public const string CHANGE_NETWORK_STATE = "android.permission.CHANGE_NETWORK_STATE";


        public const string CHANGE_WIFI_MULTICAST_STATE = "android.permission.CHANGE_WIFI_MULTICAST_STATE";


        public const string CHANGE_WIFI_STATE = "android.permission.CHANGE_WIFI_STATE";


        public const string CLEAR_APP_CACHE = "android.permission.CLEAR_APP_CACHE";


        public const string CONTROL_LOCATION_UPDATES = "android.permission.CONTROL_LOCATION_UPDATES";


        public const string DELETE_CACHE_FILES = "android.permission.DELETE_CACHE_FILES";


        public const string DELETE_PACKAGES = "android.permission.DELETE_PACKAGES";


        public const string DIAGNOSTIC = "android.permission.DIAGNOSTIC";


        public const string DISABLE_KEYGUARD = "android.permission.DISABLE_KEYGUARD";


        public const string DUMP = "android.permission.DUMP";


        public const string EXPAND_STATUS_BAR = "android.permission.EXPAND_STATUS_BAR";


        public const string FACTORY_TEST = "android.permission.FACTORY_TEST";


        public const string FOREGROUND_SERVICE = "android.permission.FOREGROUND_SERVICE";

        //Is not listed in documentation (15.08.2018)

        public const string FLASHLIGHT = "android.permission.FLASHLIGHT";


        public const string GET_ACCOUNTS = "android.permission.GET_ACCOUNTS";


        public const string GET_ACCOUNTS_PRIVILEGED = "android.permission.GET_ACCOUNTS_PRIVILEGED";


        public const string GET_PACKAGE_SIZE = "android.permission.GET_PACKAGE_SIZE";

        [Obsolete]
        public const string GET_TASKS = "android.permission.GET_TASKS";


        public const string GLOBAL_SEARCH = "android.permission.GLOBAL_SEARCH";


        public const string INSTALL_LOCATION_PROVIDER = "android.permission.INSTALL_LOCATION_PROVIDER";


        public const string INSTALL_PACKAGES = "android.permission.INSTALL_PACKAGES";


        public const string INSTALL_SHORTCUT = "com.android.launcher.permission.INSTALL_SHORTCUT";


        public const string INSTANT_APP_FOREGROUND_SERVICE
                = "com.android.launcher.permission.INSTANT_APP_FOREGROUND_SERVICE";


        public const string INTERNET = "android.permission.INTERNET";


        public const string KILL_BACKGROUND_PROCESSES = "android.permission.KILL_BACKGROUND_PROCESSES";


        public const string LOCATION_HARDWARE = "android.permission.LOCATION_HARDWARE";


        public const string MANAGE_DOCUMENTS = "android.permission.MANAGE_DOCUMENTS";


        public const string MANAGE_OWN_CALLS = "android.permission.MANAGE_OWN_CALLS";


        public const string MASTER_CLEAR = "android.permission.MASTER_CLEAR";


        public const string MEDIA_CONTENT_CONTROL = "android.permission.MEDIA_CONTENT_CONTROL";


        public const string MODIFY_AUDIO_SETTINGS = "android.permission.MODIFY_AUDIO_SETTINGS";


        public const string MODIFY_PHONE_STATE = "android.permission.MODIFY_PHONE_STATE";


        public const string MOUNT_FORMAT_FILESYSTEMS = "android.permission.MOUNT_FORMAT_FILESYSTEMS";


        public const string MOUNT_UNMOUNT_FILESYSTEMS = "android.permission.MOUNT_UNMOUNT_FILESYSTEMS";


        public const string NFC = "android.permission.NFC";


        public const string NFC_TRANSACTION_EVENT = "android.permission.NFC_TRANSACTION_EVENT";


        public const string PACKAGE_USAGE_STATS = "android.permission.PACKAGE_USAGE_STATS";

        [Obsolete]
        public const string PERSISTENT_ACTIVITY = "android.permission.PERSISTENT_ACTIVITY";


        public const string PROCESS_OUTGOING_CALLS = "android.permission.PROCESS_OUTGOING_CALLS";


        public const string READ_CALENDAR = "android.permission.READ_CALENDAR";


        public const string READ_CALL_LOG = "android.permission.READ_CALL_LOG";


        public const string READ_CONTACTS = "android.permission.READ_CONTACTS";


        public const string READ_EXTERNAL_STORAGE = "android.permission.READ_EXTERNAL_STORAGE";


        public const string READ_FRAME_BUFFER = "android.permission.READ_FRAME_BUFFER";

        [Obsolete]
        public const string READ_INPUT_STATE = "android.permission.READ_INPUT_STATE";


        public const string READ_LOGS = "android.permission.READ_LOGS";


        public const string READ_PHONE_NUMBERS = "android.permission.READ_PHONE_NUMBERS";


        public const string READ_PHONE_STATE = "android.permission.READ_PHONE_STATE";


        public const string READ_SMS = "android.permission.READ_SMS";


        public const string READ_SYNC_SETTINGS = "android.permission.READ_SYNC_SETTINGS";


        public const string READ_SYNC_STATS = "android.permission.READ_SYNC_STATS";


        public const string READ_VOICEMAIL = "com.android.voicemail.permission.READ_VOICEMAIL";


        public const string REBOOT = "android.permission.REBOOT";


        public const string RECEIVE_BOOT_COMPLETED = "android.permission.RECEIVE_BOOT_COMPLETED";


        public const string RECEIVE_MMS = "android.permission.RECEIVE_MMS";


        public const string RECEIVE_SMS = "android.permission.RECEIVE_SMS";


        public const string RECEIVE_WAP_PUSH = "android.permission.RECEIVE_WAP_PUSH";


        public const string RECORD_AUDIO = "android.permission.RECORD_AUDIO";


        public const string REORDER_TASKS = "android.permission.REORDER_TASKS";


        public const string REQUEST_COMPANION_RUN_IN_BACKGROUND
                = "android.permission.REQUEST_COMPANION_RUN_IN_BACKGROUND";


        public const string REQUEST_COMPANION_USE_DATA_IN_BACKGROUND
                = "android.permission.REQUEST_COMPANION_USE_DATA_IN_BACKGROUND";


        public const string REQUEST_DELETE_PACKAGES = "android.permission.REQUEST_DELETE_PACKAGES";


        public const string REQUEST_IGNORE_BATTERY_OPTIMIZATIONS =
                "android.permission.REQUEST_IGNORE_BATTERY_OPTIMIZATIONS";


        public const string REQUEST_INSTALL_PACKAGES = "android.permission.REQUEST_INSTALL_PACKAGES";

        [Obsolete]
        public const string RESTART_PACKAGES = "android.permission.RESTART_PACKAGES";


        public const string SEND_RESPOND_VIA_MESSAGE = "android.permission.SEND_RESPOND_VIA_MESSAGE";


        public const string SEND_SMS = "android.permission.SEND_SMS";


        public const string SET_ALARM = "com.android.alarm.permission.SET_ALARM";


        public const string SET_ALWAYS_FINISH = "android.permission.SET_ALWAYS_FINISH";


        public const string SET_ANIMATION_SCALE = "android.permission.SET_ANIMATION_SCALE";


        public const string SET_DEBUG_APP = "android.permission.SET_DEBUG_APP";

        [Obsolete]
        public const string SET_PREFERRED_APPLICATIONS = "android.permission.SET_PREFERRED_APPLICATIONS";


        public const string SET_PROCESS_LIMIT = "android.permission.SET_PROCESS_LIMIT";


        public const string SET_TIME = "android.permission.SET_TIME";


        public const string SET_TIME_ZONE = "android.permission.SET_TIME_ZONE";


        public const string SET_WALLPAPER = "android.permission.SET_WALLPAPER";

        public const string SET_WALLPAPER_HINTS = "android.permission.SET_WALLPAPER_HINTS";


        public const string SIGNAL_PERSISTENT_PROCESSES = "android.permission.SIGNAL_PERSISTENT_PROCESSES";


        public const string STATUS_BAR = "android.permission.STATUS_BAR";


        public const string SYSTEM_ALERT_WINDOW = "android.permission.SYSTEM_ALERT_WINDOW";


        public const string TRANSMIT_IR = "android.permission.TRANSMIT_IR";

        /// <summary>
        /// Permission is no longer supported. Do not use!
        /// </summary>
        [Obsolete]
        public const string UNINSTALL_SHORTCUT = "com.android.launcher.permission.UNINSTALL_SHORTCUT";


        public const string UPDATE_DEVICE_STATS = "android.permission.UPDATE_DEVICE_STATS";


        public const string USE_BIOMETRIC = "android.permission.USE_BIOMETRIC";

        /// <summary>
        /// This constant was deprecated in API level 28. Applications should request USE_BIOMETRIC instead.
        /// </summary>
        [Obsolete]
        public const string USE_FINGERPRINT = "android.permission.USE_FINGERPRINT";


        public const string USE_SIP = "android.permission.USE_SIP";


        public const string VIBRATE = "android.permission.VIBRATE";


        public const string WAKE_LOCK = "android.permission.WAKE_LOCK";


        public const string WRITE_APN_SETTINGS = "android.permission.WRITE_APN_SETTINGS";


        public const string WRITE_CALENDAR = "android.permission.WRITE_CALENDAR";


        public const string WRITE_CALL_LOG = "android.permission.WRITE_CALL_LOG";


        public const string WRITE_CONTACTS = "android.permission.WRITE_CONTACTS";


        public const string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";


        public const string WRITE_GSERVICES = "android.permission.WRITE_GSERVICES";


        public const string WRITE_SECURE_SETTINGS = "android.permission.WRITE_SECURE_SETTINGS";


        public const string WRITE_SETTINGS = "android.permission.WRITE_SETTINGS";


        public const string WRITE_SYNC_SETTINGS = "android.permission.WRITE_SYNC_SETTINGS";


        public const string WRITE_VOICEMAIL = "com.android.voicemail.permission.WRITE_VOICEMAIL";

        #endregion
    }
}