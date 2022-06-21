using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //private ObservableCollection<User> infoList;
        private ObservableCollection<User> infoList;
        
        private IGetInfo fileInfoGetter;

        

        public InfoReader(ObservableCollection<User> list, IGetInfo infoGetter)
        {
            relayCommand = new RelayCommand(OpenHandler);
            
            this.infoList = list;
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
            dialog.Filter = "(*.json)|*.json";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true)
            {
                foreach (var inf in infoList)
                {
                    inf.calculateCurrentAnalysis();
                }
                infoList.Clear();
                ObservableCollection<User> list = fileInfoGetter.GetInfoFromFile(dialog.FileNames);
                foreach (var i in list)
                {
                    infoList.Add(i);
                }
                foreach (var inf in infoList)
                {
                    inf.calculateCurrentAnalysis();
                }
            }
        }
    }
}
