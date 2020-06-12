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
    /// SignCount.xaml 的交互逻辑
    /// </summary>
    public partial class SignCount : Window
    {

        public SignPlot signPlot;
        public SignCount(List<int> list)
        {
            InitializeComponent();
            signPlot = new SignPlot(list);
            targetPosition.Children.Add(signPlot);
        }

        public void UpdateValues(List<int> list)
        {
            signPlot.SetValues(list);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
