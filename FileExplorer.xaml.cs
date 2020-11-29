using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FileManager
{
    public sealed partial class FileExplorer : Page
    {
        public static FileExplorer Inst;
        public string currPath = "C:";
        public StorageFolder currStoFo;

        public ListView FileList => lst_files;
        public StorageFolder SelStoFo => lst_dirs.SelectedItem as StorageFolder;

        public FileExplorer()
        {
            InitializeComponent();
            StartFileExplorer();
        }
        private async void StartFileExplorer()
        {
            Inst = this;
            currStoFo = await StorageFolder.GetFolderFromPathAsync(currPath);
        }
        private void lst_files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void lst_dirs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                this.txt_currDir.Text = "Operation cancelled.";
            }
        }



        private void SetParentStorageFolder(StorageFolder sf)
        {
            currStoFo = sf;
            currPath = sf.Path;
            this.txt_currDir.Text = sf.Path;
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
            this.lst_dirs.ItemsSource = sfs;
        }
        private async void GetFilesFromStorageFolder(StorageFolder sf)
        {
            var sfs = await sf.GetFilesAsync();
            this.lst_files.ItemsSource = sfs;
        }

        private void stkpnl_folder_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (SelStoFo != null)
            {
                SetParentStorageFolder(SelStoFo);
            }
        }

        
        
    }

}
