#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UltimateHelper
{
    public class ShaderVariantToolWindow : EditorWindow
    {
        private ShaderVariantCollection                     shaderVariantCollection;
        private List<Shader>                                shaders             = new();
        private List<string>                                shaderLocalKeywords = new();
        private bool[]                                      selectedLocalKeywords;
        private Vector2                                     shaderScrollPosition;
        private Vector2                                     keywordScrollPosition;
        private Vector2                                     previewScrollPosition;
        private List<ShaderVariantCollection.ShaderVariant> previewVariants = new();

        [MenuItem("UltimateTools/Shader Variant Tool",false,4)]
        public static void ShowWindow()
        {
            GetWindow<ShaderVariantToolWindow>("Shader Variant Tool");
        }

        private void OnGUI()
        {
            GUILayout.Label("Shader Variant Collection", EditorStyles.boldLabel);
            shaderVariantCollection = (ShaderVariantCollection)EditorGUILayout.ObjectField("Shader Variant Collection",
                    shaderVariantCollection, typeof(ShaderVariantCollection), false);

            GUILayout.Space(5);

            // Add Clear Shaders button
            if (GUILayout.Button("Clear Shaders"))
            {
                shaders.Clear();
                shaderLocalKeywords.Clear();
                previewVariants.Clear();
                selectedLocalKeywords = new bool[0];
            }

            GUILayout.Label($"Shaders ({shaders.Count})", EditorStyles.boldLabel);

            // Add scroll view for shaders if count exceeds 10
            if (shaders.Count > 10)
            {
                shaderScrollPosition = EditorGUILayout.BeginScrollView(shaderScrollPosition, GUILayout.Height(200));
            }

            for (var i = 0; i < shaders.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    shaders.RemoveAt(i);
                    i--; // Adjust index after removal
                }

                GUILayout.Space(5); // Add space between button and shader field
                shaders[i] = (Shader)EditorGUILayout.ObjectField($"Shader {i + 1}", shaders[i], typeof(Shader), false);

                EditorGUILayout.EndHorizontal();
            }

            if (shaders.Count > 10)
            {
                EditorGUILayout.EndScrollView();
            }

            if (GUILayout.Button("Add Shader"))
            {
                shaders.Add(null);
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Load Shader Keywords"))
            {
                LoadShaderKeywords();
            }

            if (shaderLocalKeywords != null && shaderLocalKeywords.Count > 0)
            {
                GUILayout.Label($"Select Keywords ({shaderLocalKeywords.Count})", EditorStyles.boldLabel);

                // Add scroll view for keywords if count exceeds 10
                if (shaderLocalKeywords.Count > 10)
                {
                    keywordScrollPosition =
                            EditorGUILayout.BeginScrollView(keywordScrollPosition, GUILayout.Height(200));
                }

                for (var i = 0; i < shaderLocalKeywords.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    selectedLocalKeywords[i] = EditorGUILayout.Toggle(selectedLocalKeywords[i], GUILayout.Width(20));
                    GUILayout.Space(5); // Add space between checkbox and text

                    if (GUILayout.Button(shaderLocalKeywords[i], EditorStyles.label, GUILayout.ExpandWidth(true)))
                    {
                        selectedLocalKeywords[i] = !selectedLocalKeywords[i];
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (shaderLocalKeywords.Count > 10)
                {
                    EditorGUILayout.EndScrollView();
                }
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Apply Shader Variants"))
            {
                ApplyShaderVariants();
            }

            if (GUILayout.Button("Load Preview Variants"))
            {
                LoadPreviewVariants();
            }

            if (previewVariants != null && previewVariants.Count > 0)
            {
                GUILayout.Label($"Preview Variants : {previewVariants.Count} Variant", EditorStyles.boldLabel);

                // Add scroll view for preview variants if they don't fit in the view
                previewScrollPosition = EditorGUILayout.BeginScrollView(previewScrollPosition, GUILayout.Height(200));

                Shader lastShader = null;

                foreach (var variant in previewVariants)
                {
                    if (lastShader != variant.shader)
                    {
                        if (lastShader != null)
                        {
                            GUILayout.Space(10); // Add space between different shaders
                        }

                        lastShader = variant.shader;
                    }

                    GUILayout.Label($"Shader: {variant.shader.name}, Keywords: {string.Join(", ", variant.keywords)}");
                }

                EditorGUILayout.EndScrollView();
            }
        }

        private void LoadShaderKeywords()
        {
            shaderLocalKeywords.Clear();

            foreach (var shader in shaders)
            {
                if (shader != null)
                {
                    shaderLocalKeywords.AddRange(shader.keywordSpace.keywordNames);
                }
            }

            shaderLocalKeywords   = shaderLocalKeywords.Distinct().ToList();
            selectedLocalKeywords = new bool[shaderLocalKeywords.Count];
        }

        private void ApplyShaderVariants()
        {
            if (shaderVariantCollection != null && shaders.Count > 0)
            {
                var selectedKeywordsList = shaderLocalKeywords
                                          .Where((keyword, index) => selectedLocalKeywords[index])
                                          .ToList();

                var keywordCombinations = GetKeywordCombinations(selectedKeywordsList);

                // Add the default case with no keywords
                keywordCombinations.Add(new());

                foreach (var shader in shaders)
                {
                    if (shader == null)
                    {
                        continue;
                    }

                    foreach (var combination in keywordCombinations)
                    {
                        if (combination.All(keyword => shader.keywordSpace.keywordNames.Contains(keyword)))
                        {
                            var variant = new ShaderVariantCollection.ShaderVariant
                            {
                                    shader   = shader,
                                    keywords = combination.ToArray()
                            };

                            shaderVariantCollection.Add(variant);
                        }
                    }
                }

                EditorUtility.SetDirty(shaderVariantCollection);
                AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.LogWarning("Please assign both shaders and a shader variant collection.");
            }
        }

        private void LoadPreviewVariants()
        {
            if (shaders.Count > 0)
            {
                var selectedKeywordsList = shaderLocalKeywords
                                          .Where((keyword, index) => selectedLocalKeywords[index])
                                          .ToList();

                var keywordCombinations = GetKeywordCombinations(selectedKeywordsList);

                // Add the default case with no keywords
                keywordCombinations.Add(new());

                previewVariants.Clear();

                foreach (var shader in shaders)
                {
                    if (shader == null)
                    {
                        continue;
                    }

                    foreach (var combination in keywordCombinations)
                    {
                        if (combination.All(keyword => shader.keywordSpace.keywordNames.Contains(keyword)))
                        {
                            var variant = new ShaderVariantCollection.ShaderVariant
                            {
                                    shader   = shader,
                                    keywords = combination.ToArray()
                            };

                            previewVariants.Add(variant);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("Please assign shaders.");
            }
        }

        private List<List<string>> GetKeywordCombinations(List<string> keywords)
        {
            var result = new List<List<string>>();
            GenerateCombinations(keywords, 0, new(), result);

            return result;
        }

        private void GenerateCombinations(List<string>       keywords, int index, List<string> current,
                                          List<List<string>> result)
        {
            if (index == keywords.Count)
            {
                if (current.Count > 0)
                {
                    result.Add(new(current));
                }

                return;
            }

            // Include the current keyword
            current.Add(keywords[index]);
            GenerateCombinations(keywords, index + 1, current, result);

            // Exclude the current keyword
            current.RemoveAt(current.Count - 1);
            GenerateCombinations(keywords, index + 1, current, result);
        }
    }
}
#endif