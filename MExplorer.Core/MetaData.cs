using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    /// <summary>
    /// Represents a collection of <see cref="MetaEntry"/>.
    /// </summary>
    public class MetaData : List<MetaEntry>
    {
        /// <summary>
        /// Gets the <see cref="MetaEntry"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="MetaEntry"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public MetaEntry this[string name]
        {
            get
            {
                return this.FirstOrDefault(m => string.Equals(name, m.Name));
            }
        }
    }
}
