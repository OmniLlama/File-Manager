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
        public Fml fml;
        StorageFolder installedLocationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public StorageFile fmlStorFil;
        public StorageFile currMetaFile;
        FolderInfo currFolderInfo;
        FileInfo currFileInfo;
        List<FileInfo> currFilesInfo;
        List<string[]> metaTags;

        public string[] currTaggedFiles;

        string[] currDirs;
        string[] currFiles;
        public string currPath = "E:";

        byte[] metaBuffer;
        string tagString;
        string[] idxTagLines;
        List<string> idxTags;

        public MainPage()
        {
            StartApplication();
            InitializeComponent();
            OpenBrowser();
        }



        public async void StartApplication()
        {
            Inst = this;
            try
            {
                fmlStorFil = await localFolder.GetFileAsync("base.fml");
                fml = Xml.ReadFromXmlFile<Fml>(fmlStorFil.Path);
            }
            catch
            {
                fml = new Fml();
                fmlStorFil = await localFolder.CreateFileAsync("base.fml");
                fml.path = fmlStorFil.Path;
                fml.WriteToBaseFML(false);
                //Xml.WriteToXmlFile(baseFML.Path, fml, false);
            }
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
        private async void ParseBaseFML(StorageFolder folder)
        {

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
                    currMetaFile = await folder.GetFileAsync("_meta.fm");
                    //ReadMetaFile();
                    IRandomAccessStream iras = await currMetaFile.OpenAsync(FileAccessMode.ReadWrite);
                    Stream stream = iras.AsStreamForRead();
                    metaBuffer = new byte[stream.Length];
                    stream.Read(metaBuffer, 0, (int)stream.Length);
                    stream.Close();
                    ParseMetaFile(folder);

                }
                catch
                {
                    currMetaFile = null;
                }
            }
        }
        private async void ReadMetaFile()
        {
            
        }
        private void ParseMetaFile(StorageFolder folder)
        {
            idxTags = new List<string>();
            tagString = System.Text.Encoding.Default.GetString(metaBuffer);
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
            currFolderInfo = new FolderInfo(folder.Name, folder.Path, temp_tags);
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

        private void RefreshDirDisplay(string path)
        {
            currDirs = Directory.GetDirectories(path);
            this.lst_dirs.Items.Clear();
            foreach (string dir in currDirs)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.FontWeight = FontWeights.Bold;
                lvi.Content = dir;
                this.lst_dirs.Items.Add(lvi);
            }

        }
        private void RefreshFileDisplay(string path)
        {
            currFiles = Directory.GetFiles(path);
            this.lst_files.Items.Clear();
            foreach (string file in currFiles)
            {
                ListViewItem lvi = new ListViewItem();
                string name = Path.GetFileName(file);
                lvi.Content = $"{name}";
                this.lst_files.Items.Add(lvi);
            }
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

        private void btn_fmlEditor_Click(object sender, RoutedEventArgs e)
        {
            var fmlEd = new FmlEditor();
            this.Content = fmlEd;
        }
    }

}
