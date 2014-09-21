using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    /// <summary>
    /// Indicates the UI view type. These map to the corresponding <see cref="IconSize"/>.
    /// </summary>
    [Flags]
    public enum ViewTypes
    {
        /// <summary>
        /// Grid view.
        /// </summary>
        Details = 1,
        /// <summary>
        /// The medium thumb view.
        /// </summary>
        MediumThumb = 2,
        /// <summary>
        /// The large thumb view.
        /// </summary>
        LargeThumb = 4,
        /// <summary>
        /// The extra large thumbs view.
        /// </summary>
        ExtraLargeThumb = 8,
    }
}
