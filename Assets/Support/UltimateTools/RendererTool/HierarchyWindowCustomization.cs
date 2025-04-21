#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UltimateHelper
{
    [InitializeOnLoad]
    public static class HierarchyWindowCustomization
    {
        static HierarchyWindowCustomization()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && DataRendererTool.DrawUndoInHierarchy && DataRendererTool.UndoMaterials.Count > 0)
            {
                if (DataRendererTool.UndoMaterials.TryGetValue(obj, out var undoStack))
                {
                    int    undoCount = undoStack.Count();
                    string text      = "";
                    if (undoCount > 0)
                    {
                        text = undoCount + "/" + DataRendererTool.MaxUndoCount;
                    }
                    Rect rect = new Rect(selectionRect.xMax - 50, selectionRect.y, 50, selectionRect.height);
                    GUI.Label(rect, text, EditorStyles.boldLabel);
                }
            }
        }
    }
}
#endif
