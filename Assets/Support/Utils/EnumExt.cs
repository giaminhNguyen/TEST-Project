using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UltimateHelper
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Kiểm tra xem giá trị int có hợp lệ trong một enum hay không.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="value">Giá trị cần kiểm tra.</param>
        /// <returns>True nếu giá trị hợp lệ, ngược lại trả về False.</returns>
        public static bool IsValidEnumValue<TEnum>(this int value) where TEnum : Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        /// <summary>
        /// Kiểm tra xem chuỗi có hợp lệ trong một enum hay không.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="value">Chuỗi cần kiểm tra.</param>
        /// <returns>True nếu giá trị hợp lệ, ngược lại trả về False.</returns>
        public static bool IsValidEnumValue<TEnum>(this string value) where TEnum : Enum
        {
            return Enum.TryParse(typeof(TEnum), value, true, out _);
        }

        /// <summary>
        /// Lấy danh sách tất cả các giá trị trong enum.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <returns>Danh sách các giá trị enum dưới dạng IEnumerable.</returns>
        public static IEnumerable<TEnum> GetAllValues<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Lấy giá trị enum từ tên chuỗi.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="name">Tên của enum value.</param>
        /// <returns>Giá trị enum tương ứng.</returns>
        /// <exception cref="ArgumentException">Ném lỗi nếu tên không hợp lệ.</exception>
        public static TEnum GetEnumByName<TEnum>(this string name) where TEnum : Enum
        {
            if (Enum.TryParse(typeof(TEnum), name, true, out var result))
            {
                return (TEnum)result;
            }
            throw new ArgumentException($"'{name}' không phải là tên hợp lệ của {typeof(TEnum).Name}");
        }

        /// <summary>
        /// Lấy giá trị enum từ giá trị int.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="value">Giá trị int.</param>
        /// <returns>Enum tương ứng.</returns>
        /// <exception cref="ArgumentException">Ném lỗi nếu giá trị không hợp lệ.</exception>
        public static TEnum GetEnumByValue<TEnum>(this int value) where TEnum : Enum
        {
            if (value.IsValidEnumValue<TEnum>())
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), value);
            }
            throw new ArgumentException($"'{value}' không phải là giá trị hợp lệ của {typeof(TEnum).Name}");
        }

        /// <summary>
        /// Lấy tên (name) của enum value.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="enumValue">Giá trị enum.</param>
        /// <returns>Tên tương ứng của enum value.</returns>
        public static string GetName<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return Enum.GetName(typeof(TEnum), enumValue) ?? string.Empty;
        }

        /// <summary>
        /// Kiểm tra một giá trị enum có thuộc tập giá trị cho trước hay không.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <param name="enumValue">Giá trị enum cần kiểm tra.</param>
        /// <param name="validValues">Danh sách giá trị hợp lệ.</param>
        /// <returns>True nếu thuộc tập giá trị, ngược lại trả về False.</returns>
        public static bool IsOneOf<TEnum>(this TEnum enumValue, params TEnum[] validValues) where TEnum : Enum
        {
            return validValues.Contains(enumValue);
        }

        /// <summary>
        /// Lấy attribute được gắn vào enum value.
        /// </summary>
        /// <typeparam name="TAttribute">Loại attribute cần lấy.</typeparam>
        /// <param name="enumValue">Giá trị enum.</param>
        /// <returns>Attribute tương ứng, hoặc null nếu không tồn tại.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            return fieldInfo?.GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// Chuyển đổi enum thành dictionary với key là giá trị int, value là tên.
        /// </summary>
        /// <typeparam name="TEnum">Kiểu enum.</typeparam>
        /// <returns>Dictionary chứa các cặp key-value của enum.</returns>
        public static Dictionary<int, string> ToDictionary<TEnum>() where TEnum : Enum
        {
            return GetAllValues<TEnum>().ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
        }
        
        public static int GetValue<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return Convert.ToInt32(enumValue);
        }
        
        /// <summary>
        /// Chuyển đổi giữa hai kiểu enum dựa trên giá trị của chúng (value).
        /// </summary>
        /// <typeparam name="TSource">Kiểu enum nguồn</typeparam>
        /// <typeparam name="TDestination">Kiểu enum đích</typeparam>
        /// <param name="sourceEnum">Giá trị enum nguồn</param>
        /// <returns>Giá trị enum đích có cùng value</returns>
        public static TDestination ConvertByValue<TSource, TDestination>(this TSource sourceEnum)
                where TSource : Enum
                where TDestination : Enum
        {
            // Lấy giá trị int của enum nguồn
            var sourceValue = sourceEnum.GetValue();
            return sourceValue.GetEnumByValue<TDestination>();
        }

        /// <summary>
        /// Ánh xạ enum sang một kiểu khác dựa trên logic tùy chỉnh.
        /// </summary>
        /// <typeparam name="TSource">Enum nguồn.</typeparam>
        /// <typeparam name="TDestination">Kiểu đích.</typeparam>
        /// <param name="sourceEnum">Enum nguồn cần ánh xạ.</param>
        /// <param name="mappingFunc">Hàm ánh xạ.</param>
        /// <returns>Kết quả ánh xạ.</returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource sourceEnum, Func<TSource, TDestination> mappingFunc)
            where TSource : Enum
        {
            return mappingFunc(sourceEnum);
        }
    }
}
