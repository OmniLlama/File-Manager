using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;

namespace FileManager
{
    static class Ext
    {
        public static async void LaunchFile(this StorageFile sf)
        {
            if (await Launcher.LaunchFileAsync(sf))
                MainPage.WriteToConsole("Launched File", ConsoleMessageType.Success, sf.Name);
            else
                MainPage.WriteToConsole("FAILED to launch File", ConsoleMessageType.Error, sf.Name);
        }
    }
}
