﻿#pragma checksum "..\..\Tablas.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6F224C610A8C4166C26437666D16302C50353610FAB0924304EFAB98476391D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using EL_PACTOMETRO;
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


namespace EL_PACTOMETRO {
    
    
    /// <summary>
    /// Tablas
    /// </summary>
    public partial class Tablas : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtaddEl;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtdelEl;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BteditEl;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listaVotos;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtaddGe;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtdelGe;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BteditGe;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Tablas.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listaVAutonomicas;
        
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
            System.Uri resourceLocater = new System.Uri("/EL PACTOMETRO;component/tablas.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Tablas.xaml"
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
            
            #line 8 "..\..\Tablas.xaml"
            ((EL_PACTOMETRO.Tablas)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.TablasClose);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Tablas.xaml"
            ((EL_PACTOMETRO.Tablas)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtaddEl = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Tablas.xaml"
            this.BtaddEl.Click += new System.Windows.RoutedEventHandler(this.AddEleccion);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtdelEl = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Tablas.xaml"
            this.BtdelEl.Click += new System.Windows.RoutedEventHandler(this.DelEleccion);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BteditEl = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Tablas.xaml"
            this.BteditEl.Click += new System.Windows.RoutedEventHandler(this.EditEleccion);
            
            #line default
            #line hidden
            return;
            case 5:
            this.listaVotos = ((System.Windows.Controls.ListView)(target));
            
            #line 41 "..\..\Tablas.xaml"
            this.listaVotos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lista_SelectionElecciones);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtaddGe = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\Tablas.xaml"
            this.BtaddGe.Click += new System.Windows.RoutedEventHandler(this.AddAutonomicas);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtdelGe = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\Tablas.xaml"
            this.BtdelGe.Click += new System.Windows.RoutedEventHandler(this.DelAutonomicas);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BteditGe = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\Tablas.xaml"
            this.BteditGe.Click += new System.Windows.RoutedEventHandler(this.EditAutonomicas);
            
            #line default
            #line hidden
            return;
            case 9:
            this.listaVAutonomicas = ((System.Windows.Controls.ListView)(target));
            
            #line 69 "..\..\Tablas.xaml"
            this.listaVAutonomicas.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lista_SelectionAutonomicas);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

