using TMPro;
using UnityEngine.Events;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu TMP_Dropdown
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Các phương thức tĩnh
        //================================================================================
        
        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện Dropdown.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_Dropdown.DropdownEvent self, UnityAction<int> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// Phương thức này là cách viết rút gọn của AddListener.
        /// </summary>
        /// <param name="self">Sự kiện Dropdown.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_Dropdown.DropdownEvent self, UnityAction<int> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// Phương thức này là cách viết rút gọn của RemoveListener.
        /// </summary>
        /// <param name="self">Sự kiện Dropdown.</param>
        /// <param name="call">Hành động sẽ không còn được thực thi khi sự kiện xảy ra.</param>
        public static void Remove(this TMP_Dropdown.DropdownEvent self, UnityAction<int> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// Phương thức này là cách viết rút gọn của SetListener.
        /// </summary>
        /// <param name="self">Sự kiện Dropdown.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_Dropdown.DropdownEvent self, UnityAction<int> call)
        {
            self.SetListener(call);
        }
    }
}