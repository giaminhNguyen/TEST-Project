using UnityEngine.Events;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu Slider
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Các hàm(static)
        //================================================================================
        
        /// <summary>
        /// Thiết lập listener
        /// </summary>
        /// <param name="self">Slider.SliderEvent cần thiết lập listener.</param>
        /// <param name="call">Hàm UnityAction<float> cần thêm vào listener.</param>
        public static void SetListener(this Slider.SliderEvent self, UnityAction<float> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener
        /// </summary>
        /// <param name="self">Slider.SliderEvent cần thêm listener.</param>
        /// <param name="call">Hàm UnityAction<float> cần thêm vào listener.</param>
        public static void Add(this Slider.SliderEvent self, UnityAction<float> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Xóa listener
        /// </summary>
        /// <param name="self">Slider.SliderEvent cần xóa listener.</param>
        /// <param name="call">Hàm UnityAction<float> cần xóa khỏi listener.</param>
        public static void Remove(this Slider.SliderEvent self, UnityAction<float> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener (cách viết tắt của SetListener)
        /// </summary>
        /// <param name="self">Slider.SliderEvent cần thiết lập listener.</param>
        /// <param name="call">Hàm UnityAction<float> cần thêm vào listener.</param>
        public static void Set(this Slider.SliderEvent self, UnityAction<float> call)
        {
            self.SetListener(call);
        }
    }
}