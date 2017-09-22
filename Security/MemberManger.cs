using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Newtonsoft.Json;
using Crestron.SimplSharp.CrestronIO;

namespace ILiveLib.Security
{
    public class ILiveUserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class MemberManger
    {

        string userfile = Crestron.SimplSharp.CrestronIO.Directory.GetApplicationDirectory() + "\\user.dat";

        public bool CheckUser(string username, string password)
        {
            IList<ILiveUserInfo> userlist = this.GetUsers();
            if (userlist != null)
            {
                foreach (var item in userlist)
                {
                    if (item.UserName == username && item.Password == password)
                    {
                        return true;
                    }
                }
            }
        
            return false;
        }
        public void AddUser(string username, string password)
        {
            bool exits = false;
            IList<ILiveUserInfo> userlist = this.GetUsers();
            if (userlist == null)
            {
                return;
            }
            foreach (var item in userlist)
            {
                if (item.UserName == username )
                {
                    exits= true;
                }
            }
            if (!exits)
            {
                userlist.Add(new ILiveUserInfo() { UserName = username, Password = password });
                this.SaveUsers(userlist);
            }
        }
        public void ChangePass(string username,string password,string newpassword)
        {
            ILiveUserInfo user = null;
            IList<ILiveUserInfo> userlist = this.GetUsers();
            if (userlist==null)
            {
                return;
            }
            for (int i = 0; i < userlist.Count; i++)
            {
                if (userlist[i].UserName == username && userlist[i].Password == password)
                {
                    user = userlist[i];
                    
                }
            }
            
            if (user!=null)
            {
                userlist.Remove(user);
                //user.Password = newpassword;

                userlist.Add(new ILiveUserInfo() { UserName = username, Password = newpassword });
                this.SaveUsers(userlist);
            }
        }
        public IList<ILiveUserInfo> GetUsers()
        {
            IList<ILiveUserInfo> userlist = new List<ILiveUserInfo>();

            if (File.Exists(userfile))
            {
                string str = File.ReadToEnd(userfile, Encoding.GetEncoding(28591));
                userlist = JsonConvert.DeserializeObject<IList<ILiveUserInfo>>(str);
            }
            return userlist;

        }

        public void SaveUsers(IList<ILiveUserInfo> userlist)
        {
            if (File.Exists(userfile))
            {
                File.Delete(userfile);
            }
            string strLight = JsonConvert.SerializeObject(userlist);
            using (FileStream fileStream = new FileStream(userfile, FileMode.Create))
            {

                fileStream.Write(strLight, Encoding.GetEncoding(28591));
                fileStream.Flush();
            }
        }
    }
}