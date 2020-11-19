﻿#pragma checksum "E:\_LocalProjects\FileManager\FmlEditor.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "871A13C40615040C921F6DE8F91A5795"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileManager
{
    partial class FmlEditor : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // FmlEditor.xaml line 12
                {
                    this.btn_addPseudoFile = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_addPseudoFile).Click += this.btn_addPseudoFile_Click;
                }
                break;
            case 3: // FmlEditor.xaml line 13
                {
                    this.btn_addPseudoFolder = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_addPseudoFolder).Click += this.btn_addPseudoFolder_Click;
                }
                break;
            case 4: // FmlEditor.xaml line 14
                {
                    this.lst_pseudoFolders = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_pseudoFolders).SelectionChanged += this.lst_pseudoFolders_SelectionChanged;
                }
                break;
            case 5: // FmlEditor.xaml line 15
                {
                    this.lst_pseudoFolderFiles = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_pseudoFolderFiles).SelectionChanged += this.lst_pseudoFolderFiles_SelectionChanged;
                }
                break;
            case 6: // FmlEditor.xaml line 16
                {
                    this.txtbox_pseudoFolderName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // FmlEditor.xaml line 17
                {
                    this.btn_updatePseudoFolderName = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_updatePseudoFolderName).Click += this.btn_updatePseudoFolderName_Click;
                }
                break;
            case 8: // FmlEditor.xaml line 18
                {
                    this.btn_saveFML = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_saveFML).Click += this.btn_saveFML_Click;
                }
                break;
            case 9: // FmlEditor.xaml line 19
                {
                    this.btn_removePseudoFile = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_removePseudoFile).Click += this.btn_removePseudoFile_Click;
                }
                break;
            case 10: // FmlEditor.xaml line 20
                {
                    this.btn_openPseudoFile = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_openPseudoFile).Click += this.btn_openPseudoFile_Click;
                }
                break;
            case 11: // FmlEditor.xaml line 21
                {
                    this.lst_pseudoFolderTags = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 12: // FmlEditor.xaml line 22
                {
                    this.lst_pseudoFileTags = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_pseudoFileTags).SelectionChanged += this.lst_pseudoFileTags_SelectionChanged;
                }
                break;
            case 13: // FmlEditor.xaml line 23
                {
                    this.btn_removePseudoFolder = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_removePseudoFolder).Click += this.btn_removePseudoFolder_Click;
                }
                break;
            case 14: // FmlEditor.xaml line 24
                {
                    this.txtbox_folderTag = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15: // FmlEditor.xaml line 25
                {
                    this.btn_addFolderTag = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_addFolderTag).Click += this.btn_addFolderTag_Click;
                }
                break;
            case 16: // FmlEditor.xaml line 26
                {
                    this.btn_removeFolderTag = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_removeFolderTag).Click += this.btn_removeFolderTag_Click;
                }
                break;
            case 17: // FmlEditor.xaml line 27
                {
                    this.txtbox_fileTag = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18: // FmlEditor.xaml line 28
                {
                    this.btn_addFileTag = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_addFileTag).Click += this.btn_addFileTag_Click;
                }
                break;
            case 19: // FmlEditor.xaml line 29
                {
                    this.btn_removeFileTag = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_removeFileTag).Click += this.btn_removeFileTag_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

