using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task
{
    public class FileGetInfo : IGetInfo
    {
        //private FileGetInfo file;
        private ObservableCollection<User> userList;

        public ObservableCollection<User> GetInfoFromFile(string[] fileNames)
        {
            userList = new ObservableCollection<User>();
            foreach (string fileName in fileNames)
            {
                getInfo(JToken.Parse(File.ReadAllText(@fileName)), Int32.Parse(fileName.Substring(fileName.LastIndexOf('y') + 1).Split('.')[0]));
            }
            return userList;
        }
        
        public User findUserByName(string name)
        {
            if (userList.Count > 0)
            {
                foreach (User user in userList)
                    if (user.name == name)
                        return user;
            }
            return null;
        }
        public void getInfo(JToken obj, int day)
        {
            JToken cur = obj.First;
            while(cur != obj.Last)
            {
                var json = cur.ToString();
                User user = findUserByName(JObject.Parse(json)["User"].ToString());
                if (user == null)
                {
                    user = new User(JObject.Parse(json)["User"].ToString());
                }
                user.addDayinfo(day,
                    Int32.Parse(JObject.Parse(json)["Rank"].ToString()),
                    JObject.Parse(json)["Status"].ToString(),
                    Int32.Parse(JObject.Parse(json)["Steps"].ToString()));
                userList.Add(user);
                cur = cur.Next;
            }
                    
        }
    }
}
