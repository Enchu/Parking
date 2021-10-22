using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office;
using System.IO;
using Microsoft.Win32;
using System.Data;

namespace Parking.Windows
{
    public partial class StatistWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        string name = "";
        public StatistWindow(string name1)
        {
            InitializeComponent();
            name = name1;
            Load();
        }
        public void Load()
        {
            try
            {
                if (name != "")
                {
                    DataTable dt = ClassSpravka.LoadVV("SELECT * FROM staff Where `email`='" + name + "'");
                    name = dt.Rows[0].ItemArray[2].ToString() + " " + dt.Rows[0].ItemArray[3].ToString() + " " + dt.Rows[0].ItemArray[4].ToString();
                }
                else
                {
                    DataTable dt = ClassSpravka.LoadVV("SELECT * FROM staff Where `email`='" + ClassSpravka.NameF + "'");
                    ClassSpravka.NameF = dt.Rows[0].ItemArray[2].ToString() + " " + dt.Rows[0].ItemArray[3].ToString() + " " + dt.Rows[0].ItemArray[4].ToString();
                }
            }
            catch { }
        }
        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(0, 10);
                }
            }
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var c = SeriesCollection.Count > 0 ? SeriesCollection[0].Values.Count : 5;

            var vals = new ChartValues<ObservableValue>();

            for (var i = 0; i < c; i++)
            {
                vals.Add(new ObservableValue(r.Next(0, 10)));
            }

            SeriesCollection.Add(new PieSeries
            {
                Values = vals
            });
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection.RemoveAt(0);
        }

        private void AddValueOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var series in SeriesCollection)
            {
                series.Values.Add(new ObservableValue(r.Next(0, 10)));
            }
        }

        private void RemoveValueOnClick(object sender, RoutedEventArgs e)
        {
            foreach (var series in SeriesCollection)
            {
                if (series.Values.Count > 0)
                    series.Values.RemoveAt(0);
            }
        }

        private void RestartOnClick(object sender, RoutedEventArgs e)
        {
            Chart.Update(true, true);
        }
        int countsi = 0;

        void FindAndReplace(object FindText, object ReplaceText, Microsoft.Office.Interop.Word.Application App)
        {
            App.Selection.Find.Execute(ref FindText, true, true, false, false, false, true, false, 1, ref ReplaceText, 2, false, false, false, false);
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            ManagerWindow fm = new ManagerWindow(name);
            fm.Show();
            this.Close();
        }

        private void ButtonClickCB(object sender, RoutedEventArgs e)
        {
            try
            {
                SeriesCollection = new SeriesCollection();
                DataTable DT = ClassSpravka.LoadWindow("SELECT `customer`,`building`,SUM(`price`),COUNT(`customer`) FROM `parking` Where `datestart`>'" + DPOt.SelectedDate.Value.Year + "." + DPOt.SelectedDate.Value.Month + "." + DPOt.SelectedDate.Value.Day + "' and `dataend` <'" + DPDo.SelectedDate.Value.Year + "." + DPDo.SelectedDate.Value.Month + "." + DPDo.SelectedDate.Value.Day + "' GROUP BY building");
                foreach (DataRow item in DT.Rows)
                {
                    SeriesCollection.Add(new PieSeries()
                    {
                        Title = item[0].ToString() + " - " + item[1].ToString() + " - " + item[2].ToString()+ " Рублей",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item[3].ToString())) },
                        DataLabels = true
                    });
                    DataContext = this;
                    /*MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        //if (read[0].ToString() == CBModel.Text)
                    }*/
                }
            }
            catch { }
            finally { }
        }

        private void ButtonClickOtchet(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Word.Application App = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document Doc = new Word.Document();
            try
            {
                DataTable DT = ClassSpravka.LoadVV("SELECT `customer` as `Покупатель`,`building` as `Строение`,SUM(`price`) as `Цена (Руб)` FROM `parking` Where `datestart`>'" + DPOt.SelectedDate.Value.Year + "." + DPOt.SelectedDate.Value.Month + "." + DPOt.SelectedDate.Value.Day + "' and `dataend` <'" + DPDo.SelectedDate.Value.Year + "." + DPDo.SelectedDate.Value.Month + "." + DPDo.SelectedDate.Value.Day + "' GROUP BY building");
                DataTable sum = ClassSpravka.LoadVV("SELECT COUNT(`customer`) as `Покупатель`,SUM(`price`) as `Сумма (Руб)` FROM `parking` Where `datestart`>'" + DPOt.SelectedDate.Value.Year + "." + DPOt.SelectedDate.Value.Month + "." + DPOt.SelectedDate.Value.Day + "' and `dataend` <'" + DPDo.SelectedDate.Value.Year + "." + DPDo.SelectedDate.Value.Month + "." + DPDo.SelectedDate.Value.Day + "'");
                Doc = App.Documents.Add(System.IO.Directory.GetCurrentDirectory() + @"\shablon.docx");
                Doc.Activate();
                if (name != "")
                {
                    FindAndReplace("name", name, App);
                }
                else { FindAndReplace("name", ClassSpravka.NameF, App); }
                FindAndReplace("date1", DPOt.SelectedDate.Value, App);
                FindAndReplace("date2", DPDo.SelectedDate.Value, App);
                FindAndReplace("namett", sum.Rows[0].ItemArray[0].ToString(), App);
                FindAndReplace("priceall", sum.Rows[0].ItemArray[1].ToString(), App);
                FindAndReplace("dataS", DateTime.Now.ToString(), App);
                Microsoft.Office.Interop.Word.Table Tab = Doc.Tables[1];
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    Tab.Rows.Add();
                }
                for (int i = 0; i < DT.Columns.Count - 1; i++)
                {
                    Tab.Columns.Add();
                }
                Tab.Borders.Enable = 1;
                int Row = 0, Colums = 0, RowS = 0, Count = 0;
                foreach (Microsoft.Office.Interop.Word.Row row in Tab.Rows)
                {
                    object[] sa = DT.Rows[RowS].ItemArray;
                    Colums = 0;
                    Row = 0;
                    foreach (Microsoft.Office.Interop.Word.Cell item in row.Cells)
                    {
                        if (item.RowIndex == 1)
                        {
                            item.Range.Text = DT.Columns[Colums].ColumnName;
                            Colums++;
                        }
                        else
                        {
                            item.Range.Text = sa[Row].ToString();
                            Row++;
                        }
                    }
                    Count++;
                    RowS++;
                    if (Count == RowS)
                        RowS = RowS - 1;
                }
                SaveFileDialog fm = new SaveFileDialog();
                fm.Filter = "DOCX files(*.docx)|*.docx|All files(*.*)|*.*";
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = "Отчет";
                saveFileDialog.DefaultExt = ".docx";
                saveFileDialog.Filter = "DOCX files(*.docx)|*.docx|All files(*.*)|*.*";
                saveFileDialog.ShowDialog();
                Doc.SaveAs2(saveFileDialog.FileName);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка Word" + ex.ToString()); }
            finally { Doc.Close(); }
        }
    }
}
