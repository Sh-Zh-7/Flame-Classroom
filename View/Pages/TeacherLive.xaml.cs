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
        public string liveCondition = "stop";
        public TeacherLive()
        {
            InitializeComponent();

        }
        public void Init()
        {
            App.teacher.RenewStudentListEvent += Teacher_RenewStudentListEvent;
            SetTeachersName(App.teacher.Name);
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
            System.Windows.MessageBox.Show("Fuck");
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
            }
            else if (liveCondition == "start")
            {
                ControlBtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/stop.jpg"));
                ControlBtnContent.Text = "Stop";
                liveCondition = "stop";
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
    }
}
