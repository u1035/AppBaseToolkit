using System;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace AppBaseToolkit.Extensions;

/// <summary>
/// Extension methods for <see cref="Dispatcher"/>
/// </summary>
[PublicAPI]
public static class DispatcherExtensions
{
    /// <summary>
    /// Executes <paramref name="action"/> in UI thread asynchronously, if it's not already in UI thread
    /// </summary>
    /// <param name="dispatcher"></param>
    /// <param name="action"></param>
    /// <param name="priority"></param>
    public static void WrapIfNeededAsync(this Dispatcher dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        if (!dispatcher.CheckAccess())
            dispatcher.InvokeAsync(action, priority);
        else
            action();
    }

    /// <summary>
    /// Executes <paramref name="action"/> in UI thread synchronously, if it's not already in UI thread
    /// </summary>
    /// <param name="dispatcher"></param>
    /// <param name="action"></param>
    /// <param name="priority"></param>
    public static void WrapIfNeeded(this Dispatcher dispatcher, Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        if (!dispatcher.CheckAccess())
            dispatcher.Invoke(action, priority);
        else
            action();
    }
}