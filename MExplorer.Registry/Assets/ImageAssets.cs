using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MExplorer.Registry.Assets
{
    static class ImageAssets
    {
        public static readonly ImageSource RegistryMedium = LoadImage("Regedit32.png");

        public static readonly ImageSource RegistrySmall = LoadImage("Regedit16.png");

        public static readonly ImageSource BinarySmall = LoadImage("ValueBinary16.png");
        
        public static readonly ImageSource StringSmall = LoadImage("ValueString16.png");


        private static ImageSource LoadImage(string resourceName)
        {
            var uri = new Uri("pack://application:,,,/MExplorer.Registry;component/Assets/" + resourceName);

            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = uri;
            img.EndInit();
            img.Freeze();
            return img;
        }
    }
}
