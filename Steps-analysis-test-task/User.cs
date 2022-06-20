using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task
{
    public class User
    {

        public struct dayInfo {
            public int day;
            public int rank;
            public string status;
            public int steps;
        }

        public string name;
        public List<dayInfo> info;

        public User(string name)
        {
            this.name = name;
            this.info = new List<dayInfo>();
        }
        
        public void addDayinfo(int day, int rank, string status, int steps)
        {
            dayInfo newInfo = new dayInfo();
            newInfo.day = day;
            newInfo.rank = rank;
            newInfo.status = status;
            newInfo.steps = steps;
            this.info.Add(newInfo);
        }
    }
}
