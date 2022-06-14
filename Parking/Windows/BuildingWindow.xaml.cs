using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Parking.Entities;
using Parking.Entities.Help;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Parking.Windows
{
    public partial class BuildingWindow : Window
    {
        List<Entities.NewFolder1.Directory> ListAddress = new List<Entities.NewFolder1.Directory>();
        List<Building> listBuildingTable = new List<Building>();

        string dataAddress = null;
        void Load()
        {
            listBuildingTable.Clear();

            var dataBuilding = Catalog.LoadWindow("`building`");
            var dataAddress = Catalog.LoadWindow("`address`");

            foreach (DataRow itemBuilding in dataBuilding.Rows)
            {
                foreach (DataRow itemAddress in dataAddress.Rows)
                {
                    if (itemAddress["address"].ToString() == itemBuilding["address"].ToString())
                    {
                        this.dataAddress = itemAddress["address"].ToString();
                    }
                }
                if (itemBuilding["chema"].ToString().Length > 4)
                {
                    byte[] imgData = (byte[])itemBuilding["chema"];
                    MemoryStream ms = new MemoryStream(imgData);
                    listBuildingTable.Add(new Building()
                    {
                        chema = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad),
                        id = itemBuilding["id"].ToString(),
                        building = itemBuilding["building"].ToString(),
                        number_quant = itemBuilding["number_quant"].ToString(),
                        address = this.dataAddress,
                    });
                }
                else
                {
                    listBuildingTable.Add(new Building()
                    {
                        id = itemBuilding["id"].ToString(),
                        building = itemBuilding["building"].ToString(),
                        number_quant = itemBuilding["number_quant"].ToString(),
                        address = this.dataAddress,
                    });
                }
            }
            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = listBuildingTable;

            CBAddress.Items.Clear();

            ListAddress = SelectTable.listSelectTable(CBAddress, "SELECT * FROM `address`", 0);
        }
        public BuildingWindow()
        {
            InitializeComponent();
            Load();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            ManagerWindow fm = new ManagerWindow(Name);
            fm.Show();
            this.Close();
        }

        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (TBBuilding.Text != " " && CBAddress.Text != " " && TBNumberQuant.Text != " ")
            {
                if (Msg.ShowQuestion("Вы действительно хотите выполнить это действие?"))
                {
                    Catalog.mysqlCommand("INSERT INTO `building` (`id`,`building`, `address`,`number_quant`,`chema`) VALUES (NULL,'" + TBBuilding.Text + "', '" + CBAddress.Text.Trim() + "','" + TBNumberQuant.Text + "',NULL);");
                    Load();
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            if (TBBuilding.Text != " " && CBAddress.Text != " " && TBNumberQuant.Text != " ")
            {
                if (TBIDBuilding.Text != " ")
                {
                    if (Msg.ShowQuestion("Вы действительно хотите выполнить это действие?"))
                    {
                        Catalog.mysqlCommand("UPDATE `building` SET `building` = '" + TBBuilding.Text + "',`address`='" + CBAddress.Text + "',`number_quant`='" + TBNumberQuant.Text + "' WHERE `id` = '" + TBIDBuilding.Text + "';");
                        Load();
                    }
                }
                else
                {
                    Msg.ShowError("Не выбран эелемент");
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickDelete(object sender, RoutedEventArgs e)
        {
            if (TBBuilding.Text != " ")
            {
                if (Msg.ShowQuestion("Вы действительно хотите выполнить это действие?"))
                {
                    Catalog.mysqlCommand("DELETE FROM `building` WHERE `building`.`id` = '" + TBIDBuilding.Text + "'");
                    Load();
                }
            }
            else
            {
                Msg.ShowError("Не выбран элемент");
            }
        }

        private void DataGridMouseDoubleClickOsn(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic selectedItem = DataGridOsn.SelectedItem;
                PicWorker.Source = selectedItem.chema;
                TBBuilding.Text = selectedItem.building;
                TBIDBuilding.Text = selectedItem.id;
                CBAddress.Text = selectedItem.address;
                TBNumberQuant.Text = selectedItem.number_quant;
            }
            catch { }
        }
        string pp;
        private void ButtonClickOB(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fm = new OpenFileDialog();
            fm.Filter = "*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fm.ShowDialog();
            if (fm.FileName != "")
            {
                var bitimage = new BitmapImage(new Uri(fm.FileName));
                PicWorker.Source = bitimage;
                pp = fm.FileName;
            }

        }

        private void ButtonClickZad(object sender, RoutedEventArgs e)
        {
            if (TBIDBuilding.Text != "")
            {
                if (pp.ToString() != "")
                {
                    byte[] BitImage = null;
                    FileStream FStream = new FileStream(pp, FileMode.Open, FileAccess.Read);
                    BinaryReader BR = new BinaryReader(FStream);
                    BitImage = BR.ReadBytes((int)FStream.Length);
                    Catalog.connection.Open();
                    MySqlCommand Cmd2 = new MySqlCommand("UPDATE `building` SET `chema` = @img WHERE `id` = '" + TBIDBuilding.Text + "';", Catalog.connection);
                    Cmd2.Parameters.Add(new MySqlParameter("@img", BitImage));
                    Cmd2.ExecuteReader();
                    Catalog.connection.Close();
                    MessageBox.Show("Фото обновлено");
                    Load();
                }
            }
            else
            {
                MessageBox.Show("Выберите значение из таблицы");
            }
        }
    }
}
