using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu Toggle
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Các hàm tĩnh
        //================================================================================
        
        /// <summary>
        /// Thiết lập listener
        /// </summary>
        /// <param name="self">Sự kiện Toggle cần thiết lập listener.</param>
        /// <param name="call">Phương thức sẽ được gọi khi sự kiện xảy ra.</param>
        public static void SetListener(this Toggle.ToggleEvent self, UnityAction<bool> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener
        /// </summary>
        /// <param name="self">Sự kiện Toggle cần thêm listener.</param>
        /// <param name="call">Phương thức sẽ được gọi khi sự kiện xảy ra.</param>
        public static void Add(this Toggle.ToggleEvent self, UnityAction<bool> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener
        /// </summary>
        /// <param name="self">Sự kiện Toggle cần gỡ bỏ listener.</param>
        /// <param name="call">Phương thức sẽ bị gỡ bỏ khỏi sự kiện.</param>
        public static void Remove(this Toggle.ToggleEvent self, UnityAction<bool> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener
        /// </summary>
        /// <param name="self">Sự kiện Toggle cần thiết lập listener.</param>
        /// <param name="call">Phương thức sẽ được gọi khi sự kiện xảy ra.</param>
        public static void Set(this Toggle.ToggleEvent self, UnityAction<bool> call)
        {
            self.SetListener(call);
        }
    }
}