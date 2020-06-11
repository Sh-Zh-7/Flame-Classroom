using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace FlameClassroom.Backend
{
    class ConnectionState
    {
        public Socket Connection;

        public bool Regisiter;
        public bool Login;
        public Account account;

        public ConnectionState(Socket Connection)
        {
            this.Regisiter = false;
            this.Login = false;
            this.Connection = Connection;
            account = null;
        }
    }

    class Account
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string School { set; get; }
        public string ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Account(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public void ChangeInfo(string School, string ID, string Name, string Description)
        {
            this.School = School;
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
