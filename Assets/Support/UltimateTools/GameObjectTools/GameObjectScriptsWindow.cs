#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltimateHelper
{
    public class GameObjectScriptsWindow :
            EditorWindow
    {
        private bool             _includeInactiveObjects;
        private int              _selectedScope;
        private bool             _applyToPrefabs;
        private int              _selectedFeature;
        private string           _searchScriptName;
        private int              _searchScope;
        private List<GameObject> _searchResults = new();
        private MonoScript       _searchScript;
        private float            _itemWidth               = 100;
        private Vector2          _scrollPositionSearchTab = Vector2.zero;
        private bool             _isSearchClick;


        [MenuItem("UltimateTools/Game Object Tools")]
        public static void ShowWindow()
        {
            GetWindow<GameObjectScriptsWindow>("Game Object Tools");
        }

        private void OnGUI()
        {
            GUILayout.Label("Game Object Tools", EditorStyles.boldLabel);

            _selectedFeature =
                    EditorGUILayout.Popup("Feature", _selectedFeature, new string[] { "Search", "Remove Null Script" });

            if (_selectedFeature == 0)
            {
                DrawSearchFeature();
            }
            else if (_selectedFeature == 1)
            {
                DrawRemoveFeature();
            }
        }

        private void DrawSearchFeature()
        {
            EditorGUILayout.Space();
            _searchScope = EditorGUILayout.Popup("Search Scope", _searchScope, new string[] { "Hierarchy", "Assets" });
            EditorGUILayout.Space();
            // Add drag-and-drop functionality for script
            _searchScript =
                    EditorGUILayout.ObjectField("Script", _searchScript, typeof(MonoScript), false) as MonoScript;
            EditorGUILayout.Space();

            if (GUILayout.Button("Search", GUILayout.Height(30)))
            {
                _isSearchClick = true;
                SearchForScript();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Clear Data", GUILayout.Height(30)))
            {
                _isSearchClick = false;
                ClearDataSearch();
            }

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Item Width", GUILayout.Width(100));
            _itemWidth = EditorGUILayout.FloatField(_itemWidth);
            _itemWidth = math.max(100, _itemWidth);
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Search Results :", EditorStyles.boldLabel);

            if (_isSearchClick)
            {
                var resultStyle = new GUIStyle(GUI.skin.label);
                var itemCount   = _searchResults.Count;
                resultStyle.normal.textColor = itemCount == 0 ? Color.red : Color.green;
                var scope = _searchScope == 0 ? "Hierarchy" : "Assets";
                GUILayout.Label($"{scope} : {itemCount} GameObject",
                        resultStyle);
            }

            GUILayout.EndHorizontal();

            _scrollPositionSearchTab = EditorGUILayout.BeginScrollView(_scrollPositionSearchTab);
            EditorGUILayout.BeginHorizontal();
            var width         = math.min(position.width, _itemWidth);
            var itemsPerRow   = Mathf.FloorToInt(position.width / width);
            var count         = 0;
            var totalRowWidth = itemsPerRow * width;
            var padding       = (position.width - totalRowWidth) / 2;
            RemoveDuplicateSearchResults();
            _searchResults = _searchResults.OrderByDescending(IsRoot).ToList();
            GUILayout.Space(padding);

            foreach (var obj in _searchResults)
            {
                DrawGameObjectButton(obj, itemsPerRow, width, padding, ref count);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
        }

        private void ClearDataSearch()
        {
            _searchResults.Clear();
        }

        private void RemoveDuplicateSearchResults()
        {
            var uniqueResults = new HashSet<GameObject>(_searchResults);
            _searchResults = uniqueResults.ToList();
        }

        private void DrawGameObjectButton(GameObject obj, int itemsPerRow, float width, float padding, ref int count)
        {
            if (count % itemsPerRow == 0 && count != 0)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(padding);
            }

            var buttonStyle = new GUIStyle(GUI.skin.button);

            if (IsRoot(obj))
            {
                buttonStyle.normal.textColor = Color.green;
            }

            if (GUILayout.Button(obj.name, buttonStyle, GUILayout.Width(width)))
            {
                EditorGUIUtility.PingObject(obj);
                Selection.activeGameObject = obj;
            }

            count++;
        }

        private bool IsRoot(GameObject obj)
        {
            return obj.transform.parent == null;
        }


        private void DrawRemoveFeature()
        {
            _includeInactiveObjects = EditorGUILayout.Toggle("Include Inactive Objects", _includeInactiveObjects);

            if (_selectedScope != 0)
            {
                _applyToPrefabs = EditorGUILayout.Toggle("Apply to Prefabs", _applyToPrefabs);
            }

            GUILayout.Label("Scope", EditorStyles.boldLabel);
            _selectedScope = GUILayout.SelectionGrid(_selectedScope,
                    new string[] { "Project", "Hierarchy", "Selected Objects" }, 1, EditorStyles.radioButton);

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                Apply();
            }
        }

        private void SearchForScript()
        {
            _searchResults.Clear();

            if (_searchScope == 0)
            {
                foreach (var obj in GetAllGameObjectsInHierarchy())
                {
                    if (obj.GetComponent(_searchScript.GetClass()))
                    {
                        _searchResults.Add(obj);
                    }
                    foreach(var child in obj.GetComponentsInChildren(_searchScript.GetClass()).Select(x => x))
                    {
                        _searchResults.Add(child.gameObject);
                    }
                }
            }
            else if (_searchScope == 1)
            {
                var allGameObjectGuids = AssetDatabase.FindAssets("t:GameObject");

                foreach (var guid in allGameObjectGuids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var obj  = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                    if (obj.GetComponent(_searchScript.GetClass()))
                    {
                        _searchResults.Add(obj);
                    }
                    foreach(var child in obj.GetComponentsInChildren(_searchScript.GetClass()).Select(x => x))
                    {
                        _searchResults.Add(child.gameObject);
                    }
                }
            }
            _searchResults.RemoveDuplicates();
        }

        private GameObject[] GetAllGameObjectsInHierarchy()
        {
            var allObjectsInHierarchy = new List<GameObject>();

            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                allObjectsInHierarchy.AddRange(obj.GetComponentsInChildren<Transform>().Select(x => x.gameObject));
            }

            return allObjectsInHierarchy.ToArray();
        }
        
        private void Apply()
        {
            switch (_selectedScope)
            {
                case 0:
                    RemoveNullScriptsInProject();

                    break;
                case 1:
                    RemoveNullScriptsInHierarchy();

                    break;
                case 2:
                    RemoveNullScriptsInSelectedObjects();

                    break;
            }
        }

        private void RemoveNullScriptsInProject()
        {
            RemoveNullScriptsInHierarchy();

            var allGameObjectGuids = AssetDatabase.FindAssets("t:GameObject");
            var allGameObjects     = new List<GameObject>();

            foreach (var guid in allGameObjectGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var obj  = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allGameObjects.Add(obj);
            }

            var allPrefabGuids = AssetDatabase.FindAssets("t:Prefab");

            foreach (var guid in allPrefabGuids)
            {
                var path   = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                allGameObjects.Add(prefab);
            }

            FindAndRemoveMissing(allGameObjects.ToArray());
        }

        private void RemoveNullScriptsInHierarchy()
        {
            var allObjects         = Resources.FindObjectsOfTypeAll<GameObject>();
            var allRootGameObjects = new List<GameObject>();

            foreach (var go in allObjects)
            {
                if (go.scene.isLoaded || _applyToPrefabs)
                {
                    allRootGameObjects.Add(go);
                }
            }

            FindAndRemoveMissing(allRootGameObjects.ToArray());
        }

        private void RemoveNullScriptsInSelectedObjects()
        {
            FindAndRemoveMissing(Selection.gameObjects);
        }

        private void FindAndRemoveMissing(GameObject[] objs)
        {
            var deeperSelection = objs.SelectMany(go => go.GetComponentsInChildren<Transform>(_includeInactiveObjects))
                                      .Select(t => t.gameObject);
            var prefabs        = new HashSet<Object>();
            var compCount      = 0;
            var goCount        = 0;
            var applyToPrefabs = _applyToPrefabs && _selectedScope != 0;

            foreach (var go in deeperSelection)
            {
                var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                if (count > 0)
                {
                    if (applyToPrefabs && PrefabUtility.IsPartOfAnyPrefab(go))
                    {
                        RecursivePrefabSource(go, prefabs, ref compCount, ref goCount);
                        count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

                        if (count == 0)
                        {
                            continue;
                        }
                    }

                    Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    compCount += count;
                    goCount++;
                }
            }

            Debug.Log($"Found and removed {compCount} missing scripts from {goCount} GameObjects");
        }

        private static void RecursivePrefabSource(GameObject instance, HashSet<Object> prefabs, ref int compCount,
                                                  ref int    goCount)
        {
            var source = PrefabUtility.GetCorrespondingObjectFromSource(instance);

            if (source == null || !prefabs.Add(source))
            {
                return;
            }

            RecursivePrefabSource(source, prefabs, ref compCount, ref goCount);

            var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(source);

            if (count > 0)
            {
                Undo.RegisterCompleteObjectUndo(source, "Remove missing scripts");
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(source);
                compCount += count;
                goCount++;
            }
        }
    }
}
#endif