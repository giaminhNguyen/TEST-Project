using UnityEngine;
using UnityEngine.UI;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho kiểu Graphic
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // anchoredPosition
        //================================================================================
        /// <summary>
        /// Đặt lại vị trí neo của phần tử
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static void ResetAnchoredPosition(this Graphic self) => self.rectTransform.ResetAnchoredPosition();

        /// <summary>
        /// Đặt giá trị trục X của vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetAnchoredPositionX(this Graphic self, float x) => self.rectTransform.SetAnchoredPositionX(x);

        /// <summary>
        /// Đặt giá trị trục Y của vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchoredPositionY(this Graphic self, float y) => self.rectTransform.SetAnchoredPositionY(y);

        /// <summary>
        /// Đặt giá trị vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị vị trí neo</param>
        public static void SetAnchoredPosition(this Graphic self, Vector2 v) => self.rectTransform.SetAnchoredPosition(v);

        /// <summary>
        /// Đặt giá trị vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchoredPosition(this Graphic self, float x, float y) => self.rectTransform.SetAnchoredPosition(x, y);

        /// <summary>
        /// Thêm giá trị trục X vào vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void AddAnchoredPositionX(this Graphic self, float x) => self.rectTransform.AddAnchoredPositionX(x);

        /// <summary>
        /// Thêm giá trị trục Y vào vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void AddAnchoredPositionY(this Graphic self, float y) => self.rectTransform.AddAnchoredPositionY(y);

        /// <summary>
        /// Thêm giá trị vào vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị vị trí neo</param>
        public static void AddAnchoredPosition(this Graphic self, Vector2 v) => self.rectTransform.AddAnchoredPosition(v);

        /// <summary>
        /// Thêm giá trị vào vị trí neo
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void AddAnchoredPosition(this Graphic self, float x, float y) => self.rectTransform.AddAnchoredPosition(x, y);

        //================================================================================
        // offsetMax
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của offsetMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetOffsetMaxX(this Graphic self, float x) => self.rectTransform.SetOffsetMaxX(x);

        /// <summary>
        /// Đặt giá trị trục Y của offsetMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetOffsetMaxY(this Graphic self, float y) => self.rectTransform.SetOffsetMaxY(y);

        /// <summary>
        /// Đặt giá trị offsetMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị offsetMax</param>
        public static void SetOffsetMax(this Graphic self, Vector2 v) => self.rectTransform.SetOffsetMax(v);

        /// <summary>
        /// Đặt giá trị offsetMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetOffsetMax(this Graphic self, float x, float y) => self.rectTransform.SetOffsetMax(x, y);

        //================================================================================
        // offsetMin
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của offsetMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetOffsetMinX(this Graphic self, float x) => self.rectTransform.SetOffsetMinX(x);

        /// <summary>
        /// Đặt giá trị trục Y của offsetMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetOffsetMinY(this Graphic self, float y) => self.rectTransform.SetOffsetMinY(y);

        /// <summary>
        /// Đặt giá trị offsetMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị offsetMin</param>
        public static void SetOffsetMin(this Graphic self, Vector2 v) => self.rectTransform.SetOffsetMin(v);

        /// <summary>
        /// Đặt giá trị offsetMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetOffsetMin(this Graphic self, float x, float y) => self.rectTransform.SetOffsetMin(x, y);

        //================================================================================
        // anchorMin
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của anchorMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetAnchorMinX(this Graphic self, float x) => self.rectTransform.SetAnchorMinX(x);

        /// <summary>
        /// Đặt giá trị trục Y của anchorMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchorMinY(this Graphic self, float y) => self.rectTransform.SetAnchorMinY(y);

        /// <summary>
        /// Đặt giá trị anchorMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị anchorMin</param>
        public static void SetAnchorMin(this Graphic self, Vector2 v) => self.rectTransform.SetAnchorMin(v);

        /// <summary>
        /// Đặt giá trị anchorMin
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchorMin(this Graphic self, float x, float y) => self.rectTransform.SetAnchorMin(x, y);

        //================================================================================
        // anchorMax
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của anchorMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetAnchorMaxX(this Graphic self, float x) => self.rectTransform.SetAnchorMaxX(x);

        /// <summary>
        /// Đặt giá trị trục Y của anchorMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchorMaxY(this Graphic self, float y) => self.rectTransform.SetAnchorMaxY(y);

        /// <summary>
        /// Đặt giá trị anchorMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị anchorMax</param>
        public static void SetAnchorMax(this Graphic self, Vector2 v) => self.rectTransform.SetAnchorMax(v);

        /// <summary>
        /// Đặt giá trị anchorMax
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetAnchorMax(this Graphic self, float x, float y) => self.rectTransform.SetAnchorMax(x, y);

        //================================================================================
        // pivot
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của pivot
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetPivotX(this Graphic self, float x) => self.rectTransform.SetPivotX(x);

        /// <summary>
        /// Đặt giá trị trục Y của pivot
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetPivotY(this Graphic self, float y) => self.rectTransform.SetPivotY(y);

        /// <summary>
        /// Đặt giá trị pivot
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị pivot</param>
        public static void SetPivot(this Graphic self, Vector2 v) => self.rectTransform.SetPivot(v);

        /// <summary>
        /// Đặt giá trị pivot
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetPivot(this Graphic self, float x, float y) => self.rectTransform.SetPivot(x, y);

        //================================================================================
        // sizeDelta
        //================================================================================
        /// <summary>
        /// Đặt giá trị trục X của sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void SetSizeDeltaX(this Graphic self, float x) => self.rectTransform.SetSizeDeltaX(x);

        /// <summary>
        /// Đặt giá trị trục Y của sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetSizeDeltaY(this Graphic self, float y) => self.rectTransform.SetSizeDeltaY(y);

        /// <summary>
        /// Đặt giá trị sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị sizeDelta</param>
        public static void SetSizeDelta(this Graphic self, Vector2 v) => self.rectTransform.SetSizeDelta(v);

        /// <summary>
        /// Đặt giá trị sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void SetSizeDelta(this Graphic self, float x, float y) => self.rectTransform.SetSizeDelta(x, y);

        /// <summary>
        /// Thêm giá trị trục X vào sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        public static void AddSizeDeltaX(this Graphic self, float x) => self.rectTransform.AddSizeDeltaX(x);

        /// <summary>
        /// Thêm giá trị trục Y vào sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void AddSizeDeltaY(this Graphic self, float y) => self.rectTransform.AddSizeDeltaY(y);

        /// <summary>
        /// Thêm giá trị vào sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="v">Giá trị sizeDelta</param>
        public static void AddSizeDelta(this Graphic self, Vector2 v) => self.rectTransform.AddSizeDelta(v);

        /// <summary>
        /// Thêm giá trị vào sizeDelta
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="x">Giá trị trục X</param>
        /// <param name="y">Giá trị trục Y</param>
        public static void AddSizeDelta(this Graphic self, float x, float y) => self.rectTransform.AddSizeDelta(x, y);

        //================================================================================
        // Các phương thức (static)
        //================================================================================
        /// <summary>
        /// Trả về vị trí bên trái
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static float GetAnchoredPositionLeft(this Graphic self)
        {
            return self.rectTransform.GetAnchoredPositionLeft();
        }

        /// <summary>
        /// Trả về vị trí bên phải
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static float GetAnchoredPositionRight(this Graphic self)
        {
            return self.rectTransform.GetAnchoredPositionRight();
        }

        /// <summary>
        /// Trả về vị trí bên dưới
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static float GetAnchoredPositionBottom(this Graphic self)
        {
            return self.rectTransform.GetAnchoredPositionBottom();
        }

        /// <summary>
        /// Trả về vị trí bên trên
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static float GetAnchoredPositionTop(this Graphic self)
        {
            return self.rectTransform.GetAnchoredPositionTop();
        }

        /// <summary>
        /// Trả về hình chữ nhật thể hiện các cạnh
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        public static Rect GetAnchoredEdge(this Graphic self)
        {
            return self.rectTransform.GetAnchoredEdge();
        }

        /// <summary>
        /// Đặt giá trị độ trong suốt
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="alpha">Giá trị độ trong suốt</param>
        public static void SetAlpha(this Graphic self, float alpha)
        {
            var color = self.color;
            color.a = alpha;
            self.color = color;
        }

        /// <summary>
        /// Đặt màu sắc
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="color">Màu sắc</param>
        public static void SetColor(this Graphic self, Color color)
        {
            self.color = color;
        }

        /// <summary>
        /// Đặt màu sắc
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="r">Giá trị đỏ</param>
        /// <param name="g">Giá trị xanh lá</param>
        /// <param name="b">Giá trị xanh dương</param>
        /// <param name="a">Giá trị alpha</param>
        public static void SetColor(this Graphic self, float r, float g, float b, float a)
        {
            self.color = new Color(r, g, b, a);
        }

        /// <summary>
        /// Đặt màu sắc
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="r">Giá trị đỏ</param>
        /// <param name="g">Giá trị xanh lá</param>
        /// <param name="b">Giá trị xanh dương</param>
        /// <param name="a">Giá trị alpha</param>
        public static void SetColor
        (
                this Graphic self,
                byte         r,
                byte         g,
                byte         b,
                byte         a
        )
        {
            self.color = new Color32( r, g, b, a );
        }

        /// <summary>
        /// Đặt màu sắc
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="r">Giá trị đỏ</param>
        /// <param name="g">Giá trị xanh lá</param>
        /// <param name="b">Giá trị xanh dương</param>
        public static void SetColor( this Graphic self, float r, float g, float b )
        {
            self.color = new( r, g, b, self.color.a );
        }

        /// <summary>
        /// Đặt màu sắc
        /// </summary>
        /// <param name="self">Phần tử Graphic cần thao tác</param>
        /// <param name="r">Giá trị đỏ</param>
        /// <param name="g">Giá trị xanh lá</param>
        /// <param name="b">Giá trị xanh dương</param>
        public static void SetColor( this Graphic self, byte r, byte g, byte b )
        {
            self.color = new Color32( r, g, b, ( byte )( self.color.a * 255 ) );
        }
    }
}