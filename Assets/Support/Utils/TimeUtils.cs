using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace UltimateHelper
{
    /// <summary>
    /// Lớp tiện ích thời gian cung cấp các phương thức và thuộc tính hữu ích cho việc quản lý thời gian.
    /// </summary>
    public static class TimeUtils
    {
        // Định dạng ngày
        /// <summary>Định dạng ngày/tháng/năm (ví dụ: 07/01/2024)</summary>
        public const string DD_MM_YYYY = "dd/MM/yyyy";

        /// <summary>Định dạng ngày tháng năm (ví dụ: 7 January 2024)</summary>
        public const string DD_MMM_YYYY = "dd MMM yyyy";

        /// <summary>Định dạng ngày/tháng/năm (2 chữ số) (ví dụ: 07/01/24)</summary>
        public const string DD_MM_YY = "dd/MM/yy";

        // Định dạng giờ
        /// <summary>Định dạng giờ:phút:giây (24 giờ) (ví dụ: 14:30:00)</summary>
        public const string HH_MM_SS = "HH:mm:ss";

        /// <summary>Định dạng giờ:phút (24 giờ) (ví dụ: 14:30)</summary>
        public const string HH_MM = "HH:mm";

        /// <summary>Định dạng giờ:phút AM/PM (12 giờ) (ví dụ: 02:30 PM)</summary>
        public const string HH_MM_AMPM = "hh:mm tt";

        // Định dạng kết hợp
        /// <summary>Định dạng ngày/tháng/năm giờ:phút:giây (ví dụ: 07/01/2024 14:30:00)</summary>
        public const string DD_MM_YYYY_HH_MM_SS = "dd/MM/yyyy HH:mm:ss";

        /// <summary>Định dạng ngày tháng năm giờ:phút:giây (ví dụ: 7 January 2024 14:30:00)</summary>
        public const string DD_MMM_YYYY_HH_MM_SS = "dd MMM yyyy HH:mm:ss";

        /// <summary>Định dạng năm-tháng-ngày giờ:phút:giây (ví dụ: 2024-01-07 14:30:00)</summary>
        public const string YYYY_MM_DD_HH_MM_SS = "yyyy-MM-dd HH:mm:ss";
        
        /// <summary>
        /// Kiểm tra xem định dạng thời gian hiện tại là 12 giờ hay không.
        /// </summary>
        public static bool Is12HourFormat => DateTimeFormatInfo.CurrentInfo.ShortTimePattern.Contains("h");

        /// <summary>
        /// Ngày và giờ hiện tại theo định dạng 12 giờ (ví dụ: 10:30 AM).
        /// </summary>
        public static string CurrentShortTime12 => DateTime.Now.ToString("h:mm tt");

        /// <summary>
        /// Ngày và giờ hiện tại theo định dạng 24 giờ (ví dụ: 10:30).
        /// </summary>
        public static string CurrentShortTime24 => DateTime.Now.ToString("HH:mm");

        /// <summary>
        /// Ngày hiện tại theo định dạng "Ngày tháng năm" (ví dụ: 07/01/2024).
        /// </summary>
        public static string CurrentDate => DateTime.Now.ToString("dd/MM/yyyy");
        
        /// <summary>
        /// Ngày hiện tại theo định dạng "Ngày tháng năm" (ví dụ: 07/01/2024 12:03:11).
        /// </summary>
        public static string CurrentDateAndTime => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        
        
        
        /// <summary>
        /// Kiểm tra xem thời gian hiện tại có phải là ngày cuối tuần.
        /// </summary>
        /// <returns>True nếu là ngày cuối tuần, false nếu không.</returns>
        public static bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }
        
        /// <summary>
        /// Kiểm tra xem thời gian hiện tại có phải là ngày lễ.
        /// </summary>
        /// <param name="holidays">Danh sách ngày lễ.</param>
        /// <returns>True nếu là ngày lễ, false nếu không.</returns>
        public static bool IsHoliday(List<DateTime> holidays)
        {
            return holidays.Contains(DateTime.Now.Date);
        }
        
        /// <summary>
        /// Tính toán số ngày trong tháng.
        /// </summary>
        /// <param name="year">Năm.</param>
        /// <param name="month">Tháng.</param>
        /// <returns>Số ngày trong tháng.</returns>
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }
        
        /// <summary>
        /// Kiểm tra xem một năm có phải là năm nhuận.
        /// </summary>
        /// <param name="year">Năm cần kiểm tra.</param>
        /// <returns>True nếu là năm nhuận, false nếu không.</returns>
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
        
        /// <summary>
        /// Chuyển đổi DateTime sang mili giây.
        /// </summary>
        /// <param name="dateTime">Thời gian cần chuyển đổi.</param>
        /// <returns>Mili giây.</returns>
        public static long ConvertDateTimeToMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
        
        /// <summary>
        /// Chuyển đổi mili giây sang DateTime.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>DateTime tương ứng.</returns>
        public static DateTime ConvertMillisecondsToDateTime(long milliseconds)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
        }
        
        /// <summary>
        /// Lấy số ngày trong tháng từ mili giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>Số ngày trong tháng.</returns>
        public static int GetDaysInMonthFromMilliseconds(long milliseconds)
        {
            DateTime dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }
        
        /// <summary>
        /// Lấy ngày trong tuần từ mili giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>Ngày trong tuần (1-7),其中 1 là Chủ nhật và 7 là Thứ bảy.</returns>
        public static int GetDayOfWeekFromMilliseconds(long milliseconds)
        {
            DateTime dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return (int)dateTime.DayOfWeek;
        }
        
        /// <summary>
        /// Lấy ngày trong tháng từ mili giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>Số ngày.</returns>
        public static int GetDayFromMilliseconds(long milliseconds)
        {
            DateTime dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return dateTime.Day;
        }
        
        /// <summary>
        /// Lấy tháng trong năm từ mili giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>Tháng trong năm.</returns>
        public static int GetMonthFromMilliseconds(long milliseconds)
        {
            DateTime dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return dateTime.Month; 
        }
        
        /// <summary>
        /// Lấy số năm từ mili giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <returns>Số năm.</returns>
        public static int GetYearFromMilliseconds(long milliseconds)
        {
            DateTime dateTime = ConvertMillisecondsToDateTime(milliseconds);
            return dateTime.Year;
        }

        /// <summary>
        /// Chuyển đổi giờ từ định dạng 24 giờ sang định dạng 12 giờ.
        /// </summary>
        /// <param name="hour">Giờ trong định dạng 24 giờ.</param>
        /// <returns>(Giờ, Chu kỳ) trong định dạng 12 giờ.</returns>
        public static (int hour, string period) ConvertTo12HourFormat(int hour)
        {
            if (hour < 0 || hour > 23)
                Debug.LogError("Hour must be between 0 and 23 for 24-hour format.");

            var period = hour >= 12 ? "PM" : "AM";
            hour %= 12;
            if (hour == 0)
                hour = 12;

            return (hour, period);
        }

        /// <summary>
        /// Chuyển đổi giờ từ định dạng 12 giờ sang định dạng 24 giờ.
        /// </summary>
        /// <param name="hour">Giờ trong định dạng 12 giờ.</param>
        /// <param name="period">Chu kỳ (AM/PM).</param>
        /// <returns>Giờ trong định dạng 24 giờ.</returns>
        public static int ConvertTo24HourFormat(int hour, string period)
        {
            if (hour < 1 || hour > 12)
                Debug.LogError("Hour must be between 1 and 12 for 12-hour format.");

            if (period != "AM" && period != "PM")
                Debug.LogError("Period must be either 'AM' or 'PM'.");

            switch (period)
            {
                case "PM" when hour != 12:
                    hour += 12;

                    break;
                case "AM" when hour == 12:
                    hour = 0;

                    break;
            }

            return hour;
        }

        /// <summary>
        /// Lấy thời gian địa phương hiện tại.
        /// </summary>
        /// <returns>Thời gian địa phương hiện tại.</returns>
        public static DateTime GetLocalTime()
        {
            var utcTime   = DateTime.UtcNow;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local);

            return localTime;
        }

        /// <summary>
        /// Lấy thời gian UTC hiện tại.
        /// </summary>
        /// <returns>Thời gian UTC hiện tại.</returns>
        public static DateTime GetUtcTime()
        {
            var localTime = DateTime.Now;
            var utcTime   = TimeZoneInfo.ConvertTimeToUtc(localTime);

            return utcTime;
        }

        /// <summary>
        /// Chuyển đổi mili giây sang chuỗi giờ phút giây.
        /// </summary>
        /// <param name="milliseconds">Mili giây.</param>
        /// <param name="is12HourFormat">Định dạng 12 giờ (true) hay 24 giờ (false).</param>
        /// <returns>Chuỗi giờ phút giây.</returns>
        public static string ConvertMillisecondsToTimeString(long milliseconds, bool is12HourFormat)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);

            if (is12HourFormat)
            {
                return timeSpan.ToString(@"hh\:mm\:ss tt"); // 12 giờ (ví dụ: 03:45:00 PM)
            }
            else
            {
                return timeSpan.ToString(@"HH\:mm\:ss"); // 24 giờ (ví dụ: 15:45:00)
            }
        }
        
        /// <summary>
        /// Đổi định dạng ngày và giờ theo yêu cầu.
        /// </summary>
        /// <param name="dateTime">Ngày và giờ cần đổi định dạng.</param>
        /// <param name="format">Định dạng mong muốn (ví dụ: "dd/MM/yyyy HH:mm:ss").</param>
        /// <returns>Ngày và giờ đã đổi định dạng.</returns>
        public static string FormatDateTime(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        /// <summary>
        /// Tính toán thời gian trễ từ thời điểm bắt đầu.
        /// </summary>
        /// <param name="startTime">Thời điểm bắt đầu.</param>
        /// <returns>Thời gian trễ.</returns>
        public static TimeSpan CalculateElapsedTime(DateTime startTime)
        {
            return CalculateTimeDifference(startTime, DateTime.Now);
        }
        
        /// <summary>
        /// Tính toán thời gian giữa hai thời điểm.
        /// </summary>
        /// <param name="startTime">Thời điểm bắt đầu.</param>
        /// <param name="endTime">Thời điểm kết thúc.</param>
        /// <returns>Thời gian giữa hai thời điểm.</returns>
        public static TimeSpan CalculateTimeDifference(DateTime startTime, DateTime endTime)
        {
            return endTime - startTime;
        }

        /// <summary>
        /// Kiểm tra xem thời gian hiện tại có nằm trong khoảng thời gian cụ thể.
        /// </summary>
        /// <param name="startTime">Thời điểm bắt đầu.</param>
        /// <param name="endTime">Thời điểm kết thúc.</param>
        /// <returns>True nếu thời gian hiện tại nằm trong khoảng, false nếu không.</returns>
        public static bool IsWithinTimeRange(DateTime startTime, DateTime endTime)
        {
            return DateTime.Now >= startTime && DateTime.Now <= endTime;
        }
        
        /// <summary>
        /// Kiểm tra xem thời gian hiện tại có nằm trong khoảng thời gian cụ thể.
        /// </summary>
        /// <param name="startTime">Thời điểm bắt đầu.</param>
        /// <param name="endTime">Thời điểm kết thúc.</param>
        /// <param name="timeSet">Thời điểm so sánh.</param>
        /// <returns>True nếu thời gian hiện tại nằm trong khoảng, false nếu không.</returns>
        public static bool IsWithinTimeRange(DateTime timeSet,DateTime startTime, DateTime endTime)
        {
            return timeSet >= startTime && timeSet <= endTime;
        }

        /// <summary>
        /// Tính toán thời gian còn lại đến thời điểm cụ thể.
        /// </summary>
        /// <param name="deadline">Thời điểm cần tính toán.</param>
        /// <returns>Thời gian còn lại.</returns>
        public static TimeSpan CalculateRemainingTime(DateTime deadline)
        {
            return deadline - DateTime.Now;
        }
    }
}