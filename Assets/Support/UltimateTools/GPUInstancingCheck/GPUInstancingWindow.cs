#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UltimateHelper
{
    public class GPUInstancingWindow : EditorWindow
    {
        private string _shaderName          = "";
        private bool   _enableGPUInstancing = false;
    
        [MenuItem("UltimateTools/GPU Instancing Window")]
        public static void ShowWindow()
        {
            GetWindow<GPUInstancingWindow>("GPU Instancing");
        }
    
        private void OnGUI()
        {
            GUILayout.Label("GPU Instancing Settings", EditorStyles.boldLabel);
    
            _shaderName          = EditorGUILayout.TextField("Shader Name", _shaderName);
            _enableGPUInstancing = EditorGUILayout.Toggle("Enable GPU Instancing", _enableGPUInstancing);
    
            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                ApplyGPUInstancing();
            }
        }
    
        private void ApplyGPUInstancing()
        {
            if (string.IsNullOrEmpty(_shaderName))
            {
                Debug.LogError("Shader name must not be empty.");
    
                return;
            }
    
            var shader = Shader.Find(_shaderName);
    
            if (shader == null)
            {
                Debug.LogError("Shader not found: " + _shaderName);
    
                return;
            }
    
            var materialGuids = AssetDatabase.FindAssets("t:Material");
    
            foreach (var guid in materialGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var mat  = AssetDatabase.LoadAssetAtPath<Material>(path);
    
                if (mat.shader != shader)
                {
                    continue;
                }
    
                mat.enableInstancing = _enableGPUInstancing;
                EditorUtility.SetDirty(mat);
            }
    
            AssetDatabase.SaveAssets();
            Debug.Log("GPU Instancing updated for all materials using shader: " + _shaderName);
        }
    }
}

#endif