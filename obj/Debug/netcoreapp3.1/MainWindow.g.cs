﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D44A989FDE259C30C32E131C6BEA529C71ADF7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WaferShotDie;


namespace WaferShotDie {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WaferConfig;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numWaferDiameter;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox UnitConfig;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numUnitWidth;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numUnitHeight;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ButtonControls;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddNewChipButton;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DrawChipButton;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Calculate;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ChipsetCofigGrid;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ChipsetListView;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CalcuationsGrid;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock mxUnitNumText;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock mxChipNumText;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock mxDistText;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border WaferCanvasContainer;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas WaferCanvas;
        
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
            System.Uri resourceLocater = new System.Uri("/WaferShotDie;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
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
            
            #line 8 "..\..\..\MainWindow.xaml"
            ((WaferShotDie.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.WaferConfig = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.numWaferDiameter = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\MainWindow.xaml"
            this.numWaferDiameter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.numWaferDiameter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UnitConfig = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 6:
            this.numUnitWidth = ((System.Windows.Controls.TextBox)(target));
            
            #line 61 "..\..\..\MainWindow.xaml"
            this.numUnitWidth.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.numUnitWidth_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.numUnitHeight = ((System.Windows.Controls.TextBox)(target));
            
            #line 70 "..\..\..\MainWindow.xaml"
            this.numUnitHeight.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.numUnitHeight_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonControls = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.AddNewChipButton = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\MainWindow.xaml"
            this.AddNewChipButton.Click += new System.Windows.RoutedEventHandler(this.AddNewChipButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.DrawChipButton = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\MainWindow.xaml"
            this.DrawChipButton.Click += new System.Windows.RoutedEventHandler(this.DrawChipButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Calculate = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\MainWindow.xaml"
            this.Calculate.Click += new System.Windows.RoutedEventHandler(this.Calculate_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ChipsetCofigGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 13:
            this.ChipsetListView = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 14:
            this.CalcuationsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.mxUnitNumText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.mxChipNumText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.mxDistText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.WaferCanvasContainer = ((System.Windows.Controls.Border)(target));
            
            #line 184 "..\..\..\MainWindow.xaml"
            this.WaferCanvasContainer.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.WaferCanvas_MouseWheel);
            
            #line default
            #line hidden
            
            #line 185 "..\..\..\MainWindow.xaml"
            this.WaferCanvasContainer.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.WaferCanvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 186 "..\..\..\MainWindow.xaml"
            this.WaferCanvasContainer.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.WaferCanvas_MouseUp);
            
            #line default
            #line hidden
            
            #line 187 "..\..\..\MainWindow.xaml"
            this.WaferCanvasContainer.MouseMove += new System.Windows.Input.MouseEventHandler(this.WaferCanvas_MouseMove);
            
            #line default
            #line hidden
            return;
            case 19:
            this.WaferCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

