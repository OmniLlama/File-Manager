using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FileManager
{
    public sealed partial class FileExplorer : Page
    {

        public string currPath = "E:";
        public StorageFile sfMetaFile;

        public string[] currTaggedFiles;

        byte[] metaBuffer;
        string tagString;
        string[] idxTagLines;
        List<string> idxTags;

        public StorageFolder SelStoFo => lst_dirs.SelectedItem as StorageFolder;

        public FileExplorer()
        {
            InitializeComponent();
        }
        private void lst_files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeFileSelection();
        }
        private void lst_dirs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_browse_click(object sender, RoutedEventArgs e)
        {
            OpenBrowser();
        }
        private void btn_refresh_click(object sender, RoutedEventArgs e)
        {
            RefreshDirDisplay(currPath);
            RefreshFileDisplay(currPath);
        }

        private void btn_addFileTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void OpenBrowser()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                currPath = folder.Path;

                this.lst_folderTags.Items.Clear();

                GetMetaFileFromFolder(folder);
                RefreshAllDisplays(currPath);
            }
            else
            {
                this.txt_currDir.Text = "Operation cancelled.";
            }
        }
        private async void GetMetaFileFromFolder(StorageFolder folder)
        {
            if (folder.GetFileAsync("_meta.fm") != null)
            {
                try
                {
                    sfMetaFile = await folder.GetFileAsync("_meta.fm");
                    IRandomAccessStream iras = await sfMetaFile.OpenAsync(FileAccessMode.ReadWrite);
                    Stream stream = iras.AsStreamForRead();
                    metaBuffer = new byte[stream.Length];
                    stream.Read(metaBuffer, 0, (int)stream.Length);
                    stream.Close();
                    ParseMetaFile(folder);

                }
                catch
                {
                    sfMetaFile = null;
                }
            }
        }
        private async void ReadMetaFile()
        {

        }
        private void ParseMetaFile(StorageFolder folder)
        {
            idxTags = new List<string>();
            tagString = Encoding.Default.GetString(metaBuffer);
            var temp_sects = tagString.Replace("\r", "").Replace("\n", "").Split("@");
            idxTagLines = temp_sects.First().Split("|").Skip(1).ToArray();
            var temp_taggedFiles = temp_sects.Skip(1).ToArray();
            currTaggedFiles = new string[temp_taggedFiles.Length];


            List<string> temp_tags = new List<string>();
            foreach (string tag in idxTagLines)
            {
                if (tag.Length > 0)
                {
                    string temp_tag = tag.Trim('|');
                    idxTags.Add(temp_tag);
                    temp_tags.Add(temp_tag);
                    this.lst_folderTags.Items.Add(temp_tag);
                }
            }
            for (int i = 0; i < temp_taggedFiles.Length; i++)
            {

                currTaggedFiles[i] = temp_taggedFiles[i].Split("#").First();
            }
        }
        private void RefreshAllDisplays(string path)
        {
            this.txt_currDir.Text = path;
            RefreshDirDisplay(path);
            RefreshFileDisplay(path);
        }

        private async void RefreshDirDisplay(string path)
        {
            StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(path);
            var sfs = await sf.GetFoldersAsync();
            this.lst_dirs.ItemsSource = sfs;
        }
        private async void RefreshFileDisplay(string path)
        {
            StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(path);
            var sfs = await sf.GetFilesAsync();
            this.lst_files.ItemsSource = sfs;
        }
        private void ChangeFileSelection()
        {
            this.lst_fileTags.Items.Clear();
            if (this.lst_files.SelectedItem == null) return;

            ListViewItem lvi = lst_files.SelectedItem as ListViewItem;
            if (currTaggedFiles == null) return;
            for (int i = 0; i < currTaggedFiles.Length; i++)
            {
                if (currTaggedFiles[i].Equals($"{lvi.Content}"))
                {
                    string[] tagArr = currTaggedFiles[i].Split("#").Skip(1).ToArray();
                    foreach (string tag in tagArr)
                    {
                        if (tag.Length > 0)
                            this.lst_fileTags.Items.Add(tag.Replace("\n", ""));
                    }
                }
            }
        }



        private void stkpnl_folder_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            StorageFolder folder = SelStoFo;
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
                //Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                currPath = folder.Path;

                this.lst_folderTags.Items.Clear();

                GetMetaFileFromFolder(folder);
                RefreshAllDisplays(currPath);
            }
        }

    }
}
