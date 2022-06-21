using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task.Model
{
    public static class UsersList
    {
        public static bool isDifferent20(int num, int best, int worst, int percents = 20)
        {
            return best > (int)(num + num * percents / 100) || worst < (int)(num - num * percents / 100);
               // || worst < (int)(num - num * percents / 100) || worst > (int)(num - num * percents / 100);
        }

        public static double[] getDoubleArraySteps(User user)
        {
            double[] steps = new double[user.info.Count];
            for(var i = 0; i < user.info.Count; i++)
            {
                steps[i] = user.info[i].steps;
            }
            return steps;
        }
    }
}
