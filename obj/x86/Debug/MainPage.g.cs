﻿#pragma checksum "E:\_LocalProjects\FileManager\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E10DCBDDE96FE95DBAB4482B36067124"
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
    partial class MainPage : 
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
            case 2: // MainPage.xaml line 12
                {
                    this.lst_files = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_files).SelectionChanged += this.lst_files_SelectionChanged;
                }
                break;
            case 3: // MainPage.xaml line 16
                {
                    this.btn_refresh = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_refresh).Click += this.btn_refresh_click;
                }
                break;
            case 4: // MainPage.xaml line 17
                {
                    this.txt_currDir = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.txt_currDir).SelectionChanged += this.TextBlock_SelectionChanged;
                }
                break;
            case 5: // MainPage.xaml line 18
                {
                    this.lst_dirs = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_dirs).SelectionChanged += this.lst_dirs_SelectionChanged;
                }
                break;
            case 6: // MainPage.xaml line 21
                {
                    this.btn_browse = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_browse).Click += this.btn_browse_click;
                }
                break;
            case 7: // MainPage.xaml line 22
                {
                    this.txt_tags = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // MainPage.xaml line 23
                {
                    this.lst_folderTags = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 9: // MainPage.xaml line 24
                {
                    this.txt_fileTags = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // MainPage.xaml line 25
                {
                    this.lst_fileTags = (global::Windows.UI.Xaml.Controls.ListView)(target);
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

