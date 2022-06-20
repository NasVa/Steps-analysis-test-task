using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Steps_analysis_test_task
{
    public class InfoReader : INotifyPropertyChanged
    {
        public RelayCommand relayCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private List<User> infoList;
        private IGetInfo fileInfoGetter;

        public InfoReader(IGetInfo infoGetter)
        {
            relayCommand = new RelayCommand(OpenHandler);
            this.infoList = new List<User>();
            fileInfoGetter = infoGetter;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void OpenHandler(object param)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.json";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true)
            {
                foreach (string fileName in dialog.FileNames)
                    infoList.AddRange(fileInfoGetter.GetInfoFromFile(fileName));
                OnPropertyChanged();
            }
        }
    }
}
