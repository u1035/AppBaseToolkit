using System;
using System.Windows.Input;

namespace AppBaseToolkit.Helpers
{
    /// <summary>
    /// Shows wait cursor, can be used like "using (new WaitCursor()) { }"
    /// https://stackoverflow.com/a/3481274
    /// </summary>
    public sealed class WaitCursor : IDisposable
    {
        private readonly Cursor _previousCursor;

        /// <summary>
        /// Initializes a new instance of <see cref="WaitCursor"/>
        /// </summary>
        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }
}