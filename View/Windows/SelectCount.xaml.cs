using FlameClassroom.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlameClassroom.Windows
{
    /// <summary>
    /// SelectCount.xaml 的交互逻辑
    /// </summary>
    public partial class SelectCount : Window
    {
        public SelectionPlot selectionPlot;
        public SelectCount(List<int> list)
        {
            InitializeComponent();
            selectionPlot = new SelectionPlot(list);
            targetPosition.Children.Add(selectionPlot);
        }

        public void UpdateValues(List<int> list)
        {
            selectionPlot.SetValues(list);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
