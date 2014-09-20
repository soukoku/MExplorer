using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    /// <summary>
    /// Indicates the UI view type. These map to the corresponding <see cref="IconSize"/>.
    /// </summary>
    public enum ViewTypes
    {
        /// <summary>
        /// Grid view.
        /// </summary>
        Details,
        /// <summary>
        /// The medium thumb view.
        /// </summary>
        MediumThumb,
        /// <summary>
        /// The large thumb view.
        /// </summary>
        LargeThumb,
        /// <summary>
        /// The extra large thumbs view.
        /// </summary>
        ExtraLargeThumb,
    }
}
