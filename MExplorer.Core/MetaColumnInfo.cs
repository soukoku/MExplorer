using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MExplorer
{
    /// <summary>
    /// Defines the display column info for a meta data.
    /// </summary>
    public class MetaColumnInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetaColumnInfo"/> class.
        /// </summary>
        public MetaColumnInfo()
        {
            Width = 100;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the header alignment.
        /// </summary>
        /// <value>
        /// The header alignment.
        /// </value>
        public HorizontalAlignment HeaderAlignment { get; set; }

        /// <summary>
        /// Gets or sets the content alignment.
        /// </summary>
        /// <value>
        /// The content alignment.
        /// </value>
        public TextAlignment ContentAlignment { get; set; }

        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        /// <value>
        /// The formatter.
        /// </value>
        public IValueConverter Formatter { get; set; }

        /// <summary>
        /// Gets or sets the format parameter.
        /// </summary>
        /// <value>
        /// The format parameter.
        /// </value>
        public object FormatParameter { get; set; }
    }
}
