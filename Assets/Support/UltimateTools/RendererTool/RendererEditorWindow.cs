#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace UltimateHelper
{
    public class RendererEditorWindow : EditorWindow
    {
        private int      _selectedTab     = 0;
        private int      _selectedFeature = 0;
        private int      _multiObjectTab  = 0;
        private string   _shaderFrom      = "";
        private string   _shaderTo        = "";
        private Material _materialToSet;
        private Material _materialFrom;
        private Material _materialTo;
        private Mesh     _meshToSet;
        private Mesh     _meshFrom;
        private Mesh     _meshTo;
        private bool     _applyToChildren = false;
        private int      _scopeApply      = 0;

        private Dictionary<GameObject, StackCustom<Material>> _undoMaterials = new();

        private int              _maxUndoCount = 10;
        private int              _passUndoCount;
        private bool             _drawUndoInHierarchy;
        private bool             _repaintHierarchy        = true;
        private Vector2          _scrollPositionSetupTab  = Vector2.zero;
        private Vector2          _scrollPositionSearchTab = Vector2.zero;
        private Material         _materialFromProject;
        private Material         _materialToProject;
        private bool             _updateInScenes                = true;
        private int              _searchScope                   = 0;
        private int              _searchType                    = 0;
        private string           _shaderSearch                  = "";
        private Material         _materialSearch                = null;
        private List<GameObject> _searchResultsGameObjectTarget = new();
        private List<Material>   _searchResultsMaterialTarget   = new();
        private float            _itemWidth                     = 100f;
        private float            _itemWidthMin                  = 170f;
        private string           _currentResultSearchType       = "";
        private string           _currentResultSearchScope      = "";
        private int              _returnType                    = 0;
        private int              _itemCount                     = 0;
        private string[]         _allShaders;
        private string[]         _filteredShadersFrom;
        private string[]         _filteredShadersTo;
        private string[]         _filteredShadersSearch;
        private string[]         _filteredShadersUpdateProperty;
        private int              _selectedSearchShaderIndex     = -1;
        private int              _selectedChangeFromShaderIndex = -1;
        private int              _selectedChangeToShaderIndex   = -1;

        // change property material
        private int    _selectedShaderIndexChangeProperty = -1;
        private string _shaderChangeProperty              = "";
        private string _propertyReference                 = "";

        private float   _floatValue  = 0f;
        private Color   _colorValue  = Color.white;
        private Vector4 _vectorValue = Vector4.zero;
        private Texture _textureValue;
        private int     _intValue = 0;

        private string[]                               _propertyReferences;
        private int                                    _selectedPropertyReferenceIndex = -1;
        private Dictionary<string, ShaderPropertyType> _propertyTypes                  = new();
        //


        [MenuItem("UltimateTools/Renderer Tool",false,2)]
        public static void ShowWindow()
        {
            GetWindow<RendererEditorWindow>("Renderer Tool");
        }

        private void OnEnable()
        {
            _searchScope = 0;
            LoadShaderData();
        }

        private void OnFocus()
        {
            OnChangeState();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _selectedFeature =
                    GUILayout.Toolbar(_selectedFeature, new string[] { "Search", "Setup" }, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.Space(15); // Left padding
            GUILayout.BeginVertical();

            switch (_selectedFeature)
            {
                case 0:
                    DrawSearchTab();

                    break;
                case 1:
                    DrawSetupTab();

                    break;
            }

            GUILayout.EndVertical();
            GUILayout.Space(15); // Right padding
            GUILayout.EndHorizontal();
        }

        private void OnChangeState()
        {
            LoadShaderData();
            _selectedSearchShaderIndex         = -2;
            _selectedChangeFromShaderIndex     = -2;
            _selectedChangeFromShaderIndex     = -2;
            _selectedShaderIndexChangeProperty = -2;
        }

        private void LoadShaderData()
        {
            _allShaders                    = GetAllShaderNames();
            _filteredShadersFrom           = _allShaders;
            _filteredShadersTo             = _allShaders;
            _filteredShadersSearch         = _allShaders;
            _filteredShadersUpdateProperty = _allShaders;
        }

        #region SearchTab

        private GameObject[] GetAllGameObjectsInHierarchy()
        {
            var allObjectsInHierarchy = new List<GameObject>();

            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                allObjectsInHierarchy.AddRange(obj.GetComponentsInChildren<Transform>().Select(x => x.gameObject));
            }

            return allObjectsInHierarchy.ToArray();
        }

        private string[] GetAllShaderNames()
        {
            return ShaderUtil.GetAllShaderInfo()
                             .Select(s => s.name)
                             .OrderBy(s => s)
                             .ToArray();
        }

        private void SearchShader(string shaderName)
        {
            var shader = Shader.Find(shaderName);
            _searchResultsGameObjectTarget.Clear();
            _searchResultsMaterialTarget.Clear();

            if (!shader)
            {
                Debug.LogError("Shader not found.");

                return;
            }

            SearchWithShaderInScope(shader_ => shader_ && shader_.Equals(shader));
        }

        private void SearchMaterial(Material material)
        {
            _searchResultsGameObjectTarget.Clear();

            if (material == null)
            {
                Debug.LogError("Material not found.");

                return;
            }

            SearchWithMaterialInScope(renderer => renderer.sharedMaterial == material);
        }

        private void SearchWithMaterialInScope(Func<Renderer, bool> predicate)
        {
            switch (_searchScope)
            {
                // Hierarchy
                case 1:
                {
                    var allObjects = GetAllGameObjectsInHierarchy();

                    foreach (var obj in allObjects)
                    {
                        var renderers = obj.GetComponentsInChildren<Renderer>(true);

                        foreach (var renderer in renderers)
                        {
                            if (predicate(renderer))
                            {
                                _searchResultsGameObjectTarget.Add(renderer.gameObject);
                                EditorGUIUtility.PingObject(renderer.gameObject);
                            }
                        }
                    }

                    break;
                }
                // Assets
                case 0:
                {
                    var guids = AssetDatabase.FindAssets("t:GameObject");

                    foreach (var guid in guids)
                    {
                        var path      = AssetDatabase.GUIDToAssetPath(guid);
                        var obj       = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                        var renderers = obj.GetComponentsInChildren<Renderer>(true);

                        foreach (var renderer in renderers)
                        {
                            if (predicate(renderer))
                            {
                                _searchResultsGameObjectTarget.Add(renderer.gameObject);
                            }
                        }
                    }

                    break;
                }
            }
        }

        private void SearchWithShaderInScope(Func<Shader, bool> predicate)
        {
            switch (_searchScope)
            {
                // Hierarchy
                case 1:
                {
                    var allObjects = GetAllGameObjectsInHierarchy();

                    foreach (var obj in allObjects)
                    {
                        var renderers = obj.GetComponentsInChildren<Renderer>(true);

                        foreach (var renderer in renderers)
                        {
                            if (renderer && renderer.sharedMaterial && predicate(renderer.sharedMaterial.shader))
                            {
                                _searchResultsGameObjectTarget.Add(renderer.gameObject);
                                EditorGUIUtility.PingObject(renderer.gameObject);
                            }
                        }
                    }

                    break;
                }
                // Assets
                case 0:
                {
                    var guids = AssetDatabase.FindAssets("t:GameObject");

                    foreach (var guid in guids)
                    {
                        var path      = AssetDatabase.GUIDToAssetPath(guid);
                        var obj       = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                        var renderers = obj.GetComponentsInChildren<Renderer>(true);

                        foreach (var renderer in renderers)
                        {
                            if (renderer && renderer.sharedMaterial && predicate(renderer.sharedMaterial.shader))
                            {
                                _searchResultsGameObjectTarget.Add(renderer.gameObject);
                            }
                        }
                    }

                    guids = AssetDatabase.FindAssets("t:Material");

                    foreach (var guid in guids)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guid);
                        var obj  = AssetDatabase.LoadAssetAtPath<Material>(path);

                        if (predicate(obj.shader))
                        {
                            _searchResultsMaterialTarget.Add(obj);
                        }
                    }

                    break;
                }
            }
        }

        private void DrawSearchTab()
        {
            DrawSearchScopeAndType();
            DrawSearchInputFields();
            DrawSettings();
            DrawSearchResults();
        }

        private void DrawSearchScopeAndType()
        {
            _searchScope = GUILayout.Toolbar(_searchScope, new string[] { "Assets", "Hierarchy" });
            EditorGUILayout.Space();
            GUILayout.Label("Search Type", EditorStyles.boldLabel);
            _searchType = GUILayout.Toolbar(_searchType, new string[] { "Shader", "Material" });
            EditorGUILayout.Space();
        }

        private void DrawSearchInputFields()
        {
            if (_searchType == 0)
            {
                DrawShaderSelection(ref _shaderSearch, ref _selectedSearchShaderIndex,
                        "Shader Name", ref _filteredShadersSearch);
                DrawSearchButtons(() => SearchShader(_shaderSearch), "Shader");
            }
            else
            {
                _materialSearch =
                        (Material)EditorGUILayout.ObjectField("Material", _materialSearch, typeof(Material), false);
                _returnType = 0;
                DrawSearchButtons(() => SearchMaterial(_materialSearch), "Material");
            }
        }

        private void DrawShaderSelection(ref string shaderSearch, ref int      selectedShaderIndex,
                                         string     label,        ref string[] filteredShaders)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(label);

            // Tạo một ô tìm kiếm
            var newSearch = EditorGUILayout.TextField(shaderSearch);

            // Nếu văn bản tìm kiếm thay đổi, cập nhật danh sách đã lọc
            if (newSearch != shaderSearch || selectedShaderIndex == -2)
            {
                shaderSearch = newSearch;
                filteredShaders = string.IsNullOrEmpty(shaderSearch)
                        ? _allShaders
                        : _allShaders.Where(s => s.IndexOf(newSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                     .ToArray();
                selectedShaderIndex = -1;

                for (var i = 0; i < filteredShaders.Length; i++)
                {
                    if (!filteredShaders[i].Equals(shaderSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    selectedShaderIndex = i;

                    break;
                }
            }

            // Hiển thị popup với danh sách shader đã lọc
            var newSelectedIndex = EditorGUILayout.Popup(selectedShaderIndex, filteredShaders);

            // Nếu lựa chọn thay đổi, cập nhật shader đã chọn
            if (newSelectedIndex != selectedShaderIndex)
            {
                selectedShaderIndex = newSelectedIndex;

                if (selectedShaderIndex >= 0 && selectedShaderIndex < filteredShaders.Length)
                {
                    shaderSearch = filteredShaders[selectedShaderIndex];
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawSearchButtons(Action searchAction, string searchType)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Search", GUILayout.Height(30), GUILayout.MaxWidth(500)))
            {
                searchAction();
                _currentResultSearchScope = _searchScope == 0 ? "Assets" : "Hierarchy";
                _currentResultSearchType  = searchType;
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Clear Data", GUILayout.Height(30), GUILayout.MaxWidth(500)))
            {
                _currentResultSearchType  = "";
                _currentResultSearchScope = "";
                _searchResultsGameObjectTarget.Clear();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawSettings()
        {
            EditorGUILayout.Space();
            GUILayout.Label("Setting", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Item Width", GUILayout.Width(100));
            _itemWidth = EditorGUILayout.FloatField(_itemWidth);
            _itemWidth = math.max(_itemWidthMin, _itemWidth);
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            var tabs = new List<string> { "GameObject" };

            if (_searchScope == 0 && _searchType == 0)
            {
                tabs.Add("Material");
            }

            GUILayout.Label("Return Type", EditorStyles.boldLabel);
            _returnType = GUILayout.SelectionGrid(_returnType, tabs.ToArray(), 1, EditorStyles.radioButton);
            EditorGUILayout.Space();
        }

        private void DrawSearchResults()
        {
            _scrollPositionSearchTab = EditorGUILayout.BeginScrollView(_scrollPositionSearchTab);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search Results", EditorStyles.boldLabel);

            if (!string.IsNullOrEmpty(_currentResultSearchScope))
            {
                var resultStyle = new GUIStyle(GUI.skin.label);
                var itemCount = _returnType == 0
                        ? _searchResultsGameObjectTarget.Count
                        : _searchResultsMaterialTarget.Count;
                resultStyle.normal.textColor = itemCount == 0 ? Color.red : Color.green;
                var type = _returnType == 0 ? "GameObject" : "Material";
                GUILayout.Label($"{_currentResultSearchScope} | {_currentResultSearchType} : {_itemCount} {type}",
                        resultStyle);
            }

            GUILayout.EndHorizontal();

            var width         = math.min(position.width, _itemWidth);
            var itemsPerRow   = Mathf.FloorToInt(position.width / width);
            var count         = 0;
            var totalRowWidth = itemsPerRow * width;
            var padding       = (position.width - totalRowWidth) / 2;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(padding);
            RemoveDuplicateSearchResults();
            _searchResultsGameObjectTarget = _searchResultsGameObjectTarget.OrderByDescending(IsRoot).ToList();
            _itemCount                     = _searchResultsGameObjectTarget.Count;

            if (_returnType == 0) // GameObject
            {
                DrawGameObjectResults(itemsPerRow, width, padding, ref count);
            }
            else if (_returnType == 1) // Material
            {
                DrawMaterialResults(itemsPerRow, width, padding, ref count);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
        }

        private void RemoveDuplicateSearchResults()
        {
            var uniqueResults = new HashSet<GameObject>(_searchResultsGameObjectTarget);
            _searchResultsGameObjectTarget = uniqueResults.ToList();
        }

        private void DrawGameObjectResults(int itemsPerRow, float width, float padding, ref int count)
        {
            foreach (var obj in _searchResultsGameObjectTarget)
            {
                DrawGameObjectButton(obj, itemsPerRow, width, padding, ref count);
            }
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
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(obj);
            }

            count++;
        }

        private bool IsPartOfOpenedPrefab(GameObject obj)
        {
            var openedPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if (openedPrefabStage != null)
            {
                var prefabRoot = openedPrefabStage.prefabContentsRoot;

                return obj == prefabRoot || obj.transform.IsChildOf(prefabRoot.transform);
            }

            return false;
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

        private void DrawMaterialResults(int itemsPerRow, float width, float padding, ref int count)
        {
            var materials = new List<Material>();

            foreach (var material in _searchResultsMaterialTarget)
            {
                if (material == null || materials.Contains(material))
                {
                    continue;
                }

                materials.Add(material);
            }

            _itemCount = materials.Count;

            foreach (var obj in materials)
            {
                if (count % itemsPerRow == 0 && count != 0)
                {
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(padding);
                }

                if (GUILayout.Button(obj.name, GUILayout.Width(width)))
                {
                    Selection.activeObject = obj;
                    EditorGUIUtility.PingObject(obj);
                }

                count++;
            }
        }

        private bool IsRoot(GameObject obj)
        {
            return obj.transform.parent == null;
        }

        private GameObject GetRoot(GameObject obj)
        {
            return obj.transform.root.gameObject;
        }

        #endregion

        #region SetupTab

        private void DrawSetupTab()
        {
            EditorGUILayout.Space();
            var selectedTab = GUILayout.Toolbar(_selectedTab,
                    new string[]
                    {
                            "Project Scope", "Hierarchy Scope", "Update Property Material",
                            "Setting"
                    },
                    GUILayout.Height(30));

            EditorGUILayout.Space();
            _scrollPositionSetupTab = EditorGUILayout.BeginScrollView(_scrollPositionSetupTab);
            EditorGUILayout.BeginVertical("box");

            if (selectedTab != _selectedTab)
            {
                _scrollPositionSetupTab = Vector2.zero;
                _selectedTab            = selectedTab;
            }

            switch (_selectedTab)
            {
                case 0:
                    DrawProjectScopeTab();

                    break;
                case 1:
                    DrawHierarChyScopeTab();

                    break;
                case 2:
                    DrawUpdatePropertyMaterialTab();

                    break;
                case 3:
                    DrawSettingTab();

                    break;
            }

            EditorGUILayout.EndVertical();

            if (_selectedTab == 1)
            {
                EditorGUILayout.Space();

                if (GUILayout.Button("Clear All Data", GUILayout.Height(30)))
                {
                    _repaintHierarchy = true;
                    ClearData();
                }
            }

            if (_repaintHierarchy)
            {
                ClearDataObjectNull();
                _repaintHierarchy                    = false;
                DataRendererTool.DrawUndoInHierarchy = _drawUndoInHierarchy;
                DataRendererTool.UndoMaterials       = _undoMaterials;
                DataRendererTool.MaxUndoCount        = _maxUndoCount;
                RepaintHierarchy();
            }

            EditorGUILayout.EndScrollView();
        }


        #region Update Property Material Tab

        private enum ShaderPropertyType
        {
            Default,
            Float,
            Color,
            Vector,
            Texture,
            Int
        }

        private void DrawUpdatePropertyMaterialTab()
        {
            GUILayout.Label(
                    "Công cụ này cho phép cập nhật giá trị của các thuộc tính shader cho tất cả các material trong project có sử dụng shader được chỉ định.",
                    EditorStyles.helpBox);
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            DrawShaderSelection(ref _shaderChangeProperty, ref _selectedShaderIndexChangeProperty,
                    "Shader Name", ref _filteredShadersUpdateProperty);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Load Property References", GUILayout.Height(25)))
            {
                LoadPropertyReferences();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Property Reference", GUILayout.Width(150));

            if (_propertyReferences != null && _propertyReferences.Length > 0)
            {
                var newSelectedIndex = EditorGUILayout.Popup(_selectedPropertyReferenceIndex, _propertyReferences);

                if (newSelectedIndex != _selectedPropertyReferenceIndex)
                {
                    _selectedPropertyReferenceIndex = newSelectedIndex;
                    _propertyReference = _propertyReferences[_selectedPropertyReferenceIndex].Split('(')[0].Trim();
                }
            }
            else
            {
                EditorGUILayout.LabelField("No properties found");
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (!string.IsNullOrEmpty(_propertyReference) && _propertyTypes.ContainsKey(_propertyReference))
            {
                GUILayout.Label("Set Value:");
                GUILayout.BeginHorizontal();

                switch (_propertyTypes[_propertyReference])
                {
                    case ShaderPropertyType.Float:
                        _floatValue = EditorGUILayout.FloatField(_floatValue);

                        break;
                    case ShaderPropertyType.Color:
                        _colorValue = EditorGUILayout.ColorField(_colorValue);

                        break;
                    case ShaderPropertyType.Vector:
                        _vectorValue = EditorGUILayout.Vector4Field("", _vectorValue);

                        break;
                    case ShaderPropertyType.Texture:
                        _textureValue = (Texture)EditorGUILayout.ObjectField(_textureValue, typeof(Texture), false);

                        break;
                    case ShaderPropertyType.Int:
                        _intValue = EditorGUILayout.IntField(_intValue);

                        break;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Apply", GUILayout.Height(25)))
            {
                ApplyPropertyToAllMaterials();
            }
        }

        private void LoadPropertyReferences()
        {
            if (string.IsNullOrEmpty(_shaderChangeProperty))
            {
                Debug.LogError("Shader name must not be empty.");

                return;
            }

            var shader = Shader.Find(_shaderChangeProperty);

            if (shader == null)
            {
                Debug.LogError("Shader not found: " + _shaderChangeProperty);

                return;
            }

            var propertyCount = ShaderUtil.GetPropertyCount(shader);
            var propertyList  = new List<string>();
            _propertyTypes.Clear();

            for (var i = 0; i < propertyCount; i++)
            {
                var propertyName        = ShaderUtil.GetPropertyName(shader, i);
                var propertyDescription = ShaderUtil.GetPropertyDescription(shader, i);
                var propertyType        = ShaderUtil.GetPropertyType(shader, i);

                var simplifiedType = SimplifyPropertyType(propertyType);

                if (simplifiedType != ShaderPropertyType.Default) // Exclude default properties
                {
                    var formattedProperty = $"{propertyName} ({propertyDescription}, {simplifiedType})";
                    propertyList.Add(formattedProperty);
                    _propertyTypes[propertyName] = simplifiedType;
                }
            }

            _propertyReferences             = propertyList.ToArray();
            _selectedPropertyReferenceIndex = 0;

            if (_propertyReferences.Length > 0)
            {
                _propertyReference = _propertyReferences[0].Split('(')[0].Trim();
            }

            Debug.Log("Property references loaded for shader: " + _shaderChangeProperty);
        }

        private ShaderPropertyType SimplifyPropertyType(ShaderUtil.ShaderPropertyType propertyType)
        {
            switch (propertyType)
            {
                case ShaderUtil.ShaderPropertyType.Color: return ShaderPropertyType.Color;
                case ShaderUtil.ShaderPropertyType.Float: return ShaderPropertyType.Float;
                case ShaderUtil.ShaderPropertyType.Range: return ShaderPropertyType.Float;
                case ShaderUtil.ShaderPropertyType.Int: return ShaderPropertyType.Int;
                case ShaderUtil.ShaderPropertyType.Vector: return ShaderPropertyType.Vector;
                case ShaderUtil.ShaderPropertyType.TexEnv: return ShaderPropertyType.Texture;
                default: return ShaderPropertyType.Default; // Default to Float for unsupported types
            }
        }

        private void ApplyPropertyToAllMaterials()
        {
            if (string.IsNullOrEmpty(_shaderChangeProperty) || string.IsNullOrEmpty(_propertyReference))
            {
                Debug.LogError("Shader name and property reference must not be empty.");

                return;
            }

            var shader = Shader.Find(_shaderChangeProperty);

            if (shader == null)
            {
                Debug.LogError("Shader not found: " + _shaderChangeProperty);

                return;
            }

            var materials = Resources.FindObjectsOfTypeAll<Material>();

            foreach (var mat in materials)
            {
                if (mat.shader == shader)
                {
                    if (_propertyTypes.TryGetValue(_propertyReference, out var propertyType))
                    {
                        switch (propertyType)
                        {
                            case ShaderPropertyType.Float:
                                mat.SetFloat(_propertyReference, _floatValue);

                                break;
                            case ShaderPropertyType.Color:
                                mat.SetColor(_propertyReference, _colorValue);

                                break;
                            case ShaderPropertyType.Vector:
                                mat.SetVector(_propertyReference, _vectorValue);

                                break;
                            case ShaderPropertyType.Texture:
                                mat.SetTexture(_propertyReference, _textureValue);

                                break;
                            case ShaderPropertyType.Int:
                                mat.SetInt(_propertyReference, _intValue);

                                break;
                        }

                        EditorUtility.SetDirty(mat);
                    }
                }
            }

            Debug.Log("Property updated for all materials using shader: " + _shaderChangeProperty);
        }

        #endregion

        #region Setting Tab

        private void DrawSettingTab()
        {
            GUILayout.Label("Setting", EditorStyles.boldLabel);
            _drawUndoInHierarchy = EditorGUILayout.Toggle("Draw Undo In Hierarchy", _drawUndoInHierarchy);
            _maxUndoCount        = EditorGUILayout.IntField("Max Undo Count", _maxUndoCount);

            if (_passUndoCount != _maxUndoCount)
            {
                if (_maxUndoCount <= 0)
                {
                    _maxUndoCount = 1;
                    EditorGUILayout.IntField("Max Undo Count", _maxUndoCount);
                }

                _repaintHierarchy             = true;
                _passUndoCount                = _maxUndoCount;
                DataRendererTool.MaxUndoCount = _maxUndoCount;

                foreach (var undoMaterial in _undoMaterials)
                {
                    undoMaterial.Value.ChangeSize(_maxUndoCount);
                }
            }

            if (DataRendererTool.DrawUndoInHierarchy != _drawUndoInHierarchy)
            {
                _repaintHierarchy                    = true;
                DataRendererTool.DrawUndoInHierarchy = _drawUndoInHierarchy;
            }
        }

        private void RepaintHierarchy()
        {
            EditorApplication.RepaintHierarchyWindow();
        }

        #endregion

        #region Project Scope Func

        private void DrawProjectScopeTab()
        {
            GUILayout.Label("Change Shader", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            // Shader From
            DrawShaderSelection(ref _shaderFrom, ref _selectedChangeFromShaderIndex,
                    "Shader From", ref _filteredShadersFrom);

            EditorGUILayout.Space();

            // Shader To
            DrawShaderSelection(ref _shaderTo, ref _selectedChangeToShaderIndex, "Shader To",
                    ref _filteredShadersTo);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply Shader", GUILayout.Height(30)))
            {
                ChangeShaderInProject(_shaderFrom, _shaderTo);
            }

            EditorGUILayout.Space();
            GUILayout.Label("Change Material", EditorStyles.boldLabel);
            _materialFromProject =
                    (Material)EditorGUILayout.ObjectField("Material From", _materialFromProject, typeof(Material),
                            false);
            _materialToProject =
                    (Material)EditorGUILayout.ObjectField("Material To", _materialToProject, typeof(Material), false);

            EditorGUILayout.Space();
            _updateInScenes = EditorGUILayout.Toggle("Update in Scenes", _updateInScenes);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply Material", GUILayout.Height(30)))
            {
                ChangeMaterialInProject(_materialFromProject, _materialToProject);
            }
        }

        private void ChangeMaterialInProject(Material fromMaterial, Material toMaterial)
        {
            if (fromMaterial == null || toMaterial == null)
            {
                Debug.LogError("Invalid material provided.");

                return;
            }

            // Change materials in all assets
            var guids = AssetDatabase.FindAssets("t:Material");

            foreach (var guid in guids)
            {
                var path     = AssetDatabase.GUIDToAssetPath(guid);
                var material = AssetDatabase.LoadAssetAtPath<Material>(path);

                if (material == fromMaterial)
                {
                    material = toMaterial;
                    EditorUtility.SetDirty(material);
                }
            }

            if (_updateInScenes)
            {
                // Change materials in all scenes
                foreach (var scene in EditorBuildSettings.scenes)
                {
                    if (scene.enabled)
                    {
                        var currentScene = EditorSceneManager.OpenScene(scene.path);
                        var renderers    = FindObjectsOfType<Renderer>();

                        foreach (var renderer in renderers)
                        {
                            if (renderer.sharedMaterial == fromMaterial)
                            {
                                renderer.sharedMaterial = toMaterial;
                                EditorUtility.SetDirty(renderer);
                            }
                        }

                        EditorSceneManager.SaveScene(currentScene);
                    }
                }
            }

            AssetDatabase.SaveAssets();
        }

        #endregion

        #region Hierarchy Tab

        private void ClearDataObjectNull()
        {
            var keys = _undoMaterials.Keys.ToList();

            foreach (var key in keys)
            {
                if (!key)
                {
                    _undoMaterials.Remove(key);
                }
            }
        }

        private void DrawHierarChyScopeTab()
        {
            _scopeApply = GUILayout.SelectionGrid(_scopeApply,
                    new string[] { "Apply to all object", "Apply to object selection" },
                    1, EditorStyles.radioButton);

            if (_scopeApply != 0)
            {
                GUILayout.BeginHorizontal();
                _applyToChildren = EditorGUILayout.Toggle(_applyToChildren, GUILayout.Width(20));
                GUILayout.Label("Apply to Children", GUILayout.Width(150));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            _multiObjectTab =
                    GUILayout.Toolbar(_multiObjectTab, new string[] { "Material", "Mesh" }, GUILayout.Height(30));

            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("box");

            switch (_multiObjectTab)
            {
                case 0:
                    DrawMaterialTab();

                    break;
                case 1:
                    DrawMeshTab();

                    break;
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawMaterialTab()
        {
            GUILayout.Label("Set Material", EditorStyles.boldLabel);
            _materialToSet = (Material)EditorGUILayout.ObjectField("Material", _materialToSet, typeof(Material), false);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                SetMaterialToSelectedObjects(_materialToSet);
            }

            EditorGUILayout.Space();
            GUILayout.Label("Change Material", EditorStyles.boldLabel);
            _materialFrom =
                    (Material)EditorGUILayout.ObjectField("Material From", _materialFrom, typeof(Material), false);
            _materialTo = (Material)EditorGUILayout.ObjectField("Material To", _materialTo, typeof(Material), false);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                ChangeMaterialInSelectedObjects(_materialFrom, _materialTo);
            }

            DrawUndoButton();
            DrawClearData();
        }

        private void DrawClearData()
        {
            EditorGUILayout.Space();
            var undoAvailable = IsUndoAvailable();
            GUI.enabled         = undoAvailable;
            GUI.backgroundColor = undoAvailable ? Color.white : Color.gray;

            if (GUILayout.Button("Clear Data", GUILayout.Height(30)))
            {
                _repaintHierarchy = true;
                var selectedObjects = GetObjectsSet();

                foreach (var obj in selectedObjects)
                {
                    if (!_undoMaterials.ContainsKey(obj))
                    {
                        continue;
                    }

                    _undoMaterials.Remove(obj);
                }
            }

            GUI.enabled         = true;
            GUI.backgroundColor = Color.white;
        }

        private void DrawUndoButton()
        {
            DrawSeparator();

            var undoAvailable = IsUndoAvailable();
            GUI.enabled         = undoAvailable;
            GUI.backgroundColor = undoAvailable ? Color.white : Color.gray;

            if (GUILayout.Button("Undo", GUILayout.Height(30)))
            {
                _repaintHierarchy = true;
                UndoMaterialChange();
            }

            GUI.enabled         = true;
            GUI.backgroundColor = Color.white;
        }

        private bool IsUndoAvailable()
        {
            var selectedObjects = GetObjectsSet();

            foreach (var obj in selectedObjects)
            {
                if (!_undoMaterials.ContainsKey(obj))
                {
                    continue;
                }

                var undoStack = _undoMaterials[obj];

                if (!undoStack.IsEmpty())
                {
                    return true;
                }
            }

            return false;
        }

        private void UndoMaterialChange()
        {
            var selectedObjects = GetObjectsSet();

            foreach (var obj in selectedObjects)
            {
                if (!_undoMaterials.ContainsKey(obj))
                {
                    continue;
                }

                var undoStack = _undoMaterials[obj];

                if (undoStack.IsEmpty())
                {
                    continue;
                }

                var material = undoStack.Pop();
                ApplyMaterial(obj.GetComponents<Renderer>(), material);
            }
        }

        private GameObject[] GetObjectsSet()
        {
            var objects = new List<GameObject>();

            if (_scopeApply == 0)
            {
                objects.AddRange(GetAllGameObjectsInHierarchy());
            }
            else if (_applyToChildren)
            {
                foreach (var obj in Selection.gameObjects)
                {
                    objects.AddRange(obj.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject));
                }
            }
            else
            {
                objects.AddRange(Selection.gameObjects);
            }

            objects = new HashSet<GameObject>(objects).ToList();

            return objects.ToArray();
        }

        private void DrawSeparator(float thickness = 2, float spaceUp = 10, float spaceDown = 10)
        {
            EditorGUILayout.Space(spaceUp);
            var rect = EditorGUILayout.GetControlRect(false, thickness);
            rect.height = thickness;
            EditorGUI.DrawRect(rect, new(0, 1f, 0f, 1));
            EditorGUILayout.Space(spaceDown);
        }

        private void DrawMeshTab()
        {
            GUILayout.Label("Set Mesh", EditorStyles.boldLabel);
            _meshToSet = (Mesh)EditorGUILayout.ObjectField("Mesh", _meshToSet, typeof(Mesh), false);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                SetMeshToSelectedObjects(_meshToSet);
            }

            EditorGUILayout.Space();
            GUILayout.Label("Change Mesh", EditorStyles.boldLabel);
            _meshFrom = (Mesh)EditorGUILayout.ObjectField("Mesh From", _meshFrom, typeof(Mesh), false);
            _meshTo   = (Mesh)EditorGUILayout.ObjectField("Mesh To", _meshTo, typeof(Mesh), false);

            EditorGUILayout.Space();

            if (GUILayout.Button("Apply", GUILayout.Height(30)))
            {
                ChangeMeshInSelectedObjects(_meshFrom, _meshTo);
            }
        }

        private void ChangeShaderInProject(string shaderFrom, string shaderTo)
        {
            var fromShader = Shader.Find(shaderFrom);
            var toShader   = Shader.Find(shaderTo);

            if (fromShader == null || toShader == null)
            {
                Debug.LogError("Invalid shader names provided.");

                return;
            }

            var guids = AssetDatabase.FindAssets("t:Material");

            foreach (var guid in guids)
            {
                var path     = AssetDatabase.GUIDToAssetPath(guid);
                var material = AssetDatabase.LoadAssetAtPath<Material>(path);

                // Kiểm tra nếu material không phải là một Material Variant
                if (material.shader == fromShader && material.parent == null)
                {
                    material.shader = toShader;
                    EditorUtility.SetDirty(material);
                }
            }

            AssetDatabase.SaveAssets();
        }


        private void SetMaterialToSelectedObjects(Material material)
        {
            var renderers = new List<Renderer>();

            foreach (var obj in GetObjectsSet())
            {
                if (!obj.TryGetComponent(out Renderer renderer))
                {
                    continue;
                }

                renderers.Add(renderer);
            }

            SetMaterial(renderers.ToArray(), material);
        }

        private void SetMaterial(Renderer[] renderers, Material material)
        {
            _repaintHierarchy = true;

            foreach (var renderer in renderers)
            {
                var objSet = renderer.gameObject;
                _undoMaterials.TryAdd(objSet, new(_maxUndoCount));

                var undoStack = _undoMaterials[objSet];
                undoStack.Push(renderer.sharedMaterial);
            }

            ApplyMaterial(renderers, material);
        }

        private void ChangeMaterialInSelectedObjects(Material fromMaterial, Material toMaterial)
        {
            var renderersFilter = new List<Renderer>();

            foreach (var obj in GetObjectsSet())
            {
                if (!obj.TryGetComponent(out Renderer renderer))
                {
                    continue;
                }

                renderersFilter.Add(renderer);
            }

            ChangeMaterial(renderersFilter.ToArray(), fromMaterial, toMaterial);
        }

        private void ChangeMaterial(Renderer[] renderers, Material fromMaterial, Material toMaterial)
        {
            foreach (var renderer in renderers)
            {
                ChangeMaterial(renderer, fromMaterial, toMaterial);
            }
        }

        private void ChangeMaterial(Renderer renderer, Material fromMaterial, Material toMaterial)
        {
            if (renderer.sharedMaterial != fromMaterial)
            {
                return;
            }

            renderer.sharedMaterial = toMaterial;
        }

        private void ApplyMaterial(Renderer[] renderers, Material material)
        {
            foreach (var renderer in renderers)
            {
                renderer.sharedMaterial = material;
            }
        }

        private void SetMeshToSelectedObjects(Mesh mesh)
        {
            var meshFilters = new List<MeshFilter>();

            foreach (var obj in GetObjectsSet())
            {
                if (!obj.TryGetComponent(out MeshFilter meshf))
                {
                    continue;
                }

                meshFilters.Add(meshf);
            }

            ApplyMesh(meshFilters.ToArray(), mesh);
        }

        private void ChangeMeshInSelectedObjects(Mesh fromMesh, Mesh toMesh)
        {
            var meshFilters = new List<MeshFilter>();

            foreach (var obj in GetObjectsSet())
            {
                if (!obj.TryGetComponent(out MeshFilter meshf))
                {
                    continue;
                }

                meshFilters.Add(meshf);
            }

            ChangeMesh(meshFilters.ToArray(), fromMesh, toMesh);
        }

        private void ApplyMesh(MeshFilter[] meshFilters, Mesh mesh)
        {
            foreach (var meshf in meshFilters)
            {
                ApplyMesh(meshf, mesh);
            }
        }

        private void ApplyMesh(MeshFilter meshFilter, Mesh mesh)
        {
            meshFilter.sharedMesh = mesh;
        }

        private void ChangeMesh(MeshFilter[] meshFilters, Mesh fromMesh, Mesh toMesh)
        {
            foreach (var meshF in meshFilters)
            {
                ChangeMesh(meshF, fromMesh, toMesh);
            }
        }

        private void ChangeMesh(MeshFilter meshFilter, Mesh fromMesh, Mesh toMesh)
        {
            if (meshFilter.sharedMesh == fromMesh)
            {
                meshFilter.sharedMesh = toMesh;
            }
        }

        private void ClearData()
        {
            _undoMaterials.Clear();
        }

        #endregion

        #endregion
    }
}
#endif