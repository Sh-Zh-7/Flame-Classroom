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
    class StudentSide
    {
        private Thread ConnectThread = null;
        private Socket ConnectSocket = null;

        public Account account = null;

        public string TeatherIP { set; get; }
        public string TeatherName { set; get; }
        public StudentSide(string IPstring)
        {
            TeatherIP = IPstring;

            ConnectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse(IPstring);
            IPEndPoint point = new IPEndPoint(address, 3230);

            try
            {
                ConnectSocket.Connect(point);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ConnectThread = new Thread(Receive);
            ConnectThread.IsBackground = true;
            ConnectThread.Start();
        }

        public void Receive()
        {
            while (true)
            {
                byte[] ReceiveMsg = new byte[1024 * 1024];
                try
                {
                    int length = ConnectSocket.Receive(ReceiveMsg);

                    string ReceiveStr = Encoding.UTF8.GetString(ReceiveMsg, 0, length);

                    //Console.WriteLine("---------------");
                    //Console.WriteLine(ReceiveStr);

                    Route(ReceiveStr);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }

        public void CreatAccount(string UserName, string password)
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("CreateAcount" + " " + UserName + " " + password));
        }

        public void Login(string UserName, string password)
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("Login" + " " + UserName + " " + password));
        }

        public void ChangeInfo(string School, string ID, string Name, string Description)
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("ChangeInfo" + " " + School + " " + ID + " " + Name + " " + Description));
            account.ChangeInfo(School, ID, Name, Description);
            //TODO:在页面中渲染
        }

        public void RegisterSend()
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("Registering"));
        }
        public void TFsend(string choice)
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("TF" + " " + choice));
        }

        public void ChoiceSend(string choice)
        {
            ConnectSocket.Send(Encoding.UTF8.GetBytes("CHOICE" + " " + choice));
        }
        public void Route(string Message)
        {
            string[] StrArr = Message.Split(new char[] { ' ' });
            if (StrArr[0] == "CreateAcount_Exist")
            {
                AccountExist(new EventArgs());
            }
            else if (StrArr[0] == "CreateAcount_Success")
            {
                AccountCreate(new EventArgs());
            }
            else if (StrArr[0] == "Login_NotExist")
            {
                LoginNotExist(new EventArgs());
            }
            else if (StrArr[0] == "Login_WrongPassword")
            {
                LoginWrongPassword(new EventArgs());
            }
            else if (StrArr[0] == "Login_Success")
            {
                account = JsonConvert.DeserializeObject<Account>(StrArr[1]);
                TeatherName = StrArr[2];
                LoginSuccess(new EventArgs());
            }
            else if (StrArr[0] == "Register")
            {
                Register(new EventArgs());
            }
            else if (StrArr[0] == "TF")
            {
                TF(new EventArgs());
            }
            else if (StrArr[0] == "CHOICE")
            {
                Choice(new EventArgs());
            }
        }

        public event EventHandler AccountExistEvent;
        public void AccountExist(EventArgs e)
        {
            if (AccountExistEvent != null)
            {
                AccountExistEvent(this, e);
            }
        }

        public event EventHandler AccountCreateEvent;
        public void AccountCreate(EventArgs e)
        {
            if (AccountCreateEvent != null)
            {
                AccountCreateEvent(this, e);
            }
        }

        public event EventHandler LoginNotExistEvent;
        public void LoginNotExist(EventArgs e)
        {
            if (LoginNotExistEvent != null)
            {
                LoginNotExistEvent(this, e);
            }
        }

        public event EventHandler LoginWrongPasswordEvent;
        public void LoginWrongPassword(EventArgs e)
        {
            if (LoginWrongPasswordEvent != null)
            {
                LoginWrongPasswordEvent(this, e);
            }
        }

        public event EventHandler LoginSuccessEvent;
        public void LoginSuccess(EventArgs e)
        {
            if (LoginSuccessEvent != null)
            {
                LoginSuccessEvent(this, e);
            }
        }

        public event EventHandler RegisterEvent;
        public void Register(EventArgs e)
        {
            if (RegisterEvent != null)
            {
                RegisterEvent(this, e);
            }
        }

        public event EventHandler TFEvent;

        public void TF(EventArgs e)
        {
            if (TFEvent != null)
            {
                TFEvent(this, e);
            }
        }

        public event EventHandler ChoiceEvent;
        public void Choice(EventArgs e)
        {
            if (ChoiceEvent != null)
            {
                ChoiceEvent(this, e);
            }
        }

    }
}
