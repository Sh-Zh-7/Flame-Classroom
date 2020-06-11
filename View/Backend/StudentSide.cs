using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace FlameClassroom.Backend
{
    class StudentSide
    {
        private Thread ConnectThread = null;
        private Socket ConnectSocket = null;

        public Account account = null;
        public StudentSide(string IPstring)
        {
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
                    Console.WriteLine(ex.Message);
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

        public void Route(string Message)
        {
            string[] StrArr = Message.Split(new char[] { ' '});
            if (StrArr[0] == "CreateAcount_Exist")
            {

            }
            else if (StrArr[0] == "CreateAcount_Success")
            {

            }
            else if (StrArr[0] == "Login_NotExist")
            {

            }
            else if (StrArr[0] == "Login_WrongPassword")
            {

            }
            else if (StrArr[0] == "Login_Success")
            {
                account = JsonConvert.DeserializeObject<Account>(StrArr[1]);
            }
            else if (StrArr[0] == "Register")
            {

            }

        }
    }
}
