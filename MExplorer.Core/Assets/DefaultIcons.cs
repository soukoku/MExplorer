using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MExplorer.Assets
{
    /// <summary>
    /// Defines the default item icons if available.
    /// </summary>
    public static class DefaultIcons
    {
        static DefaultIcons()
        {
            SmallContainer = LoadImage("Folder16.png");
            MediumContainer = LoadImage("Folder32.png");
            LargeContainer= LoadImage("Folder96.png");
            ExtraLargeContainer = LoadImage("Folder256.png");

            SmallItem = LoadImage("File16.png");
            MediumItem = LoadImage("File32.png");
            LargeItem = LoadImage("File96.png");
            ExtraLargeItem = LoadImage("File256.png");
        }

        private static ImageSource LoadImage(string resourceName)
        {
            var uri = new Uri("pack://application:,,,/MExplorer.Core;component/Assets/" + resourceName);

            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = uri;
            img.EndInit();
            img.Freeze();
            return img;
        }

        /// <summary>
        /// Gets the small container icon.
        /// </summary>
        /// <value>
        /// The small container.
        /// </value>
        public static ImageSource SmallContainer { get; private set; }
        /// <summary>
        /// Gets the medium container icon.
        /// </summary>
        /// <value>
        /// The medium container.
        /// </value>
        public static ImageSource MediumContainer { get; private set; }
        /// <summary>
        /// Gets the large container icon.
        /// </summary>
        /// <value>
        /// The large container.
        /// </value>
        public static ImageSource LargeContainer { get; private set; }
        /// <summary>
        /// Gets the extra large container icon.
        /// </summary>
        /// <value>
        /// The extra large container.
        /// </value>
        public static ImageSource ExtraLargeContainer { get; private set; }


        /// <summary>
        /// Gets the small item icon.
        /// </summary>
        /// <value>
        /// The small item.
        /// </value>
        public static ImageSource SmallItem { get; private set; }
        /// <summary>
        /// Gets the medium item icon.
        /// </summary>
        /// <value>
        /// The medium item.
        /// </value>
        public static ImageSource MediumItem { get; private set; }
        /// <summary>
        /// Gets the large item icon.
        /// </summary>
        /// <value>
        /// The large item.
        /// </value>
        public static ImageSource LargeItem { get; private set; }
        /// <summary>
        /// Gets the extra large item icon.
        /// </summary>
        /// <value>
        /// The extra large item.
        /// </value>
        public static ImageSource ExtraLargeItem { get; private set; }
    }
}
