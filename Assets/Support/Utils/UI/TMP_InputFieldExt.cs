using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UltimateHelper
{
    /// <summary>
    /// Các phương thức mở rộng cho loại TMP_InputField
    /// </summary>
    public static partial class UIExtensionMethods
    {
        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện nộp của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện nộp của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện nộp của TMP_InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this TMP_InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện nộp của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_InputField.SubmitEvent self, UnityAction<string> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của TMP_InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this TMP_InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện thay đổi của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_InputField.OnChangeEvent self, UnityAction<string> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_InputField.SelectionEvent self, UnityAction<string> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_InputField.SelectionEvent self, UnityAction<string> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn của TMP_InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this TMP_InputField.SelectionEvent self, UnityAction<string> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_InputField.SelectionEvent self, UnityAction<string> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn văn bản của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_InputField.TextSelectionEvent self, UnityAction<string, int, int> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn văn bản của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_InputField.TextSelectionEvent self, UnityAction<string, int, int> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn văn bản của TMP_InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this TMP_InputField.TextSelectionEvent self, UnityAction<string, int, int> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện chọn văn bản của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_InputField.TextSelectionEvent self, UnityAction<string, int, int> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện bàn phím cảm ứng của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void SetListener(this TMP_InputField.TouchScreenKeyboardEvent self, UnityAction<TouchScreenKeyboard.Status> call)
        {
            self.RemoveAllListeners();
            self.AddListener(call);
        }

        /// <summary>
        /// Thêm listener.
        /// </summary>
        /// <param name="self">Sự kiện bàn phím cảm ứng của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Add(this TMP_InputField.TouchScreenKeyboardEvent self, UnityAction<TouchScreenKeyboard.Status> call)
        {
            self.AddListener(call);
        }

        /// <summary>
        /// Gỡ bỏ listener.
        /// </summary>
        /// <param name="self">Sự kiện bàn phím cảm ứng của TMP_InputField.</param>
        /// <param name="call">Hành động cần gỡ bỏ.</param>
        public static void Remove(this TMP_InputField.TouchScreenKeyboardEvent self, UnityAction<TouchScreenKeyboard.Status> call)
        {
            self.RemoveListener(call);
        }

        /// <summary>
        /// Thiết lập listener.
        /// </summary>
        /// <param name="self">Sự kiện bàn phím cảm ứng của TMP_InputField.</param>
        /// <param name="call">Hành động cần thực thi khi sự kiện xảy ra.</param>
        public static void Set(this TMP_InputField.TouchScreenKeyboardEvent self, UnityAction<TouchScreenKeyboard.Status> call)
        {
            self.SetListener(call);
        }

        /// <summary>
        /// Thiết lập xem có nên che giấu chuỗi ký tự nhập vào hay không.
        /// </summary>
        /// <param name="self">TMP_InputField cần thiết lập.</param>
        /// <param name="isMask">Giá trị boolean xác định có nên che giấu chuỗi ký tự hay không.</param>
        public static void SetIsMask(this TMP_InputField self, bool isMask)
        {
            self.contentType = isMask
                ? TMP_InputField.ContentType.Password
                : TMP_InputField.ContentType.Standard;

            self.ForceLabelUpdate();
        }
    }
}