using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FileManager
{
    public sealed partial class FileExplorer : Page
    {
        public static FileExplorer Inst;
        public string CurrPath { get; set; }
        public string currPath = "C:";
        public StorageFolder currStoFo;

        public ListView FileList => lst_files;
        public StorageFolder SelStoFo => lst_folders.SelectedItem as StorageFolder;
        public StorageFile SelStoFi => lst_files.SelectedItem as StorageFile;
        public List<StorageFile> SelStoFis => lst_files.SelectedItems.Cast<StorageFile>().ToList();

        public FileExplorer()
        {
            InitializeComponent();
            StartFileExplorer();
        }
        private void StartFileExplorer()
        {
            Inst = this;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            OpenBrowser();
            //PopulatePseudoFolderSubMenu();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)

        {
            base.OnNavigatedTo(e);
            PopulatePseudoFolderSubMenu();
        }
        private async void btn_dirUp_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder sf = await currStoFo.GetParentAsync();
            if (sf == null) return;
            SetParentStorageFolder(sf);
        }
        private void btn_browse_click(object sender, RoutedEventArgs e)
        {
            OpenBrowser();
        }
        private void btn_refresh_click(object sender, RoutedEventArgs e)
        {
            RefreshAllDisplays(currStoFo);
        }

        private async void OpenBrowser()
        {
            FolderPicker foPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            foPicker.FileTypeFilter.Add("*");
            StorageFolder sf = await foPicker.PickSingleFolderAsync();
            if (sf != null)
            {
                SetParentStorageFolder(sf);
            }
            else
            {
                //this.txt_currDir.Text = "Operation cancelled.";
            }
        }



        private void SetParentStorageFolder(StorageFolder sf)
        {
            currStoFo = sf;
            currPath = sf.Path;
            CurrPath = sf.Path;
            this.txt_currDir.Text = CurrPath;
            // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", sf);
            RefreshAllDisplays(currStoFo);
        }

        private void RefreshAllDisplays(StorageFolder sf)
        {
            GetFoldersFromStorageFolder(sf);
            GetFilesFromStorageFolder(sf);
        }

        private async void GetFoldersFromStorageFolder(StorageFolder sf)
        {
            var sfs = await sf.GetFoldersAsync();
            this.lst_folders.ItemsSource = sfs;
        }
        private async void GetFilesFromStorageFolder(StorageFolder sf)
        {
            var sfs = await sf.GetFilesAsync();
            this.lst_files.ItemsSource = sfs;
        }

        private void lst_folders_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void lst_folders_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (SelStoFo != null)
            {
                SetParentStorageFolder(SelStoFo);
            }
        }
        private void lst_folders_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            if (this.lst_folders.SelectedItem != null)
                this.lst_folders.SelectedItem = GetDataContextFromSource<StorageFolder>(e);
        }
        private void cntxt_folders_open_click(object sender, RoutedEventArgs e)
        {
            if (SelStoFo != null)
            {
                SetParentStorageFolder(SelStoFo);
            }
        }
        private void lst_files_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void lst_files_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Func.LaunchFileFromPath(SelStoFi.Path, SelStoFi.Name);
        }
        private void lst_files_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            if (this.lst_files.SelectedItem == null) this.lst_files.SelectedItem = GetDataContextFromSource<StorageFile>(e);
        }

        private void cntxt_files_open_click(object sender, RoutedEventArgs e)
        {
            Func.LaunchFileFromPath(SelStoFi.Path, SelStoFi.Name);
        }
        private void cntxt_files_psFolders_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem mfi = sender as MenuFlyoutItem;
            var item = FML.curr.psFolders[Int32.Parse(mfi.Tag.ToString())];
            string s = "";
            foreach (StorageFile sf in SelStoFis)
            {
                item.psFiles.Add(new PseudoFile(sf.Name, sf.Path));
                s += $" {sf.Name}";
            }
            s += $" {SelStoFi.Name}";
            MainPage.WriteToConsole("Added Pseudofile(s) to", item.Name, s);
        }

        public static string StorageFolderChildCount(StorageFolder sf)
        {
            var v = GetChildCount(sf);
            return v.Result;
        }
        public static async Task<string> GetChildCount(StorageFolder sf)
        {
            var v = await sf.GetItemsAsync();
            return v.Count.ToString();
        }

        private void PopulatePseudoFolderSubMenu()
        {
            cntxt_files_psFolders.Items.Clear();
            for (int i = 0; i < FML.curr.psFolders.Count; i++)
            {
                PseudoFolder pf = FML.curr.psFolders[i];
                MenuFlyoutItem mfi = new MenuFlyoutItem();
                mfi.Text = pf.Name;
                mfi.Tag = i;
                mfi.Click += cntxt_files_psFolders_Click;
                cntxt_files_psFolders.Items.Add(mfi);
            }
        }
        public T GetDataContextFromSource<T>(RoutedEventArgs src)
        {
            return (T)(src.OriginalSource as FrameworkElement).DataContext;
        }
    }
}
