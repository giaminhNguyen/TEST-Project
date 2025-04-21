using UnityEngine;

namespace UltimateHelper
{
    public class MathUtils
    {
        public static float ClampAngle(float angle, float min, float max)
        {
            angle %= 360;
            return Mathf.Clamp(angle, min, max);
        }

        public static float ClampAngle(float angle)
        {
            return ClampAngle(angle, -360f, 360f);
        }

        public static float ClampAngle(float angle, float max)
        {
            return ClampAngle(angle, -max, max);
        }

        public static float ClampAngle(float angle, float min, float max, bool is360)
        {
            if (is360)
            {
                return ClampAngle(angle, min, max);
            }

            return Mathf.Clamp(angle, min, max);
        }

        public static float ClampAngle(float angle, bool is360)
        {
            return ClampAngle(angle, -360f, 360f, is360);
        }

        public static float ClampAngle(float angle, float max, bool is360)
        {
            return ClampAngle(angle, -max, max, is360);
        }
        
        public static float Max(params float[] values)
        {
            var max = values[0];
            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }

            return max;
        }
        
        public static float Min(params float[] values)
        {
            var min = values[0];
            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }

            return min;
        }
        
        public static int Max(params int[] values)
        {
            var max = values[0];
            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }

            return max;
        }
        
        public static int Min(params int[] values)
        {
            var min = values[0];
            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }

            return min;
        }
        
        public static float Round(float value, int digits)
        {
            var mult = Mathf.Pow(10.0f, digits);
            return Mathf.Round(value * mult) / mult;
        }
    }
}