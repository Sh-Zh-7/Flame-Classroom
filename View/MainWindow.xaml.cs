using FlameClassroom.Pages;
using FlameClassroom.Windows;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

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
        public HomePage homePage;
        public IPInput IPInputPage = new IPInput();
        // Live environment
        public TeacherLive teacherLivePage = new TeacherLive();
        public StudentLive studentLivePage = new StudentLive();

        // Identification
        public string identification;

        public MainWindow()
        {
            InitializeComponent();

            // Bind parent window
            signInPage.parentWindow = this;
            signUpPage.parentWindow = this;
            IPInputPage.parentWindow = this;
            teacherLivePage.parentWindow = this;
            studentLivePage.parentWindow = this;

            //var page = new JudgeCount(new List<int> { 4, 5, 6});
            //page.Show();
            //page.UpdateValues(new List<int> { 1, 2, 3});
            mainFrame.Content = IPInputPage;
        }
    }
}
