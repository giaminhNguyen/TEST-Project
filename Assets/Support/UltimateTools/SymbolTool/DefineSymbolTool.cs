#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace UltimateHelper
{
    public class DefineSymbolTool : EditorWindow
    {
        private const string       FilePath = "Assets/UltimateHelper/DefineSymbols.txt";
        private       List<bool>   _symbolStates;
        private       List<bool>   _initialSymbolStates;
        private       List<string> _symbols;
        private       bool         _hasChanges;
        private       Vector2      _manageSymbolsScrollPos;


        [MenuItem("UltimateTools/Define Symbol Tool",false,1)]
        public static void ShowWindow()
        {
            GetWindow<DefineSymbolTool>("Define Symbol Tool");
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnGUI()
        {
            DrawManageSymbolsTab();
        }

        private void DrawManageSymbolsTab()
        {
            var foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                    fontStyle = FontStyle.Bold,
                    normal    = { textColor = Color.white }
            };

            var innerGroupStyle = new GUIStyle(GUI.skin.box)
            {
                    normal  = { background = MakeTex(2, 2, new Color(0.04f, 0.04f, 0.04f)) }, // Nền đen hơn
                    padding = new RectOffset(15, 15, 10, 10),
                    margin  = new RectOffset(5, 5, 5, 5)
            };

            var labelStyle = new GUIStyle(EditorStyles.label)
            {
                    fontStyle = FontStyle.Normal, // Không in đậm
                    normal    = { textColor = Color.white }
            };

            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                    fontStyle = FontStyle.Bold,
                    normal    = { background = MakeTex(2, 2, Color.gray) },
                    padding   = new RectOffset(0, 0, 2, 2),
            };
            var checkChange = false;
            GUILayout.Space(10);
            _manageSymbolsScrollPos =
                    EditorGUILayout.BeginScrollView(_manageSymbolsScrollPos, GUILayout.ExpandHeight(true));

            bool                     insideFoldout      = false;
            Dictionary<string, bool> foldoutStates      = new Dictionary<string, bool>();
            string                   currentFoldoutName = "";

            for (var i = 0; i < _symbols.Count; i++)
            {
                if (_symbols[i].StartsWith("#Foldout"))
                {
                    if (insideFoldout)
                    {
                        EditorGUILayout.EndVertical(); // Đóng nhóm foldout trước đó
                    }

                    currentFoldoutName = _symbols[i].Replace("#Foldout", "").Trim();

                    // Duy trì trạng thái mở/đóng của foldout
                    if (!foldoutStates.ContainsKey(currentFoldoutName))
                    {
                        foldoutStates[currentFoldoutName] = true; // Mặc định mở
                    }

                    foldoutStates[currentFoldoutName] = EditorGUILayout.Foldout(foldoutStates[currentFoldoutName],
                            currentFoldoutName, foldoutStyle);

                    if (foldoutStates[currentFoldoutName])
                    {
                        EditorGUILayout.BeginVertical(innerGroupStyle);
                        insideFoldout = true;
                    }
                    else
                    {
                        insideFoldout = false;
                    }

                    continue;
                }

                if (_symbols[i].StartsWith("#EndFoldout"))
                {
                    if (insideFoldout)
                    {
                        EditorGUILayout.EndVertical();
                    }

                    insideFoldout = false;

                    continue;
                }

                if (insideFoldout && !foldoutStates[currentFoldoutName])
                {
                    continue; // Bỏ qua nếu foldout bị đóng
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField(_symbols[i], labelStyle, GUILayout.MaxWidth(500),
                        GUILayout.ExpandWidth(true));

                // Thay đổi màu nút theo trạng thái
                if (_symbolStates[i] != _initialSymbolStates[i])
                {
                    buttonStyle.normal.textColor = Color.yellow; // Vàng khi thay đổi
                }
                else if (_symbolStates[i])
                {
                    buttonStyle.normal.textColor = Color.green; // Xanh khi bật
                }
                else
                {
                    buttonStyle.normal.textColor = Color.red; // Đỏ khi tắt
                }

                if (GUILayout.Button(_symbolStates[i] ? "Enable" : "Disable", buttonStyle, GUILayout.Width(100)))
                {
                    checkChange     = true;
                    _symbolStates[i] = !_symbolStates[i];
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            if (insideFoldout)
            {
                EditorGUILayout.EndVertical(); // Đảm bảo foldout cuối cùng được đóng
            }

            EditorGUILayout.EndScrollView();
            
            if (checkChange)
            {
                _hasChanges = CheckForChanges();
            }
            CheckAndDrawButtonChange();
        }

        private void CheckAndDrawButtonChange()
        {
            if (!_hasChanges)
            {
                return;
            }
            
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                    fontStyle = FontStyle.Bold,
                    normal    = { background = MakeTex(2, 2, Color.white) },
                    padding   = new RectOffset(0, 0, 2, 2)
            };

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Revert Changes", buttonStyle, GUILayout.MaxWidth(500),
                        GUILayout.ExpandWidth(true)))
            {
                RevertChanges();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Apply Changes", buttonStyle, GUILayout.MaxWidth(500),
                        GUILayout.ExpandWidth(true)))
            {
                ApplyChanges();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void Init()
        {
            if (!File.Exists(FilePath))
            {
                File.WriteAllLines(FilePath,InitValueDefineSymbol());
            }

            _symbols             = new(File.ReadAllLines(FilePath));
            _symbolStates        = new(_symbols.Count);
            _initialSymbolStates = new(_symbols.Count);

            var currentSymbols =
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            var symbolList = new List<string>(currentSymbols.Split(';'));

            var updateFile    = _symbols.RemoveDuplicates();
            var updatedSystem = symbolList.RemoveDuplicates();

            for (var i = 0; i < _symbols.Count; i++)
            {
                if (!_symbols[i].Contains("="))
                {
                    continue;
                }

                var parts          = _symbols[i].Split('=');
                var originalSymbol = parts[0].Trim();
                var newSymbol      = parts[1].Trim();
                _symbols[i] = newSymbol;
                updateFile  = true;

                if (!symbolList.Contains(originalSymbol))
                {
                    continue;
                }

                updatedSystem = true;
                var indexOf = symbolList.IndexOf(originalSymbol);
                symbolList.Remove(originalSymbol);
                symbolList.Insert(indexOf, newSymbol);
            }

            if (updateFile || _symbols.RemoveDuplicates())
            {
                File.WriteAllLines(FilePath, _symbols);
            }

            if (updatedSystem || symbolList.RemoveDuplicates())
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                        string.Join(";", symbolList));
            }
            foreach (var t in _symbols)
            {
                _initialSymbolStates.Add(symbolList.Contains(t));
                _symbolStates.Add(symbolList.Contains(t));
            }

            RevertChanges();
        }

        private string[] InitValueDefineSymbol()
        {
            var defineSymbols = new List<string>();
            defineSymbols.Add("#Foldout System");
            defineSymbols.Add("USE_IRON_SOURCE");
            defineSymbols.Add("USE_ADS_ADMOB");
            defineSymbols.Add("#Foldout Firebase");
            defineSymbols.Add("USE_FIREBASE");
            defineSymbols.Add("USE_FIREBASE_REMOTE");
            defineSymbols.Add("USE_FIREBASE_ANALYTICS");
            defineSymbols.Add("USE_FIREBASE_CRASHLYTICS");
            defineSymbols.Add("USE_FIREBASE_MESSAGING");
            defineSymbols.Add("#Foldout Other");
            defineSymbols.Add("USE_DOTWEEN_CUSTOM");
            defineSymbols.Add("USE_MESSAGE");
            defineSymbols.Add("USE_DEBUG_LOG");
            defineSymbols.Add("#EndFoldout");
            return defineSymbols.ToArray();
        }

        private bool CheckForChanges()
        {
            for (var i = 0; i < _symbols.Count; i++)
            {
                if (_symbolStates[i] != _initialSymbolStates[i])
                {
                    return true;
                }
            }

            return false;
        }

        private void ApplyChanges()
        {
            var currentSymbols =
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            var symbolList = new List<string>(currentSymbols.Split(';'));

            for (var i = 0; i < _symbols.Count; i++)
            {
                if (_symbolStates[i])
                {
                    if (!symbolList.Contains(_symbols[i]))
                    {
                        symbolList.Add(_symbols[i]);
                    }
                }
                else
                {
                    if (symbolList.Contains(_symbols[i]))
                    {
                        symbolList.Remove(_symbols[i]);
                    }
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                    string.Join(";", symbolList));

            LoadChanges();
        }

        private void LoadChanges()
        {
            _initialSymbolStates = new(_symbolStates);
            _hasChanges          = false;
        }

        private void RevertChanges()
        {
            _symbolStates = new(_initialSymbolStates);
            _hasChanges   = false;
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            var pix = new Color[width * height];

            for (var i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}
#endif

