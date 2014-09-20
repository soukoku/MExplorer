using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace MExplorer.Registry
{
    class RegistryItemProvider : IItemProvider
    {
        #region IItemProvider Members

        public ContainerVM Root
        {
            get
            {
                return new ComputerVM(this);
            }
        }

        public IEnumerable<ItemVM> GetSingleChildren(ContainerVM container)
        {
            var list = new List<ItemVM>();

            var vm = container as KeyVM;
            if (vm != null)
            {
                var key = vm.Key;
                foreach (var name in key.GetValueNames())
                {
                    list.Add(new ValueVM(vm.Provider, vm, name, key.GetValueKind(name), key.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames)));
                }
            }

            return list;
        }

        public IEnumerable<ContainerVM> GetContainerChildren(ContainerVM container)
        {
            var list = new List<ContainerVM>();

            var k = container as KeyVM;
            if (k != null)
            {
                foreach (var name in k.Key.GetSubKeyNames())
                {
                    try
                    {
                        list.Add(new KeyVM(k.Provider, k, k.Key.OpenSubKey(name, RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey)));
                    }
                    catch (SecurityException) { }
                    //list.Add(new KeyVM(k.Provider, k, k.Key.OpenSubKey(name, RegistryKeyPermissionCheck.Default, System.Security.AccessControl.RegistryRights.ReadKey)));
                }
            }
            else
            {
                var root = container as ComputerVM;
                if (root != null)
                {
                    var key = root.IsRemote ? RegistryKey.OpenRemoteBaseKey(RegistryHive.ClassesRoot, root.Name) :
                            RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default);
                    list.Add(new KeyVM(root.Provider, root, key));

                    key = root.IsRemote ? RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, root.Name) :
                            RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
                    list.Add(new KeyVM(root.Provider, root, key));

                    key = root.IsRemote ? RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, root.Name) :
                            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                    list.Add(new KeyVM(root.Provider, root, key));

                    key = root.IsRemote ? RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, root.Name) :
                            RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default);
                    list.Add(new KeyVM(root.Provider, root, key));

                    key = root.IsRemote ? RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentConfig, root.Name) :
                            RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Default);
                    list.Add(new KeyVM(root.Provider, root, key));

                }
            }

            return list;
        }

        #endregion
    }
}
