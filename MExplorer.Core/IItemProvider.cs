using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    /// <summary>
    /// Contextual interface for providing items.
    /// </summary>
    public interface IItemProvider
    {
        /// <summary>
        /// Gets the root container.
        /// </summary>
        /// <value>
        /// The root.
        /// </value>
        ContainerVM Root { get; }

        /// <summary>
        /// Gets the sub items under the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        IEnumerable<ItemVM> GetSingleChildren(ContainerVM container);

        /// <summary>
        /// Gets the sub containers under the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        IEnumerable<ContainerVM> GetContainerChildren(ContainerVM container);
    }
}
