using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AppBaseToolkit.Extensions
{
    /// <summary>
    /// Contains helper functions to simplify safe comparison of floating-point numbers
    /// </summary>
    [PublicAPI]
    public static class DoubleExtensions
    {
        /// <summary>
        /// Wrapper for safe comparison of value and zero
        /// </summary>
        /// <param name="value">Value to compare</param>
        /// <param name="precision">Optional precision parameter</param>
        /// <returns>True, if value is zero, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this double value, double precision = 0.000001)
        {
            return Math.Abs(value) < precision;
        }


        /// <summary>
        /// Wrapper for safe comparison of two double values
        /// </summary>
        /// <param name="firstValue">Value to compare</param>
        /// <param name="value">Value to compare with</param>
        /// <param name="precision">Optional precision parameter</param>
        /// <returns>True, if values are equal, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualTo(this double firstValue, double value, double precision = 0.000001)
        {
            return Math.Abs(firstValue - value) < precision;
        }

        /// <summary>
        /// Compute decimal digits count for given value
        /// </summary>
        /// <param name="value">Value to compute decimal digits</param>
        /// <returns></returns>
        public static int ComputePrecision(double value)
        {
            var precision = 0;
            for (int i = 0; i < 10; i++)
            {
                var v = Math.Pow(10, -i);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (value == Math.Round(Math.Round(value / v) * v, i))
                {
                    precision = i;
                    break;
                }
            }

            return precision;
        }
    }
}
