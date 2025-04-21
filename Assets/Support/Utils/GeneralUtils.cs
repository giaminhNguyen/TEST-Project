
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;


namespace UltimateHelper
{
    public static class GeneralUtility
    {
        public static bool IsConnectedInternet(this MonoBehaviour mono)
        {
            return Application.internetReachability switch
            {
                    NetworkReachability.ReachableViaLocalAreaNetwork   => true,
                    NetworkReachability.ReachableViaCarrierDataNetwork => true,
                    _                                                  => false
            };
        }

        /// <summary>
        ///     Kiểm tra xem ứng dụng đang chạy trên UnityEditor hay không.
        /// </summary>
        public static bool IsUnityEditor()
        {
            var check = false;
            #if UNITY_EDITOR
            check = true;
            #endif
            return check;
        }

        /// <summary>
        ///     Kiểm tra xem ứng dụng đang chạy trên UnityEditor hay không.
        /// </summary>
        public static bool IsUnityEditor(this MonoBehaviour mono)
        {
            var check = false;
            #if UNITY_EDITOR
            check = true;
            #endif
            return check;
        }

        public static async void DelayAction(float delayTime, Action action)
        {
            if (delayTime < 0)
            {
                await Task.Yield();
            }
            else
            {
                await Task.Delay((int)(delayTime * 1000));
            }
            
            action?.Invoke();
        }

        public static IEnumerator DelayActionInMainThread(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action.SafeInvoke();
        }

        public static void ShowMessage(string msg)
        {
            #if !UNITY_EDITOR && UNITY_ANDROID && USE_MESSAGE
            AndroidJavaObject @static =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject androidJavaObject = new AndroidJavaClass("android.widget.Toast");
            androidJavaObject.CallStatic<AndroidJavaObject>("makeText", new object[]
            {
                    @static,
                    msg,
                    androidJavaObject.GetStatic<int>("LENGTH_SHORT")
            }).Call("show", Array.Empty<object>());
            #endif
            Log(msg);
        }
        
        public static void SetTargetFPS(int fps)
        {
            Application.targetFrameRate = fps;
        }
        
        public static string CreateRichText(string text, Color? color = null, FontStyle? fontStyle = null, int? size = null, bool underline = false)
        {
            string richText = text;

            // Thay đổi màu văn bản
            if (color.HasValue)
            {
                richText = $"<color=#{ColorToHex(color.Value)}>{richText}</color>";
            }

            // Thay đổi kiểu chữ
            if (fontStyle.HasValue)
            {
                switch (fontStyle.Value)
                {
                    case FontStyle.Bold:
                        richText = $"<b>{richText}</b>";
                        break;
                    case FontStyle.Italic:
                        richText = $"<i>{richText}</i>";
                        break;
                    case FontStyle.BoldAndItalic:
                        richText = $"<b><i>{richText}</i></b>";
                        break;
                }
            }

            // Thay đổi kích thước văn bản
            if (size.HasValue)
            {
                richText = $"<size={size.Value}>{richText}</size>";
            }

            // Thêm gạch chân
            if (underline)
            {
                richText = $"<u>{richText}</u>";
            }

            return richText;
        }

        // Hàm chuyển đổi màu sang mã hex
        public static string ColorToHex(Color color)
        {
            return $"{(int)(color.r * 255):X2}{(int)(color.g * 255):X2}{(int)(color.b * 255):X2}";
        }
        
        public static string RemoveRichTextTags(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Regular expression to match rich text tags
            string pattern = @"<.*?>";
            return Regex.Replace(input, pattern, string.Empty);
        }
        
        public static string FormatCurrency(long amount)
        {
            if (amount >= 1_000_000_000_000)
                return (amount / 1_000_000_000_000f).ToString("0.#") + "T";
            if (amount >= 1_000_000_000)
                return (amount / 1_000_000_000f).ToString("0.#") + "B";
            if (amount >= 1_000_000)
                return (amount / 1_000_000f).ToString("0.#") + "M";
            if (amount >= 1_000)
                return (amount / 1_000f).ToString("0.#") + "K";

            return amount.ToString();
        }

        #if UNITY_EDITOR
        public static void SetupComponents<T>(GameObject objSearch,Action<T> action,bool inPrefab = true, bool inScene = true,bool allSceneBuild = false, bool includeInactive = true)
                where T : Component
        {
            
            if (inPrefab)
            {
                // Tìm kiếm trong Prefabs
                string[] guids = AssetDatabase.FindAssets("t:Prefab"); // Tìm tất cả các Prefabs trong project

                foreach (string guid in guids)
                {
                    string     path   = AssetDatabase.GUIDToAssetPath(guid);
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                    if (prefab != null)
                    {
                        // Lấy tất cả các component T trong prefab
                        T[] components = prefab.GetComponentsInChildren<T>(includeInactive);

                        foreach (T component in components)
                        {
                            action(component);
                        }
                    }
                }
            }

            if (inScene)
            {
                if (allSceneBuild)
                {
                    // Quét tất cả các scene đã tải trong dự án
                    int sceneCount =
                            SceneManager.sceneCountInBuildSettings; // Số lượng scene trong dự án (trong Build Settings)

                    for (int i = 0; i < sceneCount; i++)
                    {
                        string scenePath = SceneUtility.GetScenePathByBuildIndex(i); // Lấy đường dẫn đến scene
                        Scene  scene     = SceneManager.GetSceneByPath(scenePath);

                        // Tải scene nếu chưa được tải
                        if (!scene.isLoaded)
                        {
                            SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
                        }

                        // Quét qua các đối tượng trong scene
                        T[] objectsInScene = scene.GetRootGameObjects()
                                                  .SelectMany(go => go.GetComponentsInChildren<T>(includeInactive))
                                                  .ToArray();

                        foreach (var obj in objectsInScene)
                        {
                            action(obj);
                        }

                        // Nếu scene đã được tải thêm, bạn có thể unload scene sau khi quét
                        if (!scene.isLoaded)
                        {
                            SceneManager.UnloadSceneAsync(scene);
                        }
                    }
                }
            
                // Quét các đối tượng trong scene hiện tại
                T[] objectsInCurrentScene = GameObject.FindObjectsOfType<T>();

                foreach (var obj in objectsInCurrentScene)
                {
                    action(obj);
                }
            }
            
        }
        #endif
        #region Log

        public static void LogParams(params (string name, int value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            Log(textLog);
        }

        public static void LogParams(params (string name, float value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            Log(textLog);
        }

        public static void LogParams(params (string name, string value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            Log(textLog);
        }

        public static void LogParams(params (string name, bool value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            Log(textLog);
        }

        public static void LogErrorParams(params (string name, int value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            LogError(textLog);
        }

        public static void LogErrorParams(params (string name, float value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            LogError(textLog);
        }

        public static void LogErrorParams(params (string name, string value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            LogError(textLog);
        }

        public static void LogErrorParams(params (string name, bool value)[] parameters)
        {
            var textLog = "";

            foreach (var param in parameters)
            {
                textLog += $"{param.name}: {param.value}\n";
            }

            LogError(textLog);
        }
        
        public static void Log(object message)
        {
            #if USE_DEBUG_LOG
            Debug.Log(message);
            #endif
        }
        
        public static void LogWarning(object message)
        {
            #if USE_DEBUG_LOG
            Debug.LogWarning(message);
            #endif
        }
        
        public static void LogError(object message)
        {
            #if USE_DEBUG_LOG
            Debug.LogError(message);
            #endif
        }
        
        #endregion
    }
}