using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MExplorer
{
    class ProviderLoader
    {
        public static List<IItemProvider> FindProviders()
        {
            List<IItemProvider> list = new List<IItemProvider>();

            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var file in Directory.GetFiles(folder, "*.dll"))
            {
                list.AddRange(TryLoadAssembly(file));
            }

            return list;
        }

        private static IEnumerable<IItemProvider> TryLoadAssembly(string file)
        {
            try
            {
                var ass = Assembly.LoadFile(file);
                return ass.GetTypes().Where(t =>
                        typeof(IItemProvider).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                    .Select(t => 
                        Activator.CreateInstance(t)) // todo: could use IoC
                    .Cast<IItemProvider>();
            }
            catch { }
            return Enumerable.Empty<IItemProvider>();
        }
    }
}
