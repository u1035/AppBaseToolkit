using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AppBaseToolkit.Extensions;

[PublicAPI]
public static class ObjectExtensions
{
    /// <summary>
    /// Returns provided value, checking that it's not null.
    /// If the object is null - stops debugger.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [ContractAnnotation("null=>halt")]
    [AssertionMethod]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object EnsureNotNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [System.Diagnostics.CodeAnalysis.NotNull]
        this object? obj)
    {
#if DEBUG
        Debug.Assert(obj != null, $"{obj} is null");
#endif
        return obj;
    }
}