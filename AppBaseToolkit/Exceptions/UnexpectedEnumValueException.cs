using System;
using JetBrains.Annotations;

namespace AppBaseToolkit.Exceptions;

/// <summary>
/// Exception to handle default value of <see langword="switch"/> statement
/// </summary>
[PublicAPI]
public class UnexpectedEnumValueException : NotSupportedException
{
    /// <summary>
    /// Initializes a new instance of <see cref="UnexpectedEnumValueException"/>
    /// </summary>
    /// <param name="enumValue"></param>
    public UnexpectedEnumValueException(Enum enumValue) 
        : base($"Value '{enumValue}' of enumeration '{nameof(Enum)}' is not supported")
    {
    }
}