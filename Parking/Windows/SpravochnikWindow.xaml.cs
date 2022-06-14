using Parking.Entities.NewFolder1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Parking.Windows
{
    public partial class SpravochnikWindow : Window
    {

        DataTable globalDataTable = new DataTable();
        string textName;

        void Load(string text)
        {
            try
            {
                var dataCustomer = Catalog.LoadWindow("`text`");

                DataGridOsn.ItemsSource = null;
                DataGridOsn.Items.Clear();
                DataGridOsn.ItemsSource = dataCustomer.DefaultView;
            }
            catch { }
        }

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
            string[] wordListEnRu = { "address", "Адрес", "sector", "Сектор", "floor", "Этаж" };
            foreach (var Element in MainTable.Columns)
            {
                for (int i = 0; i <= wordListEnRu.Length - 1; i += 2)
                {
                    if (Element.Header.ToString() == wordListEnRu[i].ToString())
                    {
                        Element.Header = wordListEnRu[i + 1];
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
                DataRow dataRowTable = globalDataTable.Rows[y];
                NameFirstTable.Text = dataRowTable.ItemArray[0].ToString();
                NameFirstClone.Text = dataRowTable.ItemArray[0].ToString();
                NameTwoTable.Text = dataRowTable.ItemArray[1].ToString();
                if (NameTwoTable.Text != "")
                {
                    NameTwoTable.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }

        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (NameFirstTable.Text != "")
            {
                try
                {
                    foreach (var t in DataGridOsn.Columns.ToList())
                    {
                        textName = t.Header.ToString();
                    }
                    Catalog.mysqlTableCommand($"Select count(*) from `" + CBSpravka.SelectedItem.ToString() + "` where `" + textName + "` = '" + NameFirstTable.Text + "'", "INSERT INTO `" + CBSpravka.SelectedItem.ToString() + "` (`" + textName + "`) VALUES ('" + NameFirstTable.Text + "');");
                }
                catch (Exception error)
                {
                    Msg.ShowError($"Ошибка подключения к БД{error}");
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
            if (NameFirstTable.Text != "")
            {
                try
                {
                    foreach (var nameTable in DataGridOsn.Columns.ToList())
                    {
                        textName = nameTable.Header.ToString();
                    }
                    Catalog.mysqlTableCommand($"Select count(*) from `" + CBSpravka.SelectedItem.ToString() + "` where `" + textName + "` = '" + NameFirstTable.Text + "'", "UPDATE `" + CBSpravka.SelectedItem.ToString() + "` SET `" + textName + "` = '" + NameFirstTable.Text + "' WHERE  `" + textName + "` ='" + NameFirstClone.Text + "';");
                }
                catch (Exception error)
                {
                    MessageBox.Show("Ошибка подключения к БД" + error);
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
    }
}
