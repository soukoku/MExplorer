using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer
{
    public class MetaData : List<MetaEntry>
    {
        public MetaEntry this[string name]
        {
            get
            {
                return this.FirstOrDefault(m => string.Equals(name, m.Name));
            }
        }
    }
}
