using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task
{
    public interface IGetInfo
    {
        public List<User> GetInfoFromFile(string fileName);
    }
}
