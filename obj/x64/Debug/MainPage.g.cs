﻿#pragma checksum "E:\_LocalProjects\FileManager\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1D9C1E18EFABEFDA50E1C2177596F7C4"
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
            case 2: // MainPage.xaml line 11
                {
                    this.grd_nav = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // MainPage.xaml line 22
                {
                    this.frm_main = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 4: // MainPage.xaml line 26
                {
                    this.txt_actionConsole1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // MainPage.xaml line 27
                {
                    this.txt_actionConsole2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // MainPage.xaml line 28
                {
                    this.txt_actionConsole3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // MainPage.xaml line 29
                {
                    this.txt_actionConsole4 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // MainPage.xaml line 12
                {
                    this.nav_main = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.nav_main).ItemInvoked += this.nav_main_ItemInvoked;
                }
                break;
            case 9: // MainPage.xaml line 14
                {
                    this.nav_fmlEditor = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 10: // MainPage.xaml line 15
                {
                    this.nav_fileExplorer = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 11: // MainPage.xaml line 16
                {
                    this.btn_saveFML = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_saveFML).Click += this.btn_saveFML_Click;
                }
                break;
            case 12: // MainPage.xaml line 17
                {
                    this.btn_revertFML = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_revertFML).Click += this.btn_revertFML_Click;
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
