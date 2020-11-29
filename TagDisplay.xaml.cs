using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FileManager
{
    public sealed partial class TagDisplay : Page
    {
        public string[] currTaggedFiles;

        byte[] metaBuffer;
        public StorageFile sfMetaFile;

        string tagString;
        string[] idxTagLines;
        List<string> idxTags;

        private void btn_addFileTag_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ChangeFileSelection()
        {
            this.lst_fileTags.Items.Clear();
            if (FileExplorer.Inst.FileList.SelectedItem == null) return;

            ListViewItem lvi = FileExplorer.Inst.FileList.SelectedItem as ListViewItem;
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
                    //ParseMetaFile(folder);

                }
                catch
                {
                    sfMetaFile = null;
                }
            }
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
    }
}
