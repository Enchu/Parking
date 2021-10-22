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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Parking.Windows;

namespace Parking.Windows
{
    public partial class SpravochnikWindow : Window
    {
        List<ClassSpravka.Spravka> ListName = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ff(ComboBox fs, string sql, string ls2)
        {
            List<ClassSpravka.Spravka> fg = ClassSpravka.SelectTB(sql, ls2, 0);
            for (int i = 0; i < fg.Count; i++)
            {
                fs.Items.Add(fg[i].Name);
            }
            return fg;
        }
        int Search(List<ClassSpravka.Spravka> gg, string nn)
        {
            for (int i = 0; i < gg.Count; i++)
            {
                if (gg[i].Name == nn)
                {
                    return gg[i].Id;
                }
            }
            return 0;
        }
        DataTable dt = new DataTable();
        string tt;

        void Load(string text)
        {
            try
            {
                dt = new DataTable();
                dt.Clear();
                ClassSpravka.Cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `" + text + "`;", ClassSpravka.Cn);
                dt.Load(cmd.ExecuteReader());

                DataGridOsn.ItemsSource = null;
                DataGridOsn.Items.Clear();
                nn.Clear();
                ClassSpravka.Cn.Close();

                DataGridOsn.ItemsSource = null;
                DataGridOsn.Items.Clear();
                DataGridOsn.ItemsSource = dt.DefaultView;
            }
            catch { }
        }
        public class Spravka
        {
            public string Name1 { get; set; }
            public string Name2 { get; set; }
        }
        List<Spravka> nn = new List<Spravka>();
        string it1 = null;
        string it2 = null;
        public SpravochnikWindow()
        {
            InitializeComponent();
            CBSpravka.Items.Add("sector");
            CBSpravka.Items.Add("address");
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            AdminWindow fm = new AdminWindow();
            fm.Show();
            this.Close();
        }


        public static void RussianTranslationColumns(ref DataGrid MainTable)
        {
            string[] WordListEnRu = { "address", "Адрес", "sector", "Сектор", "floor", "Этаж"};
            foreach (var Element in MainTable.Columns)
            {
                for (int i = 0; i <= WordListEnRu.Length - 1; i += 2)
                {
                    if (Element.Header.ToString() == WordListEnRu[i].ToString())
                    {
                        Element.Header = WordListEnRu[i + 1];
                        break;
                    }
                }
            }
        }

        private void CBSpravkaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load(CBSpravka.SelectedItem.ToString());
            RussianTranslationColumns(ref DataGridOsn);
        }

        private void DataGridMouseDoubleClickOsn(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int y = DataGridOsn.SelectedIndex;
                DataRow t = dt.Rows[y];
                Name1tb.Text = t.ItemArray[0].ToString();
                Namett.Text = t.ItemArray[0].ToString();
                Name2tb.Text = t.ItemArray[1].ToString();
                if (Name2tb.Text != "")
                {
                    Name2tb.Visibility = Visibility.Visible;
                }
            }
            catch { }

        }

        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (Name1tb.Text != "")
            {
                try
                {
                    foreach (var t in DataGridOsn.Columns.ToList())
                    {
                        tt = t.Header.ToString();
                    }
                    ClassSpravka.Prover($"Select count(*) from `" + CBSpravka.SelectedItem.ToString() + "` where `" + tt + "` = '" + Name1tb.Text + "'", "INSERT INTO `" + CBSpravka.SelectedItem.ToString() + "` (`" + tt + "`) VALUES ('" + Name1tb.Text + "');");
                }
                catch (Exception ex)
                {
                    Msg.ShowError($"Ошибка подключения к БД{ex}");
                }
                finally
                {

                    Load(CBSpravka.SelectedItem.ToString());
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickRed(object sender, RoutedEventArgs e)
        {
            if (Name1tb.Text != "")
            {
                try
                {
                    foreach (var t in DataGridOsn.Columns.ToList())
                    {
                        tt = t.Header.ToString();
                    }
                    ClassSpravka.Prover($"Select count(*) from `" + CBSpravka.SelectedItem.ToString() + "` where `" + tt + "` = '" + Name1tb.Text + "'", "UPDATE `" + CBSpravka.SelectedItem.ToString() + "` SET `" + tt + "` = '" + Name1tb.Text + "' WHERE  `" + tt + "` ='" + Namett.Text + "';");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к БД" + ex);
                }
                finally
                {
                    ClassSpravka.Cn.Close();
                    Load(CBSpravka.SelectedItem.ToString());
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }
    }
}
