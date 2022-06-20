using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task
{
    public interface IGetInfo
    {
        public ObservableCollection<User> GetInfoFromFile(string[] fileNames);
    }
}
