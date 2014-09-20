using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.Registry
{
    class ValueVM : ItemVM
    {
        public ValueVM(IItemProvider provider, ContainerVM parent, string name) :
            base(provider, parent, name, false)
        {

        }
    }
}
