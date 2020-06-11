﻿using System;
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
    /// IPInput.xaml 的交互逻辑
    /// </summary>
    public partial class IPInput : Page
    {
        public MainWindow parentWindow { set; get; }
        public IPInput()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.signInPage;
        }

        private void TeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            Height = 400;
            studentSubmit.Visibility = Visibility.Hidden;
            teacherSubmit.Visibility = Visibility.Visible;
        }

        private void StudentBtn_Click(object sender, RoutedEventArgs e)
        {
            Height = 400;
            teacherSubmit.Visibility = Visibility.Hidden;
            studentSubmit.Visibility = Visibility.Visible;
        }
    }
}