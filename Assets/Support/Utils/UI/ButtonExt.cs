using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateHelper
{
    // Lớp mở rộng cho Button
    public static partial class UIExtensionMethods
    {
        /// <summary>
        /// Gán một hành động cho sự kiện onClick, thay thế các listener hiện có.
        /// </summary>
        /// <param name="button">Nút cần gán sự kiện.</param>
        /// <param name="action">Hành động sẽ được thực thi khi nhấn nút.</param>
        public static void SetListener(this Button button, UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }

        /// <summary>
        /// Thêm một hành động vào sự kiện onClick.
        /// </summary>
        /// <param name="button">Nút cần thêm sự kiện.</param>
        /// <param name="action">Hành động sẽ được thêm vào sự kiện onClick.</param>
        public static void Add(this Button button, UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        /// <summary>
        /// Xóa một hành động khỏi sự kiện onClick.
        /// </summary>
        /// <param name="button">Nút cần xóa sự kiện.</param>
        /// <param name="action">Hành động sẽ được xóa khỏi sự kiện onClick.</param>
        public static void Remove(this Button button, UnityAction action)
        {
            button.onClick.RemoveListener(action);
        }

        /// <summary>
        /// Gán một hành động cho sự kiện onClick, thay thế các listener hiện có.
        /// </summary>
        /// <param name="button">Nút cần gán sự kiện.</param>
        /// <param name="action">Hành động sẽ được thực thi khi nhấn nút.</param>
        public static void Set(this Button button, UnityAction action)
        {
            button.SetListener(action);
        }
    }
}