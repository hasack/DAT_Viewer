using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace datviewer
{
    public partial class MainWindow : Window
    {

        public string? datPath;

        public string[]? header_array;

        public int column_number = 0;

        public static DataSet dataSet = new();

        public static DataTable dataTable = new();


        public MainWindow()
        {
            InitializeComponent();
 
        }

        private void ImportFile_Click(object sender, RoutedEventArgs e)
        {

            myGrid.ItemsSource = null;

            dataSet.Reset();

            dataTable.Reset();
            
            column_number = 0;

            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".dat";
            dialog.Filter = "Dat files |*.dat";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                path_box.Text = dialog.FileName;
                datPath = dialog.FileName;

                string[] rows = File.ReadAllLines(datPath, Encoding.UTF8);

                header_array = (rows[0].Replace("\u00FE", null)).Split("\u0014");

                dataSet.Tables.Add(dataTable);

                foreach (string header in header_array)
                {

                    dataTable.Columns.Add();
                }

                for (int i = 1; i < rows.Length; i++)
                {
                    string[] row_array = (rows[i].Replace("\u00FE", null)).Split("\u0014");
                    DataRow workRow = dataTable.NewRow();

                    for (int x = 0; x < row_array.Length; x++)
                    {
                        workRow[x] = row_array[x];
                    }

                    dataTable.Rows.Add(workRow);

                }

                myGrid.ItemsSource = dataTable.DefaultView;

                for (int i = 0; i < header_array.Length; i++)
                {

                    myGrid.Columns[i].Header = header_array[i];

                }


            }

        }

        private void Menu_Item_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}