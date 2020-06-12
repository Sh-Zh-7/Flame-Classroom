using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

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

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            App.student.AccountCreateEvent += Student_AccountCreateEvent;
            App.student.AccountExistEvent += Student_AccountExistEvent;
            App.student.CreatAccount(SignupUserName.Text, SignupPassword.Text);
        }

        private void Student_AccountExistEvent(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.warning.Visibility = Visibility.Visible;
            });

        }

        private void Student_AccountCreateEvent(object sender, EventArgs e)
        {
            MessageBox.Show("创建账户成功");
            //TODO:消息框的处理？
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.parentWindow.mainFrame.Content = parentWindow.signInPage;
            });
        }
    }
}
