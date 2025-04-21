using System;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng để chuyển đổi chuỗi sang các kiểu dữ liệu khác nhau
    /// </summary>
    public static class StringParseExt
    {
        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu sbyte
        /// </summary>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <returns>Giá trị sbyte</returns>
        /// <example>
        /// var result = "123".ToSByte(); // Kết quả: 123 (kiểu sbyte)
        /// </example>
        public static sbyte ToSByte(this string s)
        {
            return sbyte.Parse(s);
        }

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu sbyte, trả về null nếu không thành công
        /// </summary>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <returns>Giá trị sbyte hoặc null nếu không chuyển đổi được</returns>
        /// <example>
        /// var result = "abc".ToSByteOrNull(); // Kết quả: null
        /// </example>
        public static sbyte? ToSByteOrNull(this string s)
        {
            if (sbyte.TryParse(s, out var result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu sbyte, trả về giá trị mặc định nếu không thành công
        /// </summary>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <returns>Giá trị sbyte hoặc 0 nếu không chuyển đổi được</returns>
        /// <example>
        /// var result = "abc".ToSByteOrDefault(); // Kết quả: 0
        /// </example>
        public static sbyte ToSByteOrDefault(this string s)
        {
            return ToSByteOrDefault(s, default);
        }

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu sbyte, trả về giá trị được chỉ định nếu không thành công
        /// </summary>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <param name="defaultValue">Giá trị mặc định khi chuyển đổi không thành công</param>
        /// <returns>Giá trị sbyte hoặc defaultValue nếu không chuyển đổi được</returns>
        /// <example>
        /// var result = "abc".ToSByteOrDefault(100); // Kết quả: 100
        /// </example>
        public static sbyte ToSByteOrDefault(this string s, sbyte defaultValue)
        {
            return sbyte.TryParse(s, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Kiểm tra chuỗi có thể chuyển đổi thành kiểu sbyte hay không
        /// </summary>
        /// <param name="s">Chuỗi cần kiểm tra</param>
        /// <returns>true nếu có thể chuyển đổi, ngược lại là false</returns>
        /// <example>
        /// var result = "123".IsSByte(); // Kết quả: true
        /// var result2 = "abc".IsSByte(); // Kết quả: false
        /// </example>
        public static bool IsSByte(this string s)
        {
            return sbyte.TryParse(s, out _);
        }

        // Các phương thức tương tự cho các kiểu dữ liệu khác
        // byte
        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu byte
        /// </summary>
        public static byte ToByte(this string s)
        {
            return byte.Parse(s);
        }

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu byte, trả về null nếu không thành công
        /// </summary>
        public static byte? ToByteOrNull(this string s)
        {
            if (byte.TryParse(s, out var result))
            {
                return result;
            }
            return null;
        }

        // ... [Các phương thức khác được lặp lại tương tự với documentation tiếng Việt]

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu enum
        /// </summary>
        /// <typeparam name="T">Kiểu enum cần chuyển đổi</typeparam>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <returns>Giá trị enum tương ứng</returns>
        /// <example>
        /// enum Color { Red, Green, Blue }
        /// var result = "Red".ToEnum<Color>(); // Kết quả: Color.Red
        /// </example>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return (T)Enum.Parse(typeof(T), s);
        }

        /// <summary>
        /// Chuyển đổi chuỗi thành kiểu enum, trả về null nếu không thành công
        /// </summary>
        /// <typeparam name="T">Kiểu enum cần chuyển đổi</typeparam>
        /// <param name="s">Chuỗi cần chuyển đổi</param>
        /// <returns>Giá trị enum hoặc null nếu không chuyển đổi được</returns>
        /// <example>
        /// enum Color { Red, Green, Blue }
        /// var result = "Invalid".ToEnumOrNull<Color>(); // Kết quả: null
        /// </example>
        public static T? ToEnumOrNull<T>(this string s) where T : struct
        {
            if (Enum.TryParse(s, out T result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra chuỗi có thể chuyển đổi thành kiểu enum chỉ định hay không
        /// </summary>
        /// <typeparam name="T">Kiểu enum cần kiểm tra</typeparam>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>true nếu có thể chuyển đổi, ngược lại là false</returns>
        /// <example>
        /// enum Color { Red, Green, Blue }
        /// var result = "Red".IsEnum<Color>(); // Kết quả: true
        /// var result2 = "Yellow".IsEnum<Color>(); // Kết quả: false
        /// </example>
        public static bool IsEnum<T>(this string value) where T : struct
        {
            return Enum.TryParse(value, out T _);
        }
    }
}