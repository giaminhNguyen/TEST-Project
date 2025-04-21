using UnityEngine;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu RawImage
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // Hàm (tĩnh)
        //================================================================================
        
        /// <summary>
        /// Thiết lập texture.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTexture(this RawImage self, Texture texture)
        {
            self.texture = texture;
        }

        /// <summary>
        /// Thiết lập texture nếu không null.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureIfNotNull(this RawImage self, Texture texture)
        {
            if (self == null) return;
            self.SetTexture(texture);
        }

        /// <summary>
        /// Thiết lập texture và điều chỉnh kích thước.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndSnap(this RawImage self, Texture texture)
        {
            self.texture = texture;
            self.SetNativeSize();
        }

        /// <summary>
        /// Thiết lập texture và điều chỉnh kích thước nếu không null.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndSnapIfNotNull(this RawImage self, Texture texture)
        {
            if (self == null) return;
            self.SetTextureAndSnap(texture);
        }

        /// <summary>
        /// Thiết lập texture và trạng thái hoạt động.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndActive(this RawImage self, Texture texture)
        {
            self.texture = texture;
            self.gameObject.SetActive(texture != null);
        }

        /// <summary>
        /// Thiết lập texture và trạng thái hoạt động nếu không null.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndActiveIfNotNull(this RawImage self, Texture texture)
        {
            if (self == null) return;
            self.SetTextureAndActive(texture);
        }

        /// <summary>
        /// Thiết lập texture và trạng thái enabled.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndEnabled(this RawImage self, Texture texture)
        {
            self.texture = texture;
            self.enabled = texture != null;
        }

        /// <summary>
        /// Thiết lập texture và trạng thái enabled nếu không null.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="texture">Texture cần thiết lập.</param>
        public static void SetTextureAndEnabledIfNotNull(this RawImage self, Texture texture)
        {
            if (self == null) return;
            self.SetTextureAndEnabled(texture);
        }

        /// <summary>
        /// Trả về giá trị uvRect.x.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        public static float GetUvRectX(this RawImage self)
        {
            return self.uvRect.x;
        }

        /// <summary>
        /// Trả về giá trị uvRect.y.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        public static float GetUvRectY(this RawImage self)
        {
            return self.uvRect.y;
        }

        /// <summary>
        /// Trả về giá trị uvRect.width.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        public static float GetUvRectWidth(this RawImage self)
        {
            return self.uvRect.width;
        }

        /// <summary>
        /// Trả về giá trị uvRect.height.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        public static float GetUvRectHeight(this RawImage self)
        {
            return self.uvRect.height;
        }

        /// <summary>
        /// Thiết lập giá trị uvRect.x.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="value">Giá trị cần thiết lập.</param>
        public static void SetUvRectX(this RawImage self, float value)
        {
            var uvRect = self.uvRect;
            uvRect.x = value;
            self.uvRect = uvRect;
        }

        /// <summary>
        /// Thiết lập giá trị uvRect.y.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="value">Giá trị cần thiết lập.</param>
        public static void SetUvRectY(this RawImage self, float value)
        {
            var uvRect = self.uvRect;
            uvRect.y = value;
            self.uvRect = uvRect;
        }

        /// <summary>
        /// Thiết lập giá trị uvRect.width.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="value">Giá trị cần thiết lập.</param>
        public static void SetUvRectWidth(this RawImage self, float value)
        {
            var uvRect = self.uvRect;
            uvRect.width = value;
            self.uvRect = uvRect;
        }

        /// <summary>
        /// Thiết lập giá trị uvRect.height.
        /// </summary>
        /// <param name="self">Đối tượng RawImage.</param>
        /// <param name="value">Giá trị cần thiết lập.</param>
        public static void SetUvRectHeight(this RawImage self, float value)
        {
            var uvRect = self.uvRect;
            uvRect.height = value;
            self.uvRect = uvRect;
        }
    }
}