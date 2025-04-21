using UnityEngine;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu Image
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Hàm (tĩnh)
        //================================================================================
        
        /// <summary>
        /// Thiết lập sprite.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSprite(this Image self, Sprite sprite)
        {
            self.sprite = sprite;
        }

        /// <summary>
        /// Thiết lập sprite nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteIfNotNull(this Image self, Sprite sprite)
        {
            if (self == null) return;
            self.SetSprite(sprite);
        }

        /// <summary>
        /// Thiết lập sprite và điều chỉnh kích thước.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndSnap(this Image self, Sprite sprite)
        {
            self.sprite = sprite;
            self.SetNativeSize();
        }

        /// <summary>
        /// Thiết lập sprite và điều chỉnh kích thước nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndSnapIfNotNull(this Image self, Sprite sprite)
        {
            if (self == null) return;
            self.SetSpriteAndSnap(sprite);
        }

        /// <summary>
        /// Thiết lập sprite và kích hoạt đối tượng Image.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndActive(this Image self, Sprite sprite)
        {
            self.sprite = sprite;
            self.gameObject.SetActive(sprite != null);
        }

        /// <summary>
        /// Thiết lập sprite và kích hoạt đối tượng Image nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndActiveIfNotNull(this Image self, Sprite sprite)
        {
            if (self == null) return;
            self.SetSpriteAndActive(sprite);
        }

        /// <summary>
        /// Thiết lập sprite và bật/tắt đối tượng Image.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndEnabled(this Image self, Sprite sprite)
        {
            self.sprite = sprite;
            self.enabled = sprite != null;
        }

        /// <summary>
        /// Thiết lập sprite và bật/tắt đối tượng Image nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="sprite">Sprite cần thiết lập.</param>
        public static void SetSpriteAndEnabledIfNotNull(this Image self, Sprite sprite)
        {
            if (self == null) return;
            self.SetSpriteAndEnabled(sprite);
        }

        /// <summary>
        /// Thiết lập fillAmount.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="fillAmount">Giá trị fillAmount cần thiết lập.</param>
        public static void SetFillAmount(this Image self, float fillAmount)
        {
            self.fillAmount = fillAmount;
        }

        /// <summary>
        /// Thiết lập fillAmount nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="fillAmount">Giá trị fillAmount cần thiết lập.</param>
        public static void SetFillAmountIfNotNull(this Image self, float fillAmount)
        {
            if (self == null) return;
            self.SetFillAmount(fillAmount);
        }

        /// <summary>
        /// Thiết lập fillAmount.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="current">Giá trị hiện tại.</param>
        /// <param name="max">Giá trị tối đa.</param>
        public static void SetFillAmount(this Image self, int current, int max)
        {
            self.fillAmount = max == 0 ? 0 : (float)current / max;
        }

        /// <summary>
        /// Thiết lập fillAmount nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="current">Giá trị hiện tại.</param>
        /// <param name="max">Giá trị tối đa.</param>
        public static void SetFillAmountIfNotNull(this Image self, int current, int max)
        {
            if (self == null) return;
            self.SetFillAmount(current, max);
        }

        /// <summary>
        /// Thiết lập fillAmount.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="current">Giá trị hiện tại.</param>
        /// <param name="max">Giá trị tối đa.</param>
        public static void SetFillAmount(this Image self, long current, long max)
        {
            self.fillAmount = max == 0 ? 0 : (float)current / max;
        }

        /// <summary>
        /// Thiết lập fillAmount nếu đối tượng Image không null.
        /// </summary>
        /// <param name="self">Đối tượng Image.</param>
        /// <param name="current">Giá trị hiện tại.</param>
        /// <param name="max">Giá trị tối đa.</param>
        public static void SetFillAmountIfNotNull(this Image self, long current, long max)
        {
            if (self == null) return;
            self.SetFillAmount(current, max);
        }
    }
}