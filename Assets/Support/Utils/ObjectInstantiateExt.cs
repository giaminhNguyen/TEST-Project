using System.Collections.Generic;
using UnityEngine;

namespace UltimateHelper
{
    public static class ObjectInstantiateExt
    {
        /// <summary>
        /// Tạo một bản sao của đối tượng.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self) where T : Object
        {
            return Object.Instantiate(self);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng tại vị trí xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self, Vector3 position) where T : Object
        {
            return Object.Instantiate(self, position, Quaternion.identity);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng tại vị trí và góc quay xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <param name="rotation">Góc quay để tạo bản sao.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self, Vector3 position, Quaternion rotation) where T : Object
        {
            return Object.Instantiate(self, position, rotation);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng tại vị trí, góc quay, và cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <param name="rotation">Góc quay để tạo bản sao.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T, TParent>(this T self, Vector3 position, Quaternion rotation, TParent parent) where T : Object where TParent : Component
        {
            return Object.Instantiate(self, position, rotation, parent.transform);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng tại vị trí, góc quay, và cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <param name="rotation">Góc quay để tạo bản sao.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self, Vector3 position, Quaternion rotation, GameObject parent) where T : Object
        {
            return Object.Instantiate(self, position, rotation, parent.transform);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T, TParent>(this T self, TParent parent) where T : Object where TParent : Component
        {
            return Object.Instantiate(self, parent.transform);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self, GameObject parent) where T : Object
        {
            return Object.Instantiate(self, parent.transform);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng với cha xác định và tùy chọn giữ nguyên vị trí thế giới.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <param name="worldPositionStays">Giữ nguyên vị trí thế giới hay không.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T, TParent>(this T self, TParent parent, bool worldPositionStays) where T : Object where TParent : Component
        {
            return Object.Instantiate(self, parent.transform, worldPositionStays);
        }

        /// <summary>
        /// Tạo một bản sao của đối tượng với cha xác định và tùy chọn giữ nguyên vị trí thế giới.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <param name="worldPositionStays">Giữ nguyên vị trí thế giới hay không.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T Instantiate<T>(this T self, GameObject parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(self, parent.transform, worldPositionStays);
        }

        /// <summary>
        /// Tạo nhiều bản sao của đối tượng.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] Instantiates<T>(this T self, int count) where T : Object
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = Object.Instantiate(self);
            }

            return array;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene.
        /// </summary>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static GameObject[] InstantiatesFromSceneObject(this GameObject self, int count)
        {
            self.SetActive(true);
            var result = self.Instantiates(count, self.transform.parent);
            self.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo một bản sao từ đối tượng trong scene.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T InstantiateFromSceneObject<T>(this T self) where T : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiate(self.transform.parent);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo một bản sao từ đối tượng trong scene tại vị trí xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T InstantiateFromSceneObject<T>(this T self, Vector3 position) where T : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiate(position, Quaternion.identity, self.transform.parent);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo một bản sao từ đối tượng trong scene tại vị trí và góc quay xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="position">Vị trí để tạo bản sao.</param>
        /// <param name="rotation">Góc quay để tạo bản sao.</param>
        /// <returns>Bản sao của đối tượng.</returns>
        public static T InstantiateFromSceneObject<T>(this T self, Vector3 position, Quaternion rotation) where T : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiate(position, rotation, self.transform.parent);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] InstantiatesFromSceneObject<T>(this T self, int count) where T : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiates(count, self.transform.parent);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao của đối tượng với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] Instantiates<T, TParent>(this T self, int count, TParent parent) where T : Object where TParent : Component
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = Object.Instantiate(self, parent.transform);
            }

            return array;
        }

        /// <summary>
        /// Tạo nhiều bản sao của đối tượng với các cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parentArray">Danh sách các đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] Instantiates<T, TParent>(this T self, int count, IReadOnlyList<TParent> parentArray) where T : Object where TParent : Component
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = Object.Instantiate(self, parentArray[i].transform);
            }

            return array;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene với cha xác định.
        /// </summary>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static GameObject[] InstantiatesFromSceneObject<TParent>(this GameObject self, int count, TParent parent) where TParent : Component
        {
            self.SetActive(true);
            var result = self.Instantiates(count, parent);
            self.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] InstantiatesFromSceneObject<T, TParent>(this T self, int count, TParent parent) where T : Component where TParent : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiates(count, parent);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene với các cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <typeparam name="TParent">Loại của đối tượng cha.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parentArray">Danh sách các đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] InstantiatesFromSceneObject<T, TParent>(this T self, int count, IReadOnlyList<TParent> parentArray) where T : Component where TParent : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiates(count, parentArray);
            gameObject.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao của đối tượng với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] Instantiates<T>(this T self, int count, GameObject parent) where T : Object
        {
            var array = new T[count];

            for (var i = 0; i < count; i++)
            {
                array[i] = Object.Instantiate(self, parent.transform);
            }

            return array;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene với cha xác định.
        /// </summary>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static GameObject[] InstantiatesFromSceneObject(this GameObject self, int count, GameObject parent)
        {
            self.SetActive(true);
            var result = self.Instantiates(count, parent);
            self.SetActive(false);
            return result;
        }

        /// <summary>
        /// Tạo nhiều bản sao từ đối tượng trong scene với cha xác định.
        /// </summary>
        /// <typeparam name="T">Loại đối tượng.</typeparam>
        /// <param name="self">Đối tượng cần sao chép.</param>
        /// <param name="count">Số lượng bản sao cần tạo.</param>
        /// <param name="parent">Đối tượng cha.</param>
        /// <returns>Mảng các bản sao của đối tượng.</returns>
        public static T[] InstantiatesFromSceneObject<T>(this T self, int count, GameObject parent) where T : Component
        {
            var gameObject = self.gameObject;
            gameObject.SetActive(true);
            var result = self.Instantiates(count, parent);
            gameObject.SetActive(false);
            return result;
        }
    }
}