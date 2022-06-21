using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task.Model
{
    public static class UsersList
    {

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
