using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    public interface IItemProvider
    {
        ContainerVM Root { get; }

        IEnumerable<ItemVM> GetSingleChildren(ContainerVM container);
        IEnumerable<ContainerVM> GetContainerChildren(ContainerVM container);
    }
}
