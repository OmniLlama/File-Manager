using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FileManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Inst;
        StorageFolder installedLocationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public StorageFile sfFML;

        public NavigationViewItem navItem;

        public MainPage()
        {
            StartApplication();
            InitializeComponent();
        }



        public async void StartApplication()
        {
            Inst = this;
            try
            {
                sfFML = await localFolder.GetFileAsync("base.fml");
                FML.curr = Xml.ReadFromXmlFile<FML>(sfFML.Path);
            }
            catch
            {
                FML.curr = new FML();
                sfFML = await localFolder.CreateFileAsync("base.fml");
                FML.curr.path = sfFML.Path;
                FML.curr.WriteToFile(false);
            }
        }

        private void nav_main_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            navItem = args.InvokedItemContainer as NavigationViewItem;
            var t = Type.GetType($"FileManager.{navItem.Tag}");
            if (t != null)
                this.frm_main.Navigate(t);
        }
    }

}
