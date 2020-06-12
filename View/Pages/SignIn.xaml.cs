using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace FlameClassroom.Pages
{
    /// <summary>
    /// Student.xaml 的交互逻辑
    /// </summary>
    public partial class SignIn : Page
    {
        public MainWindow parentWindow { set; get; }
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            App.student.LoginNotExistEvent += Student_LoginNotExistEvent;
            App.student.LoginWrongPasswordEvent += Student_LoginWrongPasswordEvent;
            App.student.LoginSuccessEvent += Student_LoginSuccessEvent;
            App.student.Login(LoginUserName.Text, LoginPassword.Password);
        }

        private void Student_LoginSuccessEvent(object sender, System.EventArgs e)
        {
            MessageBox.Show("登陆成功！");
            //TODO:消息框如何处理？
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                parentWindow.homePage = new HomePage(parentWindow);
                parentWindow.mainFrame.Content = parentWindow.homePage;
            });
        }

        private void Student_LoginWrongPasswordEvent(object sender, System.EventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.warning.Content = "Password Error!";
            });
        }

        private void Student_LoginNotExistEvent(object sender, System.EventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.warning.Content = "Account doesn't exist!";
            });
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.signUpPage;
 
        }

        private void Return_to_ID_Click(object sender, RoutedEventArgs e)
        {
            var newInputPage = new IPInput();
            newInputPage.parentWindow = parentWindow;
            parentWindow.mainFrame.Content = newInputPage;
        }
    }
}
