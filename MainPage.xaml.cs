using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
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
    /// The main page for the app
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
            InitializeComponent();
            StartApplication();
        }



        public async void StartApplication()
        {
            Inst = this;
            try
            {
                sfFML = await localFolder.GetFileAsync("base.fml");
                FML.curr = Xml.ReadFromXmlFile<FML>(sfFML.Path);
                WriteToConsole("FML Loaded!", sfFML.Path);
            }
            catch
            {
                FML.curr = new FML();
                sfFML = await localFolder.CreateFileAsync("base.fml");
                FML.curr.path = sfFML.Path;
                FML.curr.WriteToFile(false);
                WriteToConsole("New FML created in local folder!", localFolder.Path);
            }
        }

        private void nav_main_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                return;
            }
            navItem = args.InvokedItemContainer as NavigationViewItem;
            var t = Type.GetType($"FileManager.{navItem.Tag}");
            if (t != null)
                this.frm_main.Navigate(t);
        }
        private void btn_saveFML_Click(object sender, RoutedEventArgs e)
        {
            FML.curr.WriteToFile(false);
            WriteToConsole("Saved FML", FML.curr.path, DateTime.Now.ToString());
        }
        public static void WriteToConsole(string s1 = null, string s2 = null, string s3 = null)
        {
            Inst.txt_actionConsole1.Text = s1 != null ? s1 : "";
            Inst.txt_actionConsole2.Text = s2 != null ? s2 : "";
            Inst.txt_actionConsole3.Text = s3 != null ? s3 : "";
        }
    }

}
