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
    /// Select.xaml 的交互逻辑
    /// </summary>
    public partial class Select : Window
    {
        public Select()
        {
            InitializeComponent();
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            if(BoxA.IsChecked==true)
            {
                App.student.ChoiceSend("A");
            }
            else if(BoxB.IsChecked==true)
            {
                App.student.ChoiceSend("B");

            }
            else if(BoxC.IsChecked==true)
            {
                App.student.ChoiceSend("C");

            }
            else if (BoxD.IsChecked == true)
            {
                App.student.ChoiceSend("D");
            }
            this.Close();
        }        
    }
}
