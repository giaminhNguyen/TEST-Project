#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace UltimateHelper
{
    public static class DataRendererTool
    {
        public static bool                                          DrawUndoInHierarchy = false;
        public static int                                           MaxUndoCount        = 10;
        public static Dictionary<GameObject, StackCustom<Material>> UndoMaterials       = new();
    }
}
#endif