using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FlameClassroom.Backend
{
    class TeacherSide
    {
        private Socket ListenSocket = null;
        private Thread CommuThread = null;

        public string TeatherIP { set; get; }

        public string Name { set; get; }
        public Dictionary<string, ConnectionState> ConnectionList = new Dictionary<string, ConnectionState>();

        public Dictionary<string, Account> AccountList = new Dictionary<string, Account>();



        public TeacherSide(string IPstring)
        {
            TeatherIP = IPstring;
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse(IPstring);
            IPEndPoint point = new IPEndPoint(address, 3230);

            ListenSocket.Bind(point);
            ListenSocket.Listen(20);

            Thread WatchThread = new Thread(WatchConnection);
            WatchThread.IsBackground = true;
            WatchThread.Start();
            InitStudentInfo();
            //Console.WriteLine("Start watch");
            //Console.WriteLine("-----");
        }

        public void WatchConnection()
        {
            Socket CommuConnection;
            while (true)//TODO 修改whlie true 的条件 判断该在何时结束
            {
                try
                {
                    CommuConnection = ListenSocket.Accept();
                    ConnectionList.Add(CommuConnection.RemoteEndPoint.ToString(), new ConnectionState(CommuConnection));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }

                IPAddress ClientIP = (CommuConnection.RemoteEndPoint as IPEndPoint).Address;
                int ClientPort = (CommuConnection.RemoteEndPoint as IPEndPoint).Port;

                string RemoteEndPoint = CommuConnection.RemoteEndPoint.ToString();

                ParameterizedThreadStart pts = new ParameterizedThreadStart(this.Receive);
                CommuThread = new Thread(pts);
                CommuThread.IsBackground = true;
                CommuThread.Start(CommuConnection);
            }

        }

        public void Receive(object CommuConnection)
        {
            Socket ServerSocket = CommuConnection as Socket;

            ServerSocket.Send(Encoding.UTF8.GetBytes("Server Starts"));
            //Console.WriteLine("####");
            //Console.WriteLine("Server");

            while (true)
            {
                byte[] ReceiveMsg = new byte[1024 * 1024];
                try
                {
                    int length = ServerSocket.Receive(ReceiveMsg);

                    string ReceiveStr = Encoding.UTF8.GetString(ReceiveMsg, 0, length);

                    //Console.WriteLine("------------");
                    //Console.WriteLine(ReceiveStr);

                    Route(ReceiveStr, ServerSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Route(string Message, Socket CommuConnection)
        {
            string[] StrArr = Message.Split(new char[] { ' ' });
            if (StrArr[0] == "Login")
            {
                Login(StrArr, CommuConnection);
            }
            else if (StrArr[0] == "CreateAcount")
            {
                CreateAcount(StrArr, CommuConnection);
            }
            else if (StrArr[0] == "ChangeInfo")
            {
                ChangeInfo(StrArr, CommuConnection);
            }
            else if (StrArr[0] == "Registering")
            {
                //ChangeRegi();
            }
            else if (StrArr[0] == "TF")
            {

            }
            else if (StrArr[0] == "CHOICE")
            {

            }
        }

        public void CreateAcount(string[] StrArr, Socket CommuConnection)
        {
            string UserName = StrArr[1];
            string Password = StrArr[2];
            if (AccountList.ContainsKey(UserName))
            {
                CommuConnection.Send(Encoding.UTF8.GetBytes("CreateAcount_Exist"));
            }
            else
            {
                CommuConnection.Send(Encoding.UTF8.GetBytes("CreateAcount_Success"));
                AccountList.Add(UserName, new Account(UserName, Password));
            }
        }

        public void Login(string[] StrArr, Socket CommuConnection)
        {
            string UserName = StrArr[1];
            string Password = StrArr[2];
            if (!AccountList.ContainsKey(UserName))
            {
                CommuConnection.Send(Encoding.UTF8.GetBytes("Login_NotExist"));
            }
            else if (Password != AccountList[UserName].Password)
            {
                CommuConnection.Send(Encoding.UTF8.GetBytes("Login_WrongPassword"));
            }
            else
            {
                RenewStudentList(new RenewEventArgs(AccountList));
                CommuConnection.Send(Encoding.UTF8.GetBytes("Login_Success" + " " + JsonConvert.SerializeObject(AccountList[UserName]) + " " + Name));
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].Login = true;
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account = AccountList[UserName];
            }
        }

        public void ChangeInfo(string[] StrArr, Socket CommuConnection)
        {
            string School = StrArr[1];
            string ID = StrArr[2];
            string Name = StrArr[3];
            string Description = StrArr[4];
            ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.ChangeInfo(School, ID, Name, Description);
            RenewStudentList(new RenewEventArgs(AccountList));
        }

        public void ChangeRegi(string[] StrArr, Socket CommuConnection)
        {
            ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.register_Enum = Register_enum.register;
            RenewRegister(new RenewEventArgs(AccountList));
        }
        public void ChangeTF(string[] StrArr, Socket CommuConnection)
        {
            if (StrArr[1] == "true")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.tF_Enum = TF_enum.T;
            }
            else if (StrArr[1] == "false")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.tF_Enum = TF_enum.F;
            }
            RenewTF(new RenewEventArgs(AccountList));

        }
        public void ChangeChoice(string[] StrArr, Socket CommuConnection)
        {
            if (StrArr[1] == "A")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.choice_Enum = Choice_enum.A;
            }
            else if (StrArr[1] == "B")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.choice_Enum = Choice_enum.B;
            }
            else if (StrArr[1] == "C")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.choice_Enum = Choice_enum.C;
            }
            else if (StrArr[1] == "D")
            {
                ConnectionList[CommuConnection.RemoteEndPoint.ToString()].account.choice_Enum = Choice_enum.D;
            }
            RenewChoice(new RenewEventArgs(AccountList));
        }

        public void PublishRegister()
        {
            foreach (ConnectionState item in ConnectionList.Values)
            {
                item.Connection.Send(Encoding.UTF8.GetBytes("Register"));
                item.account.register_Enum = Register_enum.unregister;
            }
        }

        public void PublishTFQuestion()
        {
            foreach (ConnectionState item in ConnectionList.Values)
            {
                item.Connection.Send(Encoding.UTF8.GetBytes("TF"));
                item.account.tF_Enum = TF_enum.ToChoose;
            }
        }

        public void PublishChoiceQuestion()
        {
            foreach (ConnectionState item in ConnectionList.Values)
            {
                item.Connection.Send(Encoding.UTF8.GetBytes("CHOICE"));
                item.account.choice_Enum = Choice_enum.ToChoose;
            }
        }

        public event EventHandler<RenewEventArgs> RenewStudentListEvent;

        public void RenewStudentList(RenewEventArgs e)
        {
            if (RenewStudentListEvent != null)
            {
                RenewStudentListEvent(this, e);
            }
        }

        public event EventHandler<RenewEventArgs> RenewRegisterEvent;

        public void RenewRegister(RenewEventArgs e)
        {
            if (RenewRegisterEvent != null)
            {
                RenewRegisterEvent(this, e);
            }
        }

        public event EventHandler<RenewEventArgs> RenewTFEvent;

        public void RenewTF(RenewEventArgs e)
        {
            if (RenewTFEvent != null)
            {
                RenewTFEvent(this, e);
            }
        }

        public event EventHandler<RenewEventArgs> RenewChoiceEvent;

        public void RenewChoice(RenewEventArgs e)
        {
            if (RenewChoiceEvent != null)
            {
                RenewChoiceEvent(this, e);
            }
        }

        public void InitStudentInfo()
        {
            string InfoPath = Directory.GetCurrentDirectory() + @"\StudentInfo.json";
            if (!File.Exists(InfoPath))
            {
                FileStream fs = new FileStream(InfoPath, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
                AccountList.Clear();
            }
            else if (File.ReadAllText(InfoPath) == "null")
            {
                AccountList.Clear();
            }
            else if (File.ReadAllText(InfoPath) == null)
            {
                AccountList.Clear();
            }
            else
            {
                AccountList = JsonConvert.DeserializeObject<Dictionary<string, Account>>(File.ReadAllText(InfoPath));
            }
        }//TODO:最后将序列化字符串写入到磁盘；

        public void RenewStudentInfo()
        {
            string InfoPath = Directory.GetCurrentDirectory() + @"\StudentInfo.json";
            File.WriteAllText(InfoPath, JsonConvert.SerializeObject(AccountList));
        }

        ~TeacherSide()
        {
            RenewStudentInfo();
        }
    }

    class RenewEventArgs : EventArgs
    {
        public Dictionary<string, Account> AccountList = new Dictionary<string, Account>();

        public RenewEventArgs(Dictionary<string, Account> AccountList)
        {
            this.AccountList = AccountList;
        }
    }


}
