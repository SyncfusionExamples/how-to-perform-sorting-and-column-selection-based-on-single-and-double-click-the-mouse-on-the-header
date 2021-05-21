using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
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

namespace SfDataGrid_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ClickCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.dataGrid.Loaded += OnDataGrid_Loaded;
        }

        private void OnDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var visualContainer = this.dataGrid.GetVisualContainer();
            visualContainer.MouseLeftButtonDown += VisualContainer_MouseLeftButtonDown;
            visualContainer.MouseLeftButtonUp += visualContainer_MouseLeftButtonUp;
        }

        private void VisualContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClickCount = e.ClickCount;
        }

        void visualContainer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HandleClickAction(e);
        }

        private async void HandleClickAction(MouseButtonEventArgs e)
        {
            await Task.Delay(200);
            if (ClickCount == 1)
            {
                var visualContainer = this.dataGrid.GetVisualContainer();
                var rowcolumnindex = visualContainer.PointToCellRowColumnIndex(e.GetPosition(visualContainer));
                var columnindex = this.dataGrid.ResolveToGridVisibleColumnIndex(rowcolumnindex.ColumnIndex);
                if (columnindex < 0)
                    return;

                //Return if it is not HeaderRow
                if (this.dataGrid.GetHeaderIndex() != rowcolumnindex.RowIndex)
                    return;

                var firstrowdata = this.dataGrid.GetRecordAtRowIndex(dataGrid.GetFirstRowIndex());
                //Get the record of LastRowIndex 
                var lastrowdata = this.dataGrid.GetRecordAtRowIndex(dataGrid.GetLastRowIndex());
                //Get the column of particular index
                var column = this.dataGrid.Columns[columnindex];

                if (firstrowdata == null || lastrowdata == null)
                    return;
                //Select the column
                this.dataGrid.SelectCells(firstrowdata, column, lastrowdata, column);
            }
        }
    }
}
