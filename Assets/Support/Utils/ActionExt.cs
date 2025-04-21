using System;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace UltimateHelper
{
    public static class ActionExt
    {
        
        /// <summary>
        /// Thực thi hành động một cách an toàn, tránh null reference exception.
        /// </summary>
        /// <param name="action">Hành động cần thực thi.</param>
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }
        
        /// <summary>
        /// Thực thi hành động một cách an toàn, tránh null reference exception.
        /// </summary>
        /// <param name="action">Hành động cần thực thi.</param>
        public static void SafeInvoke(this UnityEvent action)
        {
            action?.Invoke();
        }
        
        
        
        /// <summary>
        /// Thêm một delegate mới vào đầu danh sách delegate hiện tại.
        /// </summary>
        /// <typeparam name="TDelegate">Loại delegate.</typeparam>
        /// <param name="original">Delegate gốc.</param>
        /// <param name="newFirst">Delegate mới cần thêm vào đầu.</param>
        /// <returns>Delegate kết hợp.</returns>
        public static TDelegate AddFirst<TDelegate>(this TDelegate original, TDelegate newFirst)
                where TDelegate : Delegate
        {
            if (newFirst == null)
                throw new ArgumentNullException(nameof(newFirst));

            return (TDelegate)Delegate.Combine(original, newFirst);
        }
        
        
        #region DelayInvoke

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke(this Action action, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke();
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1>(this Action<T1> action, T1 param, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1    param1, T2 param2,
                                                         T3                      param3, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 param1, T2 param2,
                                                             T3                          param3, T4 param4, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 param1,
                                                                 T2 param2, T3 param3, T4 param4, T5 param5,
                                                                 float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action,
                                                                     T1 param1, T2 param2, T3 param3, T4 param4,
                                                                     T5 param5, T6 param6, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7>(
                this Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5,
                T6                                      param6, T7 param7, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4    param4,
                T5                                          param5, T6 param6, T7 param7, T8 param8, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4,
                T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T1 param1, T2 param2, T3 param3, T4 param4,
                T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T1 param1, T2 param2, T3 param3,
                T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T1 param1, T2 param2, T3 param3,
                T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12,
                float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                    param12);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T1 param1, T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                    param12, param13);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T1 param1, T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, T14 param14, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                    param12, param13, param14);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T1 param1,
                T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10,
                T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                    param12, param13, param14, param15);
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định sau 1 khoảng thời gian quy định.
        /// </summary>
        public static async void DelayInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, T1 param1,
                T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10,
                T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16, float delay)
        {
            if (action == null)
                return;
            await Task.Delay((int)(delay * 1000));
            action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                    param12, param13, param14, param15, param16);
        }

        #endregion
        
        #region RepeatInvoke

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        /// <param name="action">Hành động cần được gọi.</param>
        /// <param name="times">Số lần lặp lại việc gọi hành động.</param>
        /// <param name="delayTime">Thời gian trễ giữa mỗi lần gọi hành động, tính bằng giây.</param>
        /// <param name="invokeFirst">Nếu là true, hành động sẽ được gọi ngay lập tức trước khi bắt đầu vòng lặp trễ.</param>
        public static async void RepeatInvoke(this Action action, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke();
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke();
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1>(this Action<T1> action, T1 param1, int times, float delayTime,
                                                  bool            invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1);
            }
        }


        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2>(this Action<T1, T2> action,    T1   param1, T2 param2, int times,
                                                      float               delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1  param1, T2    param2,
                                                          T3                      param3, int times,  float delayTime,
                                                          bool                    invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3);
            }
        }


        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 param1, T2 param2,
                                                              T3 param3, T4 param4, int times, float delayTime,
                                                              bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 param1,
                                                                  T2 param2, T3 param3, T4 param4, T5 param5, int times,
                                                                  float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action,
                                                                      T1 param1, T2 param2, T3 param3, T4 param4,
                                                                      T5 param5, T6 param6, int times, float delayTime,
                                                                      bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7>(
                this Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5,
                T6 param6, T7 param7, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4 param4,
                T5 param5, T6 param6, T7 param7, T8 param8, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4,
                T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, int times, float delayTime,
                bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, T1 param1, T2 param2, T3 param3, T4 param4,
                T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, int times, float delayTime,
                bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, T1 param1, T2 param2, T3 param3,
                T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, int times,
                float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, T1 param1, T2 param2, T3 param3,
                T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12,
                int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, T1 param1, T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, T1 param1, T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, T14 param14, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, T1 param1,
                T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, T14 param14, T15 param15, int times, float delayTime, bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14, param15);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14, param15);
            }
        }

        /// <summary>
        /// Lặp lại việc gọi hành động được chỉ định một số lần nhất định với khoảng thời gian trễ giữa mỗi lần gọi.
        /// </summary>
        public static async void RepeatInvoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, T1 param1,
                T2 param2,
                T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11,
                T12 param12, T13 param13, T14 param14, T15 param15, T16 param16, int times, float delayTime,
                bool invokeFirst = true)
        {
            if (action == null)
                return;

            if (invokeFirst)
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14, param15, param16);
            }

            for (var i = 0; i < times; i++)
            {
                await Task.Delay((int)(delayTime * 1000));
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11,
                        param12, param13, param14, param15, param16);
            }
        }

        #endregion
    }
}