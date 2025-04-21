using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu ScrollRect
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Các hàm(static)
        //================================================================================
        
        /// <summary>
        /// Đặt lại vị trí cuộn lên đầu
        /// </summary>
        /// <param name="self">ScrollRect cần đặt lại vị trí cuộn.</param>
        public static void ResetScrollPositionToTop(this ScrollRect self)
        {
            self.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// Đặt lại vị trí cuộn xuống cuối
        /// </summary>
        /// <param name="self">ScrollRect cần đặt lại vị trí cuộn.</param>
        public static void ResetScrollPositionToBottom(this ScrollRect self)
        {
            self.verticalNormalizedPosition = 0;
        }

        /// <summary>
        /// Đặt lại vị trí cuộn sang trái
        /// </summary>
        /// <param name="self">ScrollRect cần đặt lại vị trí cuộn.</param>
        public static void ResetScrollPositionToLeft(this ScrollRect self)
        {
            self.horizontalNormalizedPosition = 0;
        }

        /// <summary>
        /// Đặt lại vị trí cuộn sang phải
        /// </summary>
        /// <param name="self">ScrollRect cần đặt lại vị trí cuộn.</param>
        public static void ResetScrollPositionToRight(this ScrollRect self)
        {
            self.horizontalNormalizedPosition = 1;
        }
    }
}