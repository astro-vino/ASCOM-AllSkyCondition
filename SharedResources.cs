using ASCOM.Utilities;

namespace ASCOM.AllSkyCondition
{
    /// <summary>
    /// Provides shared resources for the driver, such as the TraceLogger.
    /// </summary>
    internal static class SharedResources
    {
        private static TraceLogger _tl;

        /// <summary>
        /// Gets the trace logger instance.
        /// </summary>
        internal static TraceLogger tl
        {
            get
            {
                if (_tl == null)
                {
                    _tl = new TraceLogger("AllSkyCondition", "AllSkyCondition");
                    _tl.Enabled = false; // Default to disabled
                }
                return _tl;
            }
        }
    }
}
