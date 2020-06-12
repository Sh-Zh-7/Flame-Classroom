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

        public bool Init { set; get; }

        public Register_enum register_Enum;
        public TF_enum tF_Enum;
        public Choice_enum choice_Enum;
        public Account(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.Init = true;
            this.Name = "null";
        }
        [JsonConstructor]
        public Account(string UserName, string Password, string School, string ID, string Name, string Description, bool Init)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.School = School;
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Init = Init;
        }
        public void ChangeInfo(string School, string ID, string Name, string Description)
        {
            this.School = School;
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Init = false;
        }
    }

    public enum Register_enum
    {
        register, unregister
    };
    public enum TF_enum
    {
        T, F, ToChoose
    };
    public enum Choice_enum
    {
        A, B, C, D, ToChoose
    }
}
