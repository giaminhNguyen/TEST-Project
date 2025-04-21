using UnityEngine;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho RectTransform
    /// </summary>
    public static partial class UIExtensionMethods
    {
        //================================================================================
        // anchoredPosition
        //================================================================================
        /// <summary>
        /// Đặt vị trí neo về gốc tọa độ (0,0).
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        public static void ResetAnchoredPosition(this RectTransform self) => self.anchoredPosition = Vector2.zero;

        /// <summary>
        /// Đặt giá trị tọa độ x của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetAnchoredPositionX(this RectTransform self, float x) => self.anchoredPosition = new(x, self.anchoredPosition.y);

        /// <summary>
        /// Đặt giá trị tọa độ y của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchoredPositionY(this RectTransform self, float y) => self.anchoredPosition = new(self.anchoredPosition.x, y);

        /// <summary>
        /// Đặt giá trị tọa độ của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetAnchoredPosition(this RectTransform self, Vector2 v) => self.anchoredPosition = v;

        /// <summary>
        /// Đặt giá trị tọa độ x và y của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchoredPosition(this RectTransform self, float x, float y) => self.anchoredPosition = new(x, y);

        /// <summary>
        /// Thêm giá trị x vào tọa độ x của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void AddAnchoredPositionX(this RectTransform self, float x) => self.anchoredPosition += new Vector2(x, 0);

        /// <summary>
        /// Thêm giá trị y vào tọa độ y của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void AddAnchoredPositionY(this RectTransform self, float y) => self.anchoredPosition += new Vector2(0, y);

        /// <summary>
        /// Thêm giá trị Vector2 vào tọa độ của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void AddAnchoredPosition(this RectTransform self, Vector2 v) => self.anchoredPosition += v;

        /// <summary>
        /// Thêm giá trị x và y vào tọa độ của vị trí neo.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void AddAnchoredPosition(this RectTransform self, float x, float y) => self.anchoredPosition += new Vector2(x, y);

        //================================================================================
        // offsetMax
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của offsetMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetOffsetMaxX(this RectTransform self, float x) => self.offsetMax = new(x, self.offsetMax.y);

        /// <summary>
        /// Đặt giá trị y của offsetMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetOffsetMaxY(this RectTransform self, float y) => self.offsetMax = new(self.offsetMax.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của offsetMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetOffsetMax(this RectTransform self, Vector2 v) => self.offsetMax = v;

        /// <summary>
        /// Đặt giá trị x và y của offsetMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetOffsetMax(this RectTransform self, float x, float y) => self.offsetMax = new(x, y);

        //================================================================================
        // offsetMin
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của offsetMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetOffsetMinX(this RectTransform self, float x) => self.offsetMin = new(x, self.offsetMin.y);

        /// <summary>
        /// Đặt giá trị y của offsetMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetOffsetMinY(this RectTransform self, float y) => self.offsetMin = new(self.offsetMin.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của offsetMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetOffsetMin(this RectTransform self, Vector2 v) => self.offsetMin = v;

        /// <summary>
        /// Đặt giá trị x và y của offsetMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetOffsetMin(this RectTransform self, float x, float y) => self.offsetMin = new(x, y);

        //================================================================================
        // anchorMin
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của anchorMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetAnchorMinX(this RectTransform self, float x) => self.anchorMin = new(x, self.anchorMin.y);

        /// <summary>
        /// Đặt giá trị y của anchorMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchorMinY(this RectTransform self, float y) => self.anchorMin = new(self.anchorMin.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của anchorMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetAnchorMin(this RectTransform self, Vector2 v) => self.anchorMin = v;

        /// <summary>
        /// Đặt giá trị x và y của anchorMin.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchorMin(this RectTransform self, float x, float y) => self.anchorMin = new(x, y);

        //================================================================================
        // anchorMax
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của anchorMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetAnchorMaxX(this RectTransform self, float x) => self.anchorMax = new(x, self.anchorMax.y);

        /// <summary>
        /// Đặt giá trị y của anchorMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchorMaxY(this RectTransform self, float y) => self.anchorMax = new(self.anchorMax.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của anchorMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetAnchorMax(this RectTransform self, Vector2 v) => self.anchorMax = v;

        /// <summary>
        /// Đặt giá trị x và y của anchorMax.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetAnchorMax(this RectTransform self, float x, float y) => self.anchorMax = new(x, y);

        //================================================================================
        // pivot
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của pivot.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetPivotX(this RectTransform self, float x) => self.pivot = new(x, self.pivot.y);

        /// <summary>
        /// Đặt giá trị y của pivot.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetPivotY(this RectTransform self, float y) => self.pivot = new(self.pivot.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của pivot.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetPivot(this RectTransform self, Vector2 v) => self.pivot = v;

        /// <summary>
        /// Đặt giá trị x và y của pivot.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetPivot(this RectTransform self, float x, float y) => self.pivot = new(x, y);

        //================================================================================
        // sizeDelta
        //================================================================================
        /// <summary>
        /// Đặt giá trị x của sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void SetSizeDeltaX(this RectTransform self, float x) => self.sizeDelta = new(x, self.sizeDelta.y);

        /// <summary>
        /// Đặt giá trị y của sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetSizeDeltaY(this RectTransform self, float y) => self.sizeDelta = new(self.sizeDelta.x, y);

        /// <summary>
        /// Đặt giá trị Vector2 của sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void SetSizeDelta(this RectTransform self, Vector2 v) => self.sizeDelta = v;

        /// <summary>
        /// Đặt giá trị x và y của sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void SetSizeDelta(this RectTransform self, float x, float y) => self.sizeDelta = new(x, y);

        /// <summary>
        /// Thêm giá trị x vào sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        public static void AddSizeDeltaX(this RectTransform self, float x) => self.sizeDelta += new Vector2(x, 0);

        /// <summary>
        /// Thêm giá trị y vào sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="y">Giá trị y.</param>
        public static void AddSizeDeltaY(this RectTransform self, float y) => self.sizeDelta += new Vector2(0, y);

        /// <summary>
        /// Thêm giá trị Vector2 vào sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="v">Giá trị Vector2.</param>
        public static void AddSizeDelta(this RectTransform self, Vector2 v) => self.sizeDelta += v;

        /// <summary>
        /// Thêm giá trị x và y vào sizeDelta.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <param name="x">Giá trị x.</param>
        /// <param name="y">Giá trị y.</param>
        public static void AddSizeDelta(this RectTransform self, float x, float y) => self.sizeDelta += new Vector2(x, y);


        public static float CalculateActualSizeX(this RectTransform self,float x)
        {
            var subtract =self.rect.width - self.sizeDelta.x;
            return subtract == 0 ? x : x - subtract;
        }
        
        public static float CalculateActualSizeY(this RectTransform self,float y)
        {
            var subtract= self.rect.height - self.sizeDelta.y;
            return subtract == 0 ? y : y - subtract;
        }
        
        public static Vector2 CalculateActualSize(this RectTransform self,float x, float y)
        {
            return new Vector2(CalculateActualSizeX(self, x), CalculateActualSizeY(self, y));
        }
        
        public static Vector2 CalculateActualSize(this RectTransform self,Vector2 sizeData)
        {
            return new Vector2(CalculateActualSizeX(self, sizeData.x), CalculateActualSizeY(self, sizeData.y));
        }

        
        //================================================================================
        // Các phương thức tĩnh
        //================================================================================
        /// <summary>
        /// Trả về vị trí của cạnh trái.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <returns>Vị trí của cạnh trái.</returns>
        public static float GetAnchoredPositionLeft(this RectTransform self)
        {
            return self.anchoredPosition.x - self.sizeDelta.x * self.pivot.x * self.localScale.x;
        }

        /// <summary>
        /// Trả về vị trí của cạnh phải.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <returns>Vị trí của cạnh phải.</returns>
        public static float GetAnchoredPositionRight(this RectTransform self)
        {
            return self.anchoredPosition.x + self.sizeDelta.x * (1 - self.pivot.x) * self.localScale.x;
        }

        /// <summary>
        /// Trả về vị trí của cạnh dưới.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <returns>Vị trí của cạnh dưới.</returns>
        public static float GetAnchoredPositionBottom(this RectTransform self)
        {
            return self.anchoredPosition.y - self.sizeDelta.y * self.pivot.y * self.localScale.y;
        }

        /// <summary>
        /// Trả về vị trí của cạnh trên.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <returns>Vị trí của cạnh trên.</returns>
        public static float GetAnchoredPositionTop(this RectTransform self)
        {
            return self.anchoredPosition.y + self.sizeDelta.y * (1 - self.pivot.y) * self.localScale.y;
        }

        /// <summary>
        /// Trả về một hình chữ nhật biểu diễn các cạnh.
        /// </summary>
        /// <param name="self">Đối tượng RectTransform.</param>
        /// <returns>Hình chữ nhật biểu diễn các cạnh.</returns>
        public static Rect GetAnchoredEdge(this RectTransform self)
        {
            var rect = new Rect
            {
                xMin = self.GetAnchoredPositionLeft(),
                xMax = self.GetAnchoredPositionRight(),
                yMin = self.GetAnchoredPositionBottom(),
                yMax = self.GetAnchoredPositionTop(),
            };

            return rect;
        }
    }
}