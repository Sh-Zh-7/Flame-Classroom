using FlameClassroom.Pages;
using FlameClassroom.Windows;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Unosquare.FFME;
using System.IO;

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

            teacherLivePage.AddVoice(App.ffmpegthread.AudioList);
            teacherLivePage.AddVideo(App.ffmpegthread.VideoList);
            studentLivePage.parentWindow = this;

            mainFrame.Content = IPInputPage;

            Library.FFmpegDirectory = Directory.GetCurrentDirectory() + @"\ffmpeg";
            Library.LoadFFmpeg();
        }
    }
}
