﻿#pragma checksum "..\..\..\POMOI\WindowSpravochnik.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "07E29A72E5EF39A42A103AC1496599B2B0B4C3D4E9B490B5E21BDAF8B4D8F984"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Parking;
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


namespace Parking {
    
    
    /// <summary>
    /// WindowSpravochnik
    /// </summary>
    public partial class WindowSpravochnik : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\POMOI\WindowSpravochnik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CBSpravka;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\POMOI\WindowSpravochnik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name1tb;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\POMOI\WindowSpravochnik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name2tb;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\POMOI\WindowSpravochnik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridOsn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\POMOI\WindowSpravochnik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Namett;
        
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
            System.Uri resourceLocater = new System.Uri("/Parking;component/pomoi/windowspravochnik.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\POMOI\WindowSpravochnik.xaml"
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
            this.CBSpravka = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\..\POMOI\WindowSpravochnik.xaml"
            this.CBSpravka.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CBSpravka_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\..\POMOI\WindowSpravochnik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickAdd);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 13 "..\..\..\POMOI\WindowSpravochnik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickRed);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 14 "..\..\..\POMOI\WindowSpravochnik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickDel);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 15 "..\..\..\POMOI\WindowSpravochnik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickBack);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Name1tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.Name2tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.DataGridOsn = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\..\POMOI\WindowSpravochnik.xaml"
            this.DataGridOsn.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DataGridMouseDoubleClickOsn);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Namett = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
