﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlameClassroom.Backend;
using Unosquare.FFME;

namespace FlameClassroom
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static StudentSide student;
        internal static TeacherSide teacher;
        internal static FFmpegThread ffmpegthread = new FFmpegThread();

        ~App()
        {
            ffmpegthread.End();
        }

    }
}
