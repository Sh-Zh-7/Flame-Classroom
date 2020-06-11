using FlameClassroom.Pages;
using FlameClassroom.Pages.SubPages;
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

namespace FlameClassroom
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // Pages
        public SignIn signInPage = new SignIn();
        public SignUp signUpPage = new SignUp();
        public HomePage homePage = new HomePage();
        public IPInput IPInputPage = new IPInput();
        // Live environment
        public Live livePage = new Live();

        // Identification
        public string identification;

        public MainWindow()
        {
            InitializeComponent();

            // Bind parent window
            signInPage.parentWindow = this;
            signUpPage.parentWindow = this;
            homePage.parentWindow = this;
            IPInputPage.parentWindow = this;
            livePage.parentWindow = this;

            homePage.SetParentWnd();
            
            mainFrame.Content = IPInputPage;
        }
    }
}
