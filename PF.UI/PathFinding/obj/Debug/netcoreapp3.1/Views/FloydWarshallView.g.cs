﻿#pragma checksum "..\..\..\..\Views\FloydWarshallView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A64FD0D17AF222846C3B07526222645A16E328B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PathFinding.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PathFinding.Views {
    
    
    /// <summary>
    /// FloydWarshallView
    /// </summary>
    public partial class FloydWarshallView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 59 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DefaultGraphs;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox NodesGraph;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveNode;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewNodeName;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddNode;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid myDataGrid;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveConnection;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewConnectionTargetName;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewConnectionDistance;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddNewConnection;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CalculateFloyd;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\..\Views\FloydWarshallView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CalculationResult;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PathFinding;component/views/floydwarshallview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\FloydWarshallView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DefaultGraphs = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.NodesGraph = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.RemoveNode = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.NewNodeName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AddNode = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.myDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.RemoveConnection = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.NewConnectionTargetName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.NewConnectionDistance = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.AddNewConnection = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.CalculateFloyd = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.CalculationResult = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

