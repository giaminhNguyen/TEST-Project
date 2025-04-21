using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu InputField
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Hàm (tĩnh)
        //================================================================================
        
        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện submit của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện submit của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện submit của InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện submit của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.SetListener(call);
        }
    }
}