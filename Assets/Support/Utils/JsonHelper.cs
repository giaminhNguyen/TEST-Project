using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateHelper
{
    public static class JsonHelper
    {
        public static string ToJson<T>(T obj)
        {
            return obj == null ? "" : JsonUtility.ToJson(obj);
        }

        public static string ToJson<T>(List<T> array)
        {
            if (array.Count == 0)
            {
                return null;
            }

            WrapperList<T> wrapperList = new()
            {
                    items = array
            };

            return JsonUtility.ToJson(wrapperList);
        }

        public static string ToJson<T>(T[] array)
        {
            if (array == null)
            {
                return null;
            }

            WrapperArray<T> wrapperList = new()
            {
                    items = array
            };

            return JsonUtility.ToJson(wrapperList);
        }

        public static T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }


        public static List<T> FromJsonList<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new List<T>();
            }

            WrapperList<T> wrapperList = JsonUtility.FromJson<WrapperList<T>>(json);
            return wrapperList.items;
        }

        public static T[] FromJsonArray<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return Array.Empty<T>();
            }

            WrapperArray<T> wrapperList = JsonUtility.FromJson<WrapperArray<T>>(json);
            return wrapperList.items;
        }

        private class WrapperList<T>
        {
            public List<T> items;
        
        }
    
        private class WrapperArray<T>
        {
            public T[] items;
        }
    
    }
}