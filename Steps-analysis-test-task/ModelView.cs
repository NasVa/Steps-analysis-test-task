using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Steps_analysis_test_task.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Steps_analysis_test_task
{
    public class ModelView : INotifyPropertyChanged
    {
        public RelayCommand relayCommand { get; private set; }
        public DelegateCommand delegateCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<User> infoList;
        private IGetInfo fileInfoGetter;

        private User selectedUser;
        public User SelectedUser
        { 
            get { return this.selectedUser; }
            set
            {   
                this.selectedUser = value;
                this.OnPropertyChanged("SelectedUser");
            }
        }

        private PlotModel m_plotModel;
        public PlotModel plotModel
        {
            get { return m_plotModel; }
            set
            {
                if (value != this.m_plotModel)
                {
                    this.m_plotModel = value;
                    this.OnPropertyChanged("plotModel");
                }
            }
        }

        public void ExportData(object param)
        {
            JObject o = (JObject)JToken.FromObject(SelectedUser);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "(*.json)|*.json";
            if (dialog.ShowDialog() == true)
                using (StreamWriter file = File.CreateText(@dialog.FileName))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    o.WriteTo(writer);
                }
        }
        
        public ModelView(ObservableCollection<User> list, IGetInfo infoGetter)
        {
            relayCommand = new RelayCommand(OpenHandler);
            delegateCommand = new DelegateCommand(ExportData);
            this.infoList = list;
            fileInfoGetter = infoGetter;
            var signalAxis = new CategoryAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                MinimumMinorStep = 1,
                AbsoluteMinimum = 0,
                
            };
            var timeAxis = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                MinimumMajorStep = 1,
                AbsoluteMinimum = 0,
                
            };

            plotModel = new PlotModel();
            plotModel.Axes.Add(signalAxis);
            plotModel.Axes.Add(timeAxis);
            
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Dictionary<string, DataPoint> getHighestLowestPoints(List<DataPoint> ListOfPoints)
        {
            var nums = new Dictionary<string, DataPoint>() {
                 ["highest"] = new DataPoint(0,0),
                 ["lowest"] = new DataPoint(0, int.MaxValue),
            };
            foreach(var point in ListOfPoints)
            {
                if (point.Y > nums["highest"].Y)
                {
                    nums["highest"] = point;
                }
                if (point.Y < nums["lowest"].Y)
                {
                    nums["lowest"] = point;
                }
            }
            return nums;
        }
        public void updateGraphic(DataRowView row)
        {
            plotModel.Series.Clear();
            List<DataPoint> ListOfPoints = new List<DataPoint>();
            foreach(var steps in UsersList.getDoubleArraySteps(SelectedUser))
            {
                DataPoint newPoint = new DataPoint(ListOfPoints.Count + 1, steps/10000);
                ListOfPoints.Add(newPoint);
            }
            var series = new LineSeries
            {
                Color = OxyColors.Blue,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,

            };
            foreach (DataPoint p in ListOfPoints)
            {
                series.Points.Add(p);
            }
            Dictionary<string, DataPoint> nums = getHighestLowestPoints(ListOfPoints);
            var highestSeries = new LineSeries
            {
                Color = OxyColors.Yellow,
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.Dot,
            };
            
            highestSeries.Points.Add(nums["highest"]);

            var lowestSeries = new LineSeries
            {
                Color = OxyColors.HotPink,
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.Dot,
            };
            lowestSeries.Points.Add(nums["lowest"]);

            
            plotModel.Series.Add(series);
            plotModel.Series.Add(highestSeries);
            plotModel.Series.Add(lowestSeries);
            plotModel.InvalidatePlot(true);
        }

        public List<int> updateTableColors(DataGrid gd)
        {
            List<int> nums = new List<int>();
            int i = 0;
            foreach (User row in gd.Items)
            {
                if (UsersList.isDifferent20(SelectedUser.averageNumSteps, (int)(row.bestResult), (int)(row.worstResult))){
                    nums.Add(i);
                }
                i++;
            }
            return nums;
        }
        public void OpenHandler(object param)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.json)|*.json";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true)
            {
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
