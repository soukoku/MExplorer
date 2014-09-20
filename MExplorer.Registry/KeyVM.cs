using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MExplorer.Registry
{
    class KeyVM : ContainerVM, IDisposable
    {
        //public KeyVM(IItemProvider provider, ContainerVM parent, string name)
        //    : base(provider, parent, name, false)
        //{
        //}
        public KeyVM(IItemProvider provider, ContainerVM parent, RegistryKey key)
            : base(provider, parent, Path.GetFileName(key.Name), false)
        {
            _key = key;
        }
        public override bool IncludeContainersInAll { get { return false; } }

        private RegistryKey _key;
        public RegistryKey Key
        {
            get
            {
                //if (_key == null)
                //{
                //    return GetRoot().Key;
                //}
                return _key;
            }
        }

        //public string GetKeyPath
        //{
        //    get
        //    {
        //        StringBuilder sb = new StringBuilder(Name);
        //        KeyVM root = Parent as KeyVM;
        //        while (root != null && root != this && root.Parent != null && root.Parent is KeyVM)
        //        {
        //            root = (KeyVM)root.Parent;
        //            sb.Insert(0, root.Name).Insert(0, "\\");
        //        }

        //        return sb.ToString();
        //    }
        //}

        //KeyVM GetRoot()
        //{
        //    KeyVM root = Parent as KeyVM;
        //    while (root != null && root != this && root.Parent != null && root.Parent is KeyVM)
        //    {
        //        root = (KeyVM)root.Parent;
        //    }
        //    return root;
        //}

        protected override void OnDisposeManaged()
        {
            if (_key != null)
            {
                _key.Dispose();
                _key = null;
            }
            base.OnDisposeManaged();
        }
    }
}