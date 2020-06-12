using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveCharts;
using LiveCharts.Wpf;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlameClassroom.UserControls;

namespace FlameClassroom.Windows
{
    /// <summary>
    /// JudgeCount.xaml 的交互逻辑
    /// </summary>
    public partial class JudgeCount : Window
    {
        public JudgePlot judgePlot;
        public JudgeCount(List<int> list)
        {
            InitializeComponent();
            judgePlot = new JudgePlot(list);
            plotPosition.Children.Add(judgePlot);
        }

        public void UpdateValues(List<int> list)
        {
            judgePlot.SetValues(list);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
