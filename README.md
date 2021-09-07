# How to perform Sorting and Column Selection based on single and double click the mouse on the header based on click count in WPF DataGrid (SfDataGrid)?

## About the sample

This example illustrates how to perform Sorting and Column Selection based on single and double click the mouse on the header based on click count in WPF DataGrid (SfDataGrid).

By default, [WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) (SfDataGrid) will sort the column when double clicking on the header cell of the column if [SfDataGrid.SortClickAction](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.SfGridBase.html#Syncfusion_UI_Xaml_Grid_SfGridBase_SortClickAction) is set as `DoubleClick`. You can customize the DataGrid to perform sorting on double click and perform column selection on single click on the header cell by handling the mouse events.

```C#

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

```

KB article - [How to perform Sorting and Column Selection based on single and double click the mouse on the header based on click count in WPF DataGrid (SfDataGrid)?](https://www.syncfusion.com/kb/12618/how-to-perform-sorting-and-column-selection-based-on-single-and-double-click-the-mouse-on)

## Requirements to run the demo 

Visual Studio 2015 and above versions.
