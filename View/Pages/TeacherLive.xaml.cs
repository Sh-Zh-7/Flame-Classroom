using FlameClassroom.Windows;
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
using System.Windows.Controls;

namespace FlameClassroom.Pages
{

    public class Student
    {
        public Student(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
    /// <summary>
    /// Live.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherLive : Page
    {
        public MainWindow parentWindow { set; get; }
        public string liveCondition = "start";
        public SignCount RegisterPage;
        public SelectCount ChoicePage;
        public JudgeCount TFPage;
        public TeacherLive()
        {
            InitializeComponent();

        }
        public void Init()
        {
            App.teacher.RenewStudentListEvent += Teacher_RenewStudentListEvent;
            App.teacher.RenewRegisterEvent += Teacher_RenewRegisterEvent;
            App.teacher.RenewTFEvent += Teacher_RenewTFEvent;
            App.teacher.RenewChoiceEvent += Teacher_RenewChoiceEvent;
            SetTeachersName(App.teacher.Name);
        }

        private void Teacher_RenewChoiceEvent(object sender, Backend.RenewEventArgs e)
        {
            var NewChoiceList = new List<int>();
            int A = 0;
            int B = 0;
            int C = 0;
            int D = 0;
            int notchoose = 0;
            foreach (var item in e.AccountList.Values)
            {
                if (item.choice_Enum == Backend.Choice_enum.A)
                {
                    A++;
                }
                else if (item.choice_Enum == Backend.Choice_enum.B)
                {
                    B++;
                }
                else if (item.choice_Enum == Backend.Choice_enum.C)
                {
                    C++;
                }
                else if(item.choice_Enum == Backend.Choice_enum.D)
                {
                    D++;
                }
                else if(item.choice_Enum == Backend.Choice_enum.ToChoose)
                {
                    notchoose++;
                }
            }

            NewChoiceList.Add(A);
            NewChoiceList.Add(B);
            NewChoiceList.Add(C);
            NewChoiceList.Add(D);
            NewChoiceList.Add(notchoose);


            ChoicePage.UpdateValues(NewChoiceList);
        }

        private void Teacher_RenewTFEvent(object sender, Backend.RenewEventArgs e)
        {
            var NewJudgeList = new List<int>();
            int truechoice = 0;
            int falsechoice = 0;
            int notchoose = 0;
            foreach (var item in e.AccountList.Values)
            {
                if (item.tF_Enum == Backend.TF_enum.T)
                {
                    truechoice++;
                }
                else if (item.tF_Enum == Backend.TF_enum.F)
                {
                    falsechoice++;
                }
                else if(item.tF_Enum == Backend.TF_enum.ToChoose)
                {
                    notchoose++;
                }
            }

            NewJudgeList.Add(truechoice);
            NewJudgeList.Add(falsechoice);
            NewJudgeList.Add(notchoose);

            TFPage.UpdateValues(NewJudgeList);
        }

        private void Teacher_RenewRegisterEvent(object sender, Backend.RenewEventArgs e)
        {
            var NewRegiList = new List<int>();
            int sign = 0;
            int notsign = 0;
            foreach (var item in e.AccountList.Values)
            {
                if(item.register_Enum==Backend.Register_enum.register)
                {
                    sign++;
                }
                else if(item.register_Enum == Backend.Register_enum.unregister)
                {
                    notsign++;
                }
            }

            NewRegiList.Add(sign);
            NewRegiList.Add(notsign);
            RegisterPage.UpdateValues(NewRegiList);
        }

        private void Teacher_RenewStudentListEvent(object sender, Backend.RenewEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                var StudentList = new List<Student>();
                foreach(var item in e.AccountList.Values)
                {
                    if(item.Name=="null")
                    {
                        StudentList.Add(new Student("undefined"));
                    }
                    else
                    {
                        StudentList.Add(new Student(item.Name));
                    }
                }

                personList.ItemsSource = StudentList;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.teacher.PublishRegister();
            RegisterPage = new SignCount(new List<int>{ 0, 0});
            RegisterPage.Show();
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.homePage;
        }

        private void ControlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (liveCondition == "stop")
            {
                ControlBtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/start.jpg"));
                ControlBtnContent.Text = "Start";
                liveCondition = "start";
                App.ffmpegthread.End();
            }
            else if (liveCondition == "start")
            {
                ControlBtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/stop.jpg"));
                ControlBtnContent.Text = "Stop";
                liveCondition = "stop";
                App.ffmpegthread.SetArguments(App.teacher.TeatherIP);
                App.ffmpegthread.Execute();
            }
        }

        private ListBoxItem CreateListBoxItemByName(string listBoxItemStr)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Style = FindResource("itemStyle") as Style;
            listBoxItem.Content = listBoxItemStr;
            return listBoxItem;
        }

        public void AddVoice(List<string> voiceDeviceNames)
        {
            foreach(string voiceDeviceName in voiceDeviceNames)
            {
                voice.Items.Add(CreateListBoxItemByName(voiceDeviceName));
            }
        }

        public void AddVideo(List<string> videoDeviceNames)
        {
            foreach (string videoDeviceName in videoDeviceNames)
            {
                video.Items.Add(CreateListBoxItemByName(videoDeviceName));
            }
        }

        private void SetTeachersName(string name)
        {
            teacherName.Text = name;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(questionkind.SelectedIndex==0)
            {
                App.teacher.PublishChoiceQuestion();
                ChoicePage = new SelectCount(new List<int> { 0, 0,0,0,0 });
                ChoicePage.Show();
            }
            else if(questionkind.SelectedIndex == 1)
            {
                App.teacher.PublishTFQuestion();
                TFPage = new JudgeCount(new List<int> { 0, 0,0});
                TFPage.Show();
            }
        }

        private void voice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)voice.SelectedItem;

            App.ffmpegthread.AudioDevice = (string)item.Content;
        }

        private void video_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)video.SelectedItem;

            App.ffmpegthread.VideoDevice = (string)item.Content;
        }
    }
}
