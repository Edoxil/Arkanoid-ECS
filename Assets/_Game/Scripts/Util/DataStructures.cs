using UnityEngine;

namespace AppCore
{
    [System.Serializable]
    public struct FloatRange
    {
        public float Min;
        public float Max;

        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandom()
        {
            return UnityEngine.Random.Range(Min, Max);
        }

        public static float Random(float min, float max)
        {
            return new FloatRange(min, max).GetRandom();
        }

        private bool IsEqual(float a, float b, float tolerance = 0.000001f)
        {
            return Mathf.Abs(a - b) < tolerance;
        }

        private bool IsGreater(float a, float b)
        {
            return a > b;
        }

        private bool IsLess(float a, float b)
        {
            return a < b;
        }

        /// <summary>
        /// Return true if value in range. Min and max inclusive
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(float value)
        {
            return IsLess(value, Max) || IsEqual(value, Max) && IsGreater(value, Min) || IsEqual(value, Min);
        }

        public override string ToString()
        {
            return $"Min: {Min} Max: {Max}";
        }

    }
    [System.Serializable]
    public struct IntRange
    {
        public int Min;
        public int Max;

        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Max inclusive
        /// </summary>
        /// <returns></returns>
        public int GetRandom()
        {
            return UnityEngine.Random.Range(Min, Max + 1);
        }
        /// <summary>
        /// Return true if value in range. Min and max inclusive
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(int value)
        {
            return value <= Max && value >= Min;
        }

        public static int Random(int min, int max)
        {
            return new IntRange(min, max).GetRandom();
        }
    }

}