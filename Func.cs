using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.System;
using Windows.UI.Xaml.Data;

namespace FileManager
{
    class Func
    {
        public static async void LaunchFileFromPath(string path, string name)
        {
            StorageFile sf = await StorageFile.GetFileFromPathAsync(path);
            if (await Launcher.LaunchFileAsync(sf))
                MainPage.WriteToConsole("Launched File", name, DateTime.Now.ToString());
            else
                MainPage.WriteToConsole("FAILED to launch File", name, DateTime.Now.ToString());
        }
        public static async void LaunchFile(PseudoFile ps)
        {
            StorageFile sf = await StorageFile.GetFileFromPathAsync(ps.path);
            if (await Launcher.LaunchFileAsync(sf))
                MainPage.WriteToConsole("Launched File", ps.name, DateTime.Now.ToString());
            else
                MainPage.WriteToConsole("FAILED to launch File", ps.name, DateTime.Now.ToString());
        }
    }
    public sealed class StorageObjInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var v = value as StorageItemContentProperties;
            if (value == null)
                return null;

            //if (parameter == null)
            //    return value;

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
    public sealed class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}
