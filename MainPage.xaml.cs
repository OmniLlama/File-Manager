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

        public bool fmlChanged = false;
        public DateTime lastSaved;
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
                FML.last = FML.curr;
                WriteToConsole("FML Loaded!", ConsoleMessageType.Warning, sfFML.Path);
                lastSaved = DateTime.Now;
            }
            catch
            {
                FML.curr = new FML();
                sfFML = await localFolder.CreateFileAsync("base.fml");
                FML.curr.path = sfFML.Path;
                FML.curr.WriteToFile(false);
                WriteToConsole("New FML created in local folder!", ConsoleMessageType.Error, localFolder.Path);
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
            fmlChanged = false;
            lastSaved = DateTime.Now;
            WriteToConsole("Saved FML", FML.curr.path, lastSaved.ToString());
        }
        public static Color clr_nrml = Colors.White;
        public static Color clr_succ = Colors.LightGreen;
        public static Color clr_warn = Colors.DarkOrange;
        public static Color clr_err = Colors.DarkMagenta;
        public static Color clr_info = Color.FromArgb(255, 215, 109, 255);
        public static void WriteToConsole(string msg, ConsoleMessageType cmt1 = ConsoleMessageType.Success, string info = null)
        {
            SolidColorBrush brush = new SolidColorBrush(clr_nrml);
            switch (cmt1)
            {
                case ConsoleMessageType.Success:
                    brush = new SolidColorBrush(clr_succ);
                    break;
                case ConsoleMessageType.Warning:
                    brush = new SolidColorBrush(clr_warn);
                    break;
                case ConsoleMessageType.Error:
                    brush = new SolidColorBrush(clr_err);
                    break;
                default:
                    break;
            }
            Inst.txt_actionConsole1.Foreground = brush;
            Inst.txt_actionConsole1.Text = msg;
            Inst.txt_actionConsole2.Text = info != null ? info : "";
            Inst.txt_actionConsole4.Text = DateTime.Now.ToString();
        }
        public static void WriteToConsole(string s1 = null, string s2 = null, string s3 = null, ConsoleMessageType cmt1 = ConsoleMessageType.Success)
        {
            Inst.txt_actionConsole1.Text = s1 != null ? s1 : "";
            Inst.txt_actionConsole2.Text = s2 != null ? s2 : "";
            Inst.txt_actionConsole4.Text = s3 != null ? s3 : "";
        }

        private void btn_revertFML_Click(object sender, RoutedEventArgs e)
        {
            FML.RevertChanges();
            WriteToConsole("Reverted to last saved FML", FML.curr.path, (DateTime.Now.Subtract(lastSaved).Duration()).ToString());
        }
    }
    public enum ConsoleMessageType { Success, Warning, Error };

}
