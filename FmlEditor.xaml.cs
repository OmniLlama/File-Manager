using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace FileManager
{
    public sealed partial class FmlEditor : Page
    {
        public static FmlEditor Inst;
        public Fml fml;
        public FmlEditor()
        {
            InitializeComponent();
            PopulateLists(MainPage.Inst.fml);
        }
        private void PopulateLists(Fml fml)
        {
            lst_pseudoFolders.Items.Clear();
            foreach (PseudoFolder pf in fml.psFolders)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Content = pf.name;
                lst_pseudoFolders.Items.Add(lvi);
            }
        }
        private void btn_addFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_addPseudoFolder_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Inst.fml.psFolders.Add(new PseudoFolder());
            //Xml.WriteToXmlFile(MainPage.Inst.fmlStorFil.Path, MainPage.Inst.fml, false);
            PopulateLists(MainPage.Inst.fml);
        }

        private void lst_pseudoFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst_pseudoFolders.SelectedItem == null) return;
            txtbox_pseudoFolderName.Text = ((ListViewItem)lst_pseudoFolders.SelectedItem).Content.ToString();
        }

        private void btn_updatePseudoFolderName_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainPage.Inst.fml.psFolders[lst_pseudoFolders.SelectedIndex].name = txtbox_pseudoFolderName.Text;
            PopulateLists(MainPage.Inst.fml);
        }

        private void btn_saveFML_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Inst.fml.WriteToBaseFML(false);
        }
    }
}
