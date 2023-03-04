using System.Collections.Generic;
using System.Windows.Media;
using JetBrains.Annotations;

namespace AppBaseToolkit.Helpers
{
    /// <summary>
    /// Cache for re-using brushes
    /// </summary>
    [PublicAPI]
    public static class BrushesCache
    {
        private static readonly object Lock = new();
        private static readonly Dictionary<Color, Brush> Cache = new();

        /// <summary>
        /// Returns new or existing frozen brush
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Brush GetBrush(Color color)
        {
            lock (Lock)
            {
                if (Cache.TryGetValue(color, out var brush))
                    return brush;

                brush = new SolidColorBrush(color);
                if (brush.CanFreeze)
                    brush.Freeze();

                Cache.Add(color, brush);
                return brush;
            }
        }
    }
}