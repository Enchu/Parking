﻿#pragma checksum "..\..\..\Windows\SpravochnikWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "45EF76D8AF74AE14333BEE30F62F361CD75C47959AEFB6CB0B835944B51D830A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Parking.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Parking.Windows {
    
    
    /// <summary>
    /// SpravochnikWindow
    /// </summary>
    public partial class SpravochnikWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\Windows\SpravochnikWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CBSpravka;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\SpravochnikWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name1tb;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Windows\SpravochnikWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name2tb;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Windows\SpravochnikWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Namett;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Windows\SpravochnikWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridOsn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Parking;component/windows/spravochnikwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\SpravochnikWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 23 "..\..\..\Windows\SpravochnikWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickExit);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CBSpravka = ((System.Windows.Controls.ComboBox)(target));
            
            #line 38 "..\..\..\Windows\SpravochnikWindow.xaml"
            this.CBSpravka.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CBSpravkaSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Name1tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Name2tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Namett = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 44 "..\..\..\Windows\SpravochnikWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClickAdd);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 45 "..\..\..\Windows\SpravochnikWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClickRed);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DataGridOsn = ((System.Windows.Controls.DataGrid)(target));
            
            #line 51 "..\..\..\Windows\SpravochnikWindow.xaml"
            this.DataGridOsn.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DataGridMouseDoubleClickOsn);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
