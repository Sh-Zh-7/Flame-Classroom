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
    /// Sign.xaml 的交互逻辑
    /// </summary>
    public partial class Sign : Window
    {
        public Sign()
        {
            InitializeComponent();
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            App.student.RegisterSend();
            this.Close();
        }
    }
}
