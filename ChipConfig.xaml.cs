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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaferShotDie
{
    /// <summary>
    /// Interaction logic for ChipConfig.xaml
    /// </summary>
    public partial class ChipConfig : UserControl
    {
        public int ChipNum;

        public ChipConfig(int ChipNum)
        {
            InitializeComponent();
            this.ChipNum = ChipNum-1;
        }

        private void numChipStartX_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.Chips.Unit.set[ChipNum].startX = ToDouble(numChipStartX.Text);
        }

        private void numChipStartY_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.Chips.Unit.set[ChipNum].startY = ToDouble(numChipStartY.Text);
        }

        private void numChipWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.Chips.Unit.set[ChipNum].width = ToDouble(numChipWidth.Text);
        }

        private void numChipHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainWindow.Chips.Unit.set[ChipNum].height = ToDouble(numChipHeight.Text);
        }

        private double ToDouble(string S)
        {
            try
            {
                return Convert.ToDouble(S);
            }
            catch
            {
                return 0f;
            }
        }
    }
}
