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
    /// Judege.xaml 的交互逻辑
    /// </summary>
    public partial class Judege : Window
    {
        public Judege()
        {
            InitializeComponent();
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            if(BoxT.IsChecked==true)
            {
                App.student.TFsend("true");
            }
            else if(BoxF.IsChecked==true)
            {
                App.student.TFsend("false");
            }
            this.Close();
        }
    }
}
