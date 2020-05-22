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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlameClassroom.Pages
{
    /// <summary>
    /// SignUp.xaml 的交互逻辑
    /// </summary>

    public partial class SignUp : Page
    {
        public MainWindow parentWindow { set; get; }
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.signInPage;
        }
    }
}
