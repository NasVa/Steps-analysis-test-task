using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Steps_analysis_test_task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        InfoReader infoReader;
        public MainWindow()
        {
            InitializeComponent();
            var infoList = new ObservableCollection<User>();
            InfoGrid.ItemsSource = infoList;
            infoReader = new InfoReader(infoList, new FileGetInfo());
            DataContext = infoReader;


        }

        private void InfoGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row = gd.SelectedItem as DataRowView;
            
        }

        private void InfoGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row = gd.SelectedItem as DataRowView;
            infoReader.updateGraphic(row);
            List<int> nums = infoReader.updateTableColors(gd);
            for (int i = 0; i < gd.Items.Count; i++)
            {
                DataGridRow dataGridRow = (DataGridRow)gd.ItemContainerGenerator.ContainerFromIndex(i);
                dataGridRow.Background = Brushes.White;
            }
            for (int i = 0; i < nums.Count; i++)
            {
                DataGridRow dataGridRow = (DataGridRow)gd.ItemContainerGenerator.ContainerFromIndex(nums[i]);
                dataGridRow.Background = Brushes.PaleGreen;
            }
        }
    }
}
