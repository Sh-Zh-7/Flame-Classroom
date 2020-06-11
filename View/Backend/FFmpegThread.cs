using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FlameClassroom.Backend
{
    class FFmpegThread
    {
        private Process _ffmpegProcess;
        private List<string> _VideoList = new List<string>();
        public List<string> VideoList
        {
            get => _VideoList;
        }
        private List<string> _AudioList = new List<string>();
        public List<string> AudioList
        {
            get => _AudioList;
        }
        private List<string> _VideoDecoderList = new List<string>();
        public List<string> VideoDecoderList
        {
            get => _VideoDecoderList;
        }

        private List<string> _BitRateList = new List<string>();
        public List<string> BitRateList
        {
            get => _BitRateList;
        }
        private List<string> _FrameRateList = new List<string>();
        public List<string> FrameRateList
        {
            get => _FrameRateList;
        }
        private string _IPAddress;
        private string _Address;
        private string _FileName;

        private string _VideoDevice;
        private string _AudioDevice;
        private string _VideoDecoder;

        private string _Argument = "";
        public FFmpegThread()
        {
            InitDownload();
            InitFileName();
            GetDevice();
            SetFixedParameters();
        }

        public void InitDownload()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg\ffmpeg-4.2.1-win64-shared\bin\ffmpeg.exe"))
            {
                //Console.WriteLine("File doesn't exist!");
                var downloadClient = new WebClient();
                downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);
                downloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                downloadClient.DownloadFileAsync(new Uri("https://ffmpeg.zeranoe.com/builds/win64/shared/ffmpeg-4.2.1-win64-shared.zip"), Directory.GetCurrentDirectory() + @"\ffmpeg.zip");
            }

            else
            {
            }
        }

        public void InitFileName()
        {
            _FileName = Directory.GetCurrentDirectory() + @"\ffmpeg\ffmpeg-4.2.1-win64-shared\bin\ffmpeg.exe";
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            //Console.WriteLine(e.ProgressPercentage);
        }

        private void DownloadFileCallback(object sender, AsyncCompletedEventArgs e)
        {
            ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + @"\ffmpeg.zip", Directory.GetCurrentDirectory() + @"\ffmpeg");
            File.Delete(Directory.GetCurrentDirectory() + @"\ffmpeg.zip");
        }
        public void GetDevice()
        {
            var CheckDeviceProcess = new Process();
            CheckDeviceProcess.StartInfo.FileName = _FileName;
            CheckDeviceProcess.StartInfo.Arguments = "-list_devices true -f dshow -i dummy";
            CheckDeviceProcess.StartInfo.UseShellExecute = false;
            CheckDeviceProcess.StartInfo.RedirectStandardError = true;
            CheckDeviceProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;

            CheckDeviceProcess.Start();

            var error = CheckDeviceProcess.StandardError.ReadToEnd();

            CheckDeviceProcess.Close();

            Regex VideoPattern = new Regex(@"\[dshow\s*@\s*\w+\]\s+""(.+)""");
            Regex AudioPattern = new Regex(@"\[dshow\s*@\s*\w+\]\s+""(.+)""");

            string[] SplitString = error.Split(new string[] { "DirectShow video devices (some may be both video and audio devices)", "DirectShow audio devices" }, StringSplitOptions.RemoveEmptyEntries);

            MatchCollection VideoCollection = VideoPattern.Matches(SplitString[1]);
            MatchCollection AudioCollection = AudioPattern.Matches(SplitString[2]);
            _VideoList.Add("desktop");
            foreach (Match item in VideoCollection)
            {
                _VideoList.Add(item.Groups[1].ToString());
            }
            foreach (Match item in AudioCollection)
            {
                _AudioList.Add(item.Groups[1].ToString());
            }
        }

        public void SetFixedParameters()
        {
            _VideoDecoderList.Add("h264");
            _VideoDecoderList.Add("libx265");
            _VideoDecoderList.Add("libx264");
            _FrameRateList.Add("10");
            _FrameRateList.Add("20");
            _FrameRateList.Add("30");
            _BitRateList.Add("400k");
            _BitRateList.Add("600k");
            _BitRateList.Add("800k");
        }

        public void SetArguments(string videodevice, string audiodevice, string bitrate, string framerate, string videodecoder, string IPAddress)
        {
            _Argument.Remove(0, _Argument.Length);
            if (videodevice == "desktop")
            {
                _Argument += "-f gdigrab -i desktop ";
                //命令行中的空格一律放在末尾
            }
            else
            {
                _Argument += "-f dshow -i video=\"" + videodevice + "\"" + " ";
            }
            _Argument += "-f  dshow -i audio=\"" + audiodevice + "\"" + " ";
            _Argument += "-r " + framerate + " ";
            _Argument += "-vb " + bitrate + " ";
            _Argument += "-vcodec " + videodecoder + " ";
            _Argument += "-preset:v ultrafast -tune:v zerolatency -f flv -listen 1 ";
            _Argument += "rtmp://" + IPAddress + "/live/Fire-Classroom";
            _Address = "rtmp://" + IPAddress + "/live/Fire-Classroom";
        }
        public void Execute()
        {
            _ffmpegProcess = new Process();
            _ffmpegProcess.StartInfo.FileName = _FileName;
            _ffmpegProcess.StartInfo.Arguments = _Argument;
            _ffmpegProcess.StartInfo.CreateNoWindow = true;
            _ffmpegProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _ffmpegProcess.Start();
        }

        public void End()
        {
            _ffmpegProcess.Kill();
        }

        public void test()
        {
            foreach (var item in _VideoList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------");
            foreach (var item in _AudioList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
