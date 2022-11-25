using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AppBaseToolkit.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    [PublicAPI]
    public static class StringExtensions
    {
        /// <summary>
        /// string is null or empty
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ContractAnnotation("null => true")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? data)
        {
            return string.IsNullOrEmpty(data!);
        }

        /// <summary>
        /// string is not null and not empty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [ContractAnnotation("null => false")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? value) =>
            !string.IsNullOrEmpty(value!);

        /// <summary>
        /// Split string by any of wide used delimiters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] SplitByAnyDelimiter(this string value) 
            => value.Split(new[] { ' ', ',', ';', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
