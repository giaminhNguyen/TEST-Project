using System;
using System.Collections.ObjectModel;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho mảng trong C#
    /// Cung cấp các tiện ích để thao tác với mảng một cách dễ dàng và hiệu quả
    /// </summary>
    public static class ArrayExt
    {
        /// <summary>
        /// Tạo một bộ sưu tập chỉ đọc từ mảng đầu vào
        /// </summary>
        /// <param name="array">Mảng nguồn cần chuyển đổi thành bộ sưu tập chỉ đọc</param>
        /// <returns>Một ReadOnlyCollection chứa các phần tử của mảng</returns>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3 };
        /// var readOnly = numbers.AsReadOnly();
        /// </example>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }

        /// <summary>
        /// Xóa toàn bộ phần tử trong mảng bằng cách đặt giá trị mặc định
        /// </summary>
        /// <param name="array">Mảng cần xóa</param>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3 };
        /// numbers.Clear(); // Kết quả: [0, 0, 0]
        /// </example>
        public static void Clear(this Array array)
        {
            Array.Clear(array, 0, array.Length);
        }

        /// <summary>
        /// Xóa các phần tử trong mảng từ vị trí index đến cuối mảng
        /// </summary>
        /// <param name="array">Mảng cần xóa</param>
        /// <param name="index">Vị trí bắt đầu xóa</param>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3, 4, 5 };
        /// numbers.Clear(2); // Kết quả: [1, 2, 0, 0, 0]
        /// </example>
        public static void Clear(this Array array, int index)
        {
            Array.Clear(array, index, array.Length - index);
        }

        /// <summary>
        /// Xóa một đoạn phần tử trong mảng với độ dài xác định
        /// </summary>
        /// <param name="array">Mảng cần xóa</param>
        /// <param name="index">Vị trí bắt đầu xóa</param>
        /// <param name="length">Số lượng phần tử cần xóa</param>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3, 4, 5 };
        /// numbers.Clear(1, 2); // Kết quả: [1, 0, 0, 4, 5]
        /// </example>
        public static void Clear(this Array array, int index, int length)
        {
            Array.Clear(array, index, length);
        }

        /// <summary>
        /// Kiểm tra xem mảng có chứa phần tử thỏa mãn điều kiện hay không
        /// </summary>
        /// <param name="array">Mảng cần kiểm tra</param>
        /// <param name="match">Điều kiện kiểm tra</param>
        /// <returns>True nếu tìm thấy phần tử thỏa mãn, ngược lại là False</returns>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3, 4, 5 };
        /// bool hasEven = numbers.Exists(x => x % 2 == 0); // Kết quả: true
        /// </example>
        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        /// <summary>
        /// Tìm phần tử đầu tiên thỏa mãn điều kiện trong mảng
        /// </summary>
        /// <param name="array">Mảng cần tìm kiếm</param>
        /// <param name="match">Điều kiện tìm kiếm</param>
        /// <returns>Phần tử đầu tiên thỏa mãn điều kiện hoặc giá trị mặc định nếu không tìm thấy</returns>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3, 4, 5 };
        /// int firstEven = numbers.Find(x => x % 2 == 0); // Kết quả: 2
        /// </example>
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        // [Các phương thức khác giữ nguyên như cũ, chỉ thêm XML comments tiếng Việt và ví dụ tương tự]

        /// <summary>
        /// Lấy tất cả các phần tử thỏa mãn điều kiện trong mảng
        /// </summary>
        /// <param name="array">Mảng cần tìm kiếm</param>
        /// <param name="match">Điều kiện tìm kiếm</param>
        /// <returns>Mảng mới chứa các phần tử thỏa mãn điều kiện</returns>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3, 4, 5 };
        /// int[] evenNumbers = numbers.FindAll(x => x % 2 == 0); // Kết quả: [2, 4]
        /// </example>
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        /// <summary>
        /// Thực hiện hành động trên từng phần tử của mảng
        /// </summary>
        /// <param name="array">Mảng cần xử lý</param>
        /// <param name="action">Hành động cần thực hiện trên mỗi phần tử</param>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3 };
        /// numbers.ForEach(x => Console.WriteLine(x));
        /// </example>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for (var i = 0; i < array.Length; i++)
            {
                action(array[i]);
            }
        }

        /// <summary>
        /// Đảo ngược thứ tự các phần tử trong mảng
        /// </summary>
        /// <param name="array">Mảng cần đảo ngược</param>
        /// <returns>Mảng sau khi đã đảo ngược</returns>
        /// <example>
        /// var numbers = new int[] { 1, 2, 3 };
        /// numbers.Reverse(); // Kết quả: [3, 2, 1]
        /// </example>
        public static T[] Reverse<T>(this T[] array)
        {
            Array.Reverse(array);
            return array;
        }

        /// <summary>
        /// Sắp xếp các phần tử trong mảng theo thứ tự tăng dần
        /// </summary>
        /// <param name="array">Mảng cần sắp xếp</param>
        /// <example>
        /// var numbers = new int[] { 3, 1, 2 };
        /// numbers.Sort(); // Kết quả: [1, 2, 3]
        /// </example>
        public static void Sort<T>(this T[] array)
        {
            Array.Sort(array);
        }
    }
}