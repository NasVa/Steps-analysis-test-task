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

        public string name { get; private set; }
        public List<dayInfo> info;
        public int averageNumSteps { get; private set; }
        public int bestResult { get; private set; }
        public int worstResult { get; private set; }

        public User(string name)
        {
            this.name = name;
            this.info = new List<dayInfo>();
            this.averageNumSteps = 0;
            this.bestResult = 0;
            this.worstResult = 0;
        }
        
        public void calculateCurrentAnalysis()
        {
            List<int> sum = new List<int>();
            foreach(var inf in info)
            {
                sum.Add(inf.steps);
            }
            this.averageNumSteps = sum.Sum()/sum.Count;
            this.bestResult = sum.Max();
            this.worstResult = sum.Min();
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
