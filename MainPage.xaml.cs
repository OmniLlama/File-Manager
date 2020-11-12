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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FileManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public StorageFile currMetaFile;
        FolderInfo currFolderInfo;
        FileInfo currFileInfo;
        List<FileInfo> currFilesInfo;
        List<string[]> metaTags;

        public string[] currTaggedFiles;

        string[] currDirs;
        string[] currFiles;
        public string currPath = "E:";

        int tagBufferSize = 4096;
        byte[] tagBuffer;
        string tagString;
        string[] idxTagLines;
        List<string> dirTags;

        public MainPage()
        {
            InitializeComponent();
            OpenBrowser();
        }



        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

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
        async private void OpenBrowser()
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
                RefreshAllDisplays(currPath);

                lst_folderTags.Items.Clear();

                GetMetaFileFromFolder(folder);
            }
            else
            {
                txt_currDir.Text = "Operation cancelled.";
            }
        }
        async private void GetMetaFileFromFolder(StorageFolder folder)
        {
            if (folder.GetFileAsync("_meta.fm") != null)
            {
                try
                {
                    currMetaFile = await folder.GetFileAsync("_meta.fm");
                    IRandomAccessStream iras = await currMetaFile.OpenAsync(FileAccessMode.ReadWrite);
                    Stream stream = iras.AsStreamForRead();
                    tagBuffer = new byte[stream.Length];
                    stream.Read(tagBuffer, 0, (int)stream.Length);
                    stream.Close();
                    ParseMetaFile(folder);

                }
                catch
                {
                    currMetaFile = null;
                }
            }
        }
        private void ParseMetaFile(StorageFolder folder)
        {
            dirTags = new List<string>();
            tagString = System.Text.Encoding.Default.GetString(tagBuffer);
            tagString = tagString.Replace("\r", "").Replace("\n", "");
            var temp = tagString.Split("@");
            idxTagLines = temp.First().Split("|").Skip(1).ToArray();
            currTaggedFiles = temp.Skip(1).ToArray();

            List<string> tempTags = new List<string>();
            foreach (string tag in idxTagLines)
            {
                if (tag.Length > 0)
                {
                    string tempTag = tag.Trim('|');
                    dirTags.Add(tempTag);
                    tempTags.Add(tempTag);
                    lst_folderTags.Items.Add(tempTag);
                }
            }
            currFolderInfo = new FolderInfo(folder.Name, tempTags);
        }
        private void RefreshAllDisplays(string path)
        {
            txt_currDir.Text = path;
            RefreshDirDisplay(path);
            RefreshFileDisplay(path);
        }

        private void RefreshDirDisplay(string path)
        {
            currDirs = Directory.GetDirectories(path);
            lst_dirs.Items.Clear();
            foreach (string dir in currDirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.FontWeight = FontWeights.Bold;
                lvi.Content = dir;
                lst_dirs.Items.Add(lvi);
            }

        }
        private void RefreshFileDisplay(string path)
        {
            currFiles = Directory.GetFiles(path);
            lst_files.Items.Clear();
            foreach (string file in currFiles)
            {
                ListViewItem lvi = new ListViewItem();
                string name = Path.GetFileName(file);
                lvi.Content = $"{name}";
                lst_files.Items.Add(lvi);
            }
        }
        private void ChangeFileSelection()
        {
            lst_fileTags.Items.Clear();
            if (lst_files.SelectedItem == null) return;

            ListViewItem lvi = lst_files.SelectedItem as ListViewItem;
            if (currTaggedFiles == null) return;
            for (int i = 0; i < currTaggedFiles.Length; i++)
            {
                if (currTaggedFiles[i].Contains($"{lvi.Content}"))
                {
                    string[] tagArr = currTaggedFiles[i].Split("#").Skip(1).ToArray();
                    foreach (string tag in tagArr)
                    {
                        if (tag.Length > 0)
                            lst_fileTags.Items.Add(tag.Replace("\n", ""));
                    }
                }
            }
        }
    }

}
