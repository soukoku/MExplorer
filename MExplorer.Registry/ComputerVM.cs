using MExplorer.Registry.Assets;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MExplorer.Registry
{
    class ComputerVM : ContainerVM
    {
        public ComputerVM(IItemProvider provider) : this(provider, null) { }
        public ComputerVM(IItemProvider provider, string machine) :
            base(provider, null, machine ?? "Computer", false)
        {
            IsRemote = !string.IsNullOrEmpty(machine);

            //using (var kf = KnownFolders.Computer)
            //{
            //    _shell = ShellObject.FromParsingName(kf.ParsingName);
            //    _shell.Thumbnail.CurrentSize = new System.Windows.Size(16, 16);
            //}
        }

        //ShellObject _shell;

        protected override ImageSource GetIcon(IconSize size)
        {
            switch (size)
            {
                //case IconSize.ExtraLarge:
                //    return _shell.Thumbnail.LargeBitmapSource;
                //case IconSize.Large:
                //    return _shell.Thumbnail.MediumBitmapSource;
                case IconSize.Medium:
                    return ImageAssets.RegistryMedium;
                case IconSize.Small:
                    return ImageAssets.RegistrySmall;
            }
            return null;
        }

        public override string AppendText
        {
            get
            {
                return "[registry]";
            }
        }

        public bool IsRemote { get; private set; }

        public override bool IncludeContainersInAll { get { return false; } }

        //protected override void OnDisposeManaged()
        //{
        //    if (_shell != null)
        //    {
        //        _shell.Dispose();
        //        _shell = null;
        //    }
        //    base.OnDisposeManaged();
        //}

    }
}
