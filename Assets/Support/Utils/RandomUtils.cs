using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UltimateHelper
{
    /// <summary>
    /// Lớp này quản lý các hàm số random chung
    /// </summary>
    public static class RandomUtils
    {
        //================================================================================
        // Thuộc tính (static)
        //================================================================================
        /// <summary>
        /// Trả về một điểm random bên trong hình tròn đơn vị
        /// </summary>
        public static Vector2 InsideUnitCircle => Random.insideUnitCircle;

        /// <summary>
        /// Trả về một điểm random bên trong hình cầu đơn vị
        /// </summary>
        public static Vector3 InsideUnitSphere => Random.insideUnitSphere;

        /// <summary>
        /// Trả về một điểm random trên bề mặt hình cầu đơn vị
        /// </summary>
        public static Vector3 OnUnitSphere => Random.onUnitSphere;

        /// <summary>
        /// Trả về một giá trị Quaternion random
        /// </summary>
        public static Quaternion Rotation => Random.rotation;

        /// <summary>
        /// Trả về một giá trị Quaternion random đều
        /// </summary>
        public static Quaternion RotationUniform => Random.rotationUniform;

        /// <summary>
        /// Trả về một số random từ 0.0 đến 1.0
        /// </summary>
        public static float Between0And1 => Random.value;

        /// <summary>
        /// Trả về một số random từ 0.0 đến 360.0
        /// </summary>
        public static float Between0And360 => Between0And1 * 360;

        /// <summary>
        /// Trả về giá trị random False hoặc True
        /// </summary>
        public static bool FalseOrTrue => Random.Range(0, 2) == 0;

        /// <summary>
        /// Trả về giá trị random 0 hoặc 1
        /// </summary>
        public static byte Value0Or1 => Convert.ToByte(FalseOrTrue);

        //================================================================================
        // Phương thức (static)
        //================================================================================
        /// <summary>
        /// Thiết lập giá trị hạt giống và khởi tạo trạng thái tạo số random
        /// </summary>
        public static void InitState(int seed)
        {
            Random.InitState(seed);
        }

        /// <summary>
        /// Trả về một phần tử random từ mảng đã cho
        /// </summary>
        public static T RandomAt<T>(params T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }

        /// <summary>
        /// Trả về một phần tử random từ danh sách đã cho
        /// </summary>
        public static T RandomAt<T>(IList<T> values)
        {
            return values[Random.Range(0, values.Count)];
        }

        /// <summary>
        /// Trả về một phần tử random từ danh sách chỉ đọc đã cho
        /// </summary>
        public static T RandomAt<T>(IReadOnlyList<T> values)
        {
            return values[Random.Range(0, values.Count)];
        }

        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static int Range(int max)
        {
            return Random.Range(0, max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max
        /// </summary>
        public static Vector2 Range(Vector2 min, Vector2 max)
        {
            return new(Range(min.x, max.x), Range(min.y, max.y));
        }
        
        /// <summary>
        /// Trả về một số random từ min đến max
        /// </summary>
        public static Vector3 Range(Vector3 min, Vector3 max)
        {
            return new(Range(min.x, max.x), Range(min.y, max.y),Range(min.z, max.z));
        }
        
        /// <summary>
        /// Trả về giá trị bool random
        /// </summary>
        public static bool RangeBool()
        {
            return Range(11) % 2 == 0;
        }
        
        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static byte RangeByte(byte max)
        {
            return (byte)Random.Range(0, max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static byte RangeByte(byte min, byte max)
        {
            return (byte)Random.Range(min, max);
        }

        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static short RangeShort(short max)
        {
            return (short)Random.Range(0, max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static short RangeShort(short min, short max)
        {
            return (short)Random.Range(min, max);
        }

                /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static ushort RangeUshort(ushort max)
        {
            return (ushort)Random.Range(0, max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static ushort RangeUshort(ushort min, ushort max)
        {
            return (ushort)Random.Range(min, max);
        }

        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static uint RangeUint(uint max)
        {
            return (uint)Random.Range(0, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static uint RangeUint(uint min, uint max)
        {
            return (uint)Random.Range((int)min, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static long RangeLong(long max)
        {
            return Random.Range(0, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static long RangeLong(long min, long max)
        {
            return Random.Range((int)min, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ 0 đến max - 1
        /// </summary>
        public static ulong RangeUlong(ulong max)
        {
            return (ulong)Random.Range(0, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max - 1
        /// </summary>
        public static ulong RangeUlong(ulong min, ulong max)
        {
            return (ulong)Random.Range((int)min, (int)max);
        }

        /// <summary>
        /// Trả về một số random từ min đến max
        /// </summary>
        public static float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}