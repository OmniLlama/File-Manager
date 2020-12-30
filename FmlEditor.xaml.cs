using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using Windows.System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FileManager
{
    public sealed partial class FmlEditor : Page
    {
        public static FmlEditor Inst;
        public PseudoFolder selPSFolder => lst_pseudoFolders.SelectedItem as PseudoFolder;
        public PseudoFile selPSFile => lst_pseudoFolderFiles.SelectedItem as PseudoFile;
        public List<PseudoFile> selPSFiles => lst_pseudoFolderFiles.SelectedItems as List<PseudoFile>;
        public string selPSFolderTag => lst_pseudoFolderTags.SelectedItem as string;
        public string selPSFileTag => lst_pseudoFileTags.SelectedItem as string;
        public FmlEditor()
        {
            InitializeComponent();
            StartFmlEditor();
            RefreshLists();
        }

        private void StartFmlEditor()
        {
            Inst = this;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            RefreshLists();
        }
        private async void btn_addPseudoFile_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFolder == null) return;
            var filePicker = new FileOpenPicker()
            {
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            filePicker.FileTypeFilter.Add("*");
            var stoFiles = await filePicker.PickMultipleFilesAsync();
            string s = "";
            if (stoFiles != null)
            {
                foreach (var sf in stoFiles)
                {
                    selPSFolder.psFiles.Add(new PseudoFile(sf.Name, sf.Path));
                    s += $" {sf.Name}";
                }
                RefreshPseudoFileList();
                MainPage.WriteToConsole("Added Pseudofile(s) to", selPSFolder.name, s);
            }
        }

        private void btn_addPseudoFolder_Click(object sender, RoutedEventArgs e)
        {
            FML.curr.psFolders.Add(new PseudoFolder());
            RefreshLists();
            MainPage.WriteToConsole("Added PseudoFolder");
        }


        private void btn_updatePseudoFolderName_Click(object sender, RoutedEventArgs e)
        {
            selPSFolder.name = txtbox_pseudoFolderName.Text;
            RefreshLists();
        }



        private void btn_removePseudoFile_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedPseudoFiles();
        }



        private void btn_removePseudoFolder_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFolder == null) return;
            FML.curr.psFolders.Remove(selPSFolder);
            MainPage.WriteToConsole("Removed PseudoFolder", selPSFolder.name, DateTime.Now.ToString());
            RefreshPseudoFolderList();
        }


        private void btn_openPseudoFile_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFile != null) Func.LaunchFile(selPSFile);
        }

        private void btn_addFolderTag_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFolder == null) return;
            if (!selPSFolder.tags.Exists(x => x.ToLower().Equals(txtbox_folderTag.Text.ToLower())))
            {
                selPSFolder.tags.Add(txtbox_folderTag.Text);
            }
            RefreshPseudoFolderList();
            MainPage.WriteToConsole("Added tag to", selPSFolder.name, txtbox_folderTag.Text);
            RefreshPseudoFolderTagList();
            txtbox_folderTag.Text = "";
        }

        private void btn_removeFolderTag_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFolderTag == null || selPSFolder == null) return;
            selPSFolder.tags.Remove(selPSFolderTag);
            MainPage.WriteToConsole("Removed tag from", selPSFolder.name, selPSFolderTag);
        }
        private void btn_addFileTag_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFile == null) return;
            if (!selPSFile.tags.Exists(x => x.ToLower().Equals(txtbox_fileTag.Text.ToLower())))
            {
                selPSFile.tags.Add(txtbox_fileTag.Text);
            }
            RefreshPseudoFileList();
            RefreshPseudoFileTagList();
            MainPage.WriteToConsole("Added tag to", selPSFile.name, txtbox_fileTag.Text);
            txtbox_fileTag.Text = "";
        }

        private void btn_removeFileTag_Click(object sender, RoutedEventArgs e)
        {
            if (selPSFileTag == null || selPSFile == null) return;
            selPSFile.tags.Remove(selPSFileTag);
            MainPage.WriteToConsole("Removed tag from", selPSFile.name, selPSFileTag);
        }
        private void lst_pseudoFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selPSFolder == null) return;
            lst_pseudoFolderFiles.ItemsSource = selPSFolder.psFiles;
            lst_pseudoFolderTags.ItemsSource = selPSFolder.tags;
            txtbox_pseudoFolderName.Text = selPSFolder.name;
        }
        private void lst_pseudoFolderFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (selPSFile == null) return;
            lst_pseudoFileTags.ItemsSource = selPSFile.tags;
        }
        private void lst_pseudoFileTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /**
         * REFRESH METHODS
         * */
        private void RefreshPseudoFolderList()
        {
            lst_pseudoFolders.ItemsSource = null;
            lst_pseudoFolders.ItemsSource = FML.curr.psFolders;
        }
        private void RefreshPseudoFolderTagList()
        {
            lst_pseudoFolderTags.ItemsSource = null;
            if (selPSFolder == null) return;
            lst_pseudoFolderTags.ItemsSource = selPSFolder.tags;
        }
        private void RefreshPseudoFileList()
        {
            lst_pseudoFolderFiles.ItemsSource = null;
            if (selPSFolder == null) return;
            lst_pseudoFolderFiles.ItemsSource = selPSFolder.psFiles;
        }
        private void RefreshPseudoFileTagList()
        {
            lst_pseudoFileTags.ItemsSource = null;
            if (selPSFile == null) return;
            lst_pseudoFileTags.ItemsSource = selPSFile.tags;
        }
        private void RefreshLists()
        {
            RefreshPseudoFolderList();
            RefreshPseudoFolderTagList();
            RefreshPseudoFileList();
            RefreshPseudoFileTagList();
        }

        private void cntxt_files_open_click(object sender, RoutedEventArgs e)
        {
            Func.LaunchFileFromPath(selPSFile.path, selPSFile.name);
        }

        private void lst_pseudoFolderFiles_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            //var frmElmt = e.OriginalSource as FrameworkElement;
            //this.lst_pseudoFolderFiles.SelectedItem = frmElmt.DataContext;
        }

        private void cntxt_files_remove_click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedPseudoFiles();
        }
        private void RemoveSelectedPseudoFiles()
        {
            var files = lst_pseudoFolderFiles.SelectedItems.AsEnumerable();
            if (files != null)
            {
                string s = "";
                foreach (PseudoFile ps in files)
                {
                    selPSFolder.psFiles.Remove(ps);
                    s += $" {ps.name}";
                }
                RefreshPseudoFileList();
                MainPage.WriteToConsole("Removed Pseudofile(s) from", selPSFolder.name, s);
            }
        }
    }
}
