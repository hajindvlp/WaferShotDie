using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WaferShotDie
{
    /// <summary>
    /// Interaction logic for Logger.xaml
    /// </summary>
    public partial class Logger : Window
    {
        public Logger()
        {
            InitializeComponent();
        }

        public void Log(string S)
        {
            TextBlock.Text += $"{S}\n";
        }
    }  
}
