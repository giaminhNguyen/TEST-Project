#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UltimateHelper
{
    public class SRPBatchingCompatibility : EditorWindow
    {
        private List<ShaderInfo> incompatibleShaders = new();
        private Vector2          scrollPos;
        private bool             showWarnings      = true;
        private bool             showErrors        = true;
        private bool             showNotCompatible = true;

        [MenuItem("UltimateTools/Check SRP Batching Compatibility")]
        public static void ShowWindow()
        {
            GetWindow<SRPBatchingCompatibility>("SRP Batching Compatibility Checker");
        }

        private void OnGUI()
        {
            var padding     = 15;
            var windowWidth = position.width - 2 * padding;

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            showWarnings      = GUILayout.Toggle(showWarnings, "Warning", GUILayout.Width(100));
            showErrors        = GUILayout.Toggle(showErrors, "Error", GUILayout.Width(100));
            showNotCompatible = GUILayout.Toggle(showNotCompatible, "Not Compatible", GUILayout.Width(150));
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Scan Shaders", GUILayout.Height(30)))
            {
                FindShader();
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Result Shaders:", EditorStyles.boldLabel);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            var filteredShaders = incompatibleShaders
                                 .Where(s => (showWarnings && s.HasWarning) || (showErrors && s.HasError)
                                                                            || (showNotCompatible && s.NotCompatible))
                                 .ToList();

            var columns   = 2; // Display two items per row
            var spacing   = 10f;
            var itemWidth = (windowWidth - (columns - 1) * spacing) / columns;
            var itemStyle = new GUIStyle(GUI.skin.button)
            {
                    richText  = true,
                    alignment = TextAnchor.MiddleCenter,
                    border    = new(2, 2, 2, 2)
            };

            GUILayout.BeginHorizontal();
            GUILayout.Space(padding);
            GUILayout.BeginVertical();

            for (var i = 0; i < filteredShaders.Count; i += columns)
            {
                EditorGUILayout.BeginHorizontal();

                for (var j = 0; j < columns; j++)
                {
                    if (i + j < filteredShaders.Count)
                    {
                        var shaderInfo = filteredShaders[i + j];

                        var textMess = "";

                        // Display warnings and errors
                        if (shaderInfo.HasWarning)
                        {
                            textMess += "[<color=yellow>WARNING</color>]";
                        }

                        if (shaderInfo.HasError)
                        {
                            textMess += "[<color=red>ERROR</color>]";
                        }

                        if (GUILayout.Button($"<color=white>{shaderInfo.Name}</color>      <color=white>{textMess}</color>",
                                    itemStyle,
                                    GUILayout.Width(itemWidth)))
                        {
                            PingAndLoadShader(shaderInfo.Path);
                        }

                        if (j < columns - 1)
                        {
                            GUILayout.Space(spacing);
                        }
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.Space(padding);
            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"Total shaders: {filteredShaders.Count}", EditorStyles.boldLabel);
        }

        public static bool CheckShaderForInstancing(Shader shader)
        {
            if (shader == null)
            {
                return false;
            }

            // Lấy đường dẫn file của shader
            var shaderPath = AssetDatabase.GetAssetPath(shader);

            // Đọc và kiểm tra nội dung của shader
            if (CheckShaderFileForInstancing(shaderPath))
            {
                return true;
            }

            return false;
        }

        private static bool IsShaderGraph(Shader shader)
        {
            // Kiểm tra xem shader có phải là Shader Graph
            var path = AssetDatabase.GetAssetPath(shader);

            return path.EndsWith(".shadergraph") || path.EndsWith(".shadersubgraph");
        }

        private static bool CheckShaderFileForInstancing(string shaderPath)
        {
            if (string.IsNullOrEmpty(shaderPath) || !File.Exists(shaderPath))
            {
                return false;
            }

            // Đọc nội dung của file shader
            var shaderLines = File.ReadAllLines(shaderPath);

            foreach (var line in shaderLines)
            {
                if (line.Contains("CBUFFER_START") ||
                    line.Contains("CBUFFER_END") || line.Contains("cbuffer UnityPerMaterial"))
                {
                    return true;
                }

                // Kiểm tra các file được include
                if (line.StartsWith("#include"))
                {
                    var includePath = GetIncludedFilePath(line, shaderPath);

                    if (CheckShaderFileForInstancing(includePath))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static string GetIncludedFilePath(string includeLine, string shaderPath)
        {
            var includeFile     = includeLine.Split('"')[1];         // Lấy đường dẫn file trong dấu ngoặc kép
            var shaderDirectory = Path.GetDirectoryName(shaderPath); // Thư mục chứa shader
            var includeFilePath = Path.Combine(shaderDirectory, includeFile);

            // Nếu không tìm thấy file, thử tìm trong thư mục Standard Unity Shaders
            if (!File.Exists(includeFilePath))
            {
                includeFilePath = Path.Combine("Assets", includeFile);
            }

            return includeFilePath;
        }

        private void FindShader()
        {
            incompatibleShaders.Clear();
            var shaderGuids = AssetDatabase.FindAssets("t:Shader");

            foreach (var guid in shaderGuids)
            {
                var path   = AssetDatabase.GUIDToAssetPath(guid);
                var shader = AssetDatabase.LoadAssetAtPath<Shader>(path);

                if (!shader || IsDefaultShader(path) || IsShaderGraph(shader))
                {
                    continue;
                }

                var isNotCompatible = !CheckShaderForInstancing(shader);

                if (ShaderUtil.ShaderHasWarnings(shader) || ShaderUtil.ShaderHasError(shader) || isNotCompatible)
                {
                    incompatibleShaders.Add(new()
                    {
                            Name          = shader.name,
                            Path          = path,
                            HasWarning    = ShaderUtil.ShaderHasWarnings(shader),
                            HasError      = ShaderUtil.ShaderHasError(shader),
                            NotCompatible = isNotCompatible
                    });
                }
            }
        }

        private static bool IsDefaultShader(string shaderPath)
        {
            return shaderPath.Contains("Packages") || shaderPath.Contains("Resources") || shaderPath.Contains("Hidden/");
        }

        private void PingAndLoadShader(string assetPath)
        {
            var shader = AssetDatabase.LoadAssetAtPath<Shader>(assetPath);
            EditorGUIUtility.PingObject(shader);
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Shader>(assetPath);
        }

        private class ShaderInfo
        {
            public string Name          { get; set; }
            public string Path          { get; set; }
            public bool   HasWarning    { get; set; }
            public bool   HasError      { get; set; }
            public bool   NotCompatible { get; set; }
        }
    }
}
#endif