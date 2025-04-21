using UnityEngine;

namespace UltimateHelper
{
    public static class AnimatorExt
    {
        /// <summary>
        /// Chuyển đổi mượt mà sang trạng thái animation mới trong Animator bằng CrossFade.
        /// </summary>
        /// <param name="animator">Animator cần thực hiện chuyển đổi.</param>
        /// <param name="stateName">Tên của trạng thái animation.</param>
        /// <param name="transitionDuration">Thời gian chuyển đổi (theo giây).</param>
        /// <param name="layer">Lớp (layer) của Animator, mặc định là 0.</param>
        /// <param name="normalizedTime">Thời gian bắt đầu của animation, mặc định là -1 (giữ nguyên).</param>
        public static void PlaySmooth(this Animator animator,  string stateName, float transitionDuration = 0.25f,
                                      int           layer = 0, float  normalizedTime = -1f)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return;
            }
            animator.CrossFade(stateName, transitionDuration, layer); 
        }
        
        /// <summary>
        /// Chuyển đổi mượt mà sang trạng thái animation mới trong Animator bằng CrossFade.
        /// </summary>
        /// <param name="animator">Animator cần thực hiện chuyển đổi.</param>
        /// <param name="stateHash">Hash của trạng thái animation.</param>
        /// <param name="transitionDuration">Thời gian chuyển đổi (theo giây).</param>
        /// <param name="layer">Lớp (layer) của Animator, mặc định là 0.</param>
        /// <param name="normalizedTime">Thời gian bắt đầu của animation, mặc định là -1 (giữ nguyên).</param>
        public static void PlaySmooth(this Animator animator,  int   stateHash, float transitionDuration = 0.25f,
                                      int           layer = 0, float normalizedTime = -1f)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return;
            }
            animator.CrossFade(stateHash, transitionDuration, layer); 
        }
        
        /// <summary>
        /// Chuyển mượt mà đến animation mới và đảm bảo các sự kiện trong animation mới và cũ trong quá trình chuyển vẫn được gọi
        /// </summary>
        /// <param name="animator">Animator để điều khiển</param>
        /// <param name="stateName">Tên của trạng thái animation muốn chuyển đến</param>
        /// <param name="transitionDuration">Thời gian chuyển đổi (mượt mà) sang animation mới (thời gian blend)</param>
        /// <param name="layer">Lớp animation nếu cần, mặc định là 0</param>
        public static void PlaySmoothWithImmediateEvents(this Animator animator, string stateName, float transitionDuration = 0.25f, int layer = 0)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");
                return;
            }

            // Dùng Play để đảm bảo animation bắt đầu ngay lập tức
            animator.Play(stateName, layer, 0f); // Bắt đầu từ đầu của animation

            // Dùng CrossFade để có hiệu ứng chuyển mượt mà giữa các animation
            animator.CrossFade(stateName, transitionDuration, layer);
        }
        
        /// <summary>
        /// Chuyển mượt mà đến animation mới và đảm bảo các sự kiện trong animation mới và cũ trong quá trình chuyển vẫn được gọi
        /// </summary>
        /// <param name="animator">Animator để điều khiển</param>
        /// <param name="stateHash">Hash của trạng thái animation.</param>
        /// <param name="transitionDuration">Thời gian chuyển đổi (mượt mà) sang animation mới (thời gian blend)</param>
        /// <param name="layer">Lớp animation nếu cần, mặc định là 0</param>
        public static void PlaySmoothWithImmediateEvents(this Animator animator, int stateHash, float transitionDuration = 0.25f, int layer = 0)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");
                return;
            }

            // Dùng Play để đảm bảo animation bắt đầu ngay lập tức
            animator.Play(stateHash, layer, 0f); // Bắt đầu từ đầu của animation

            // Dùng CrossFade để có hiệu ứng chuyển mượt mà giữa các animation
            animator.CrossFade(stateHash, transitionDuration, layer);
        }
        

        /// <summary>
        /// Dừng animation hiện tại bằng cách đặt tốc độ Animator về 0.
        /// </summary>
        /// <param name="animator">Animator cần dừng.</param>
        public static void Pause(this Animator animator)
        {
            SetSpeed(animator, 0f);
        }

        /// <summary>
        /// Tiếp tục phát animation bằng cách khôi phục tốc độ Animator về 1.
        /// </summary>
        /// <param name="animator">Animator cần tiếp tục.</param>
        public static void Resume(this Animator animator)
        {
            SetSpeed(animator, 1f);
        }

        /// <summary>
        /// Tiếp tục phát animation bằng cách khôi phục tốc độ Animator về 1.
        /// </summary>
        /// <param name="animator">Animator cần tiếp tục.</param>
        public static void SetSpeed(this Animator animator, float speed)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return;
            }

            animator.speed = speed;
        }

        /// <summary>
        /// Thiết lập giá trị Trigger và tự động reset sau khi thiết lập.
        /// </summary>
        /// <param name="animator">Animator cần thiết lập Trigger.</param>
        /// <param name="triggerName">Tên của Trigger.</param>
        public static void SetTriggerOneShot(this Animator animator, string triggerName)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return;
            }

            animator.SetTrigger(triggerName);
            animator.ResetTrigger(triggerName);
        }

        /// <summary>
        /// Đặt giá trị cho một parameter trong Animator, tự động phát hiện kiểu parameter.
        /// </summary>
        /// <param name="animator">Animator cần thiết lập parameter.</param>
        /// <param name="parameterName">Tên của parameter.</param>
        /// <param name="value">Giá trị cần đặt (int, float, bool).</param>
        public static void SetParameter(this Animator animator, string parameterName, object value)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return;
            }

            if (value is int intValue)
            {
                animator.SetInteger(parameterName, intValue);
            }
            else if (value is float floatValue)
            {
                animator.SetFloat(parameterName, floatValue);
            }
            else if (value is bool boolValue)
            {
                animator.SetBool(parameterName, boolValue);
            }
            else
            {
                Debug.LogError($"Kiểu giá trị {value.GetType()} không được hỗ trợ.");
            }
        }

        /// <summary>
        /// Lấy hash của animation đang chạy trên một layer cụ thể.
        /// </summary>
        /// <param name="animator">Animator cần kiểm tra.</param>
        /// <param name="layer">Lớp (layer) của Animator, mặc định là 0.</param>
        /// <returns>Trả về hash của animation đang chạy.</returns>
        public static int GetCurrentAnimationHash(this Animator animator, int layer = 0)
        {
            if (animator == null)
            {
                Debug.LogError("Animator không được gắn!");

                return 0;
            }

            var currentState = animator.GetCurrentAnimatorStateInfo(layer);

            return currentState.shortNameHash;
        }

        /// <summary>
        /// Kiểm tra xem animation có đang chạy không dựa trên tên.
        /// </summary>
        /// <param name="animator">Animator cần kiểm tra.</param>
        /// <param name="stateName">Tên của trạng thái animation.</param>
        /// <param name="layer">Lớp (layer) của Animator, mặc định là 0.</param>
        /// <returns>Trả về true nếu animation đang chạy, ngược lại trả về false.</returns>
        public static bool IsPlaying(this Animator animator, string stateName, int layer = 0)
        {
            if (animator == null)
                return false;

            var currentState = animator.GetCurrentAnimatorStateInfo(layer);

            return currentState.IsName(stateName);
        }

        /// <summary>
        /// Kiểm tra xem animation có đang chạy không dựa trên hash.
        /// </summary>
        /// <param name="animator">Animator cần kiểm tra.</param>
        /// <param name="stateHash">Hash của trạng thái animation.</param>
        /// <param name="layer">Lớp (layer) của Animator, mặc định là 0.</param>
        /// <returns>Trả về true nếu animation đang chạy, ngược lại trả về false.</returns>
        public static bool IsPlaying(this Animator animator, int stateHash, int layer = 0)
        {
            if (animator == null)
                return false;

            var currentState = animator.GetCurrentAnimatorStateInfo(layer);

            return currentState.shortNameHash == stateHash;
        }
    }
}