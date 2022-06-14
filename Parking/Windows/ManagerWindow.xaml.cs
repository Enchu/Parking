using Parking.Entities;
using Parking.Entities.Help;
using Parking.Entities.NewFolder1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Parking.Windows
{
    public partial class ManagerWindow : Window
    {
        List<Directory> ListOwner = new List<Directory>();
        List<Directory> ListPlace = new List<Directory>();
        List<Directory> ListBuilding = new List<Directory>();

        List<Manager> listManagerTable = new List<Manager>();
        string tableEmail = null;
        string tableSector = null;
        string tableBuilding = null;
        void Load()
        {
            listManagerTable.Clear();

            var dataParking = Catalog.LoadWindow("`parking`");
            var dataCustomer = Catalog.LoadWindow("`customer`");
            var dataSector = Catalog.LoadWindow("`sector`");
            var dataBuilding = Catalog.LoadWindow("`building`");

            foreach (DataRow itemParking in dataParking.Rows)
            {
                foreach (DataRow itemCustomer in dataCustomer.Rows)
                {
                    if (itemCustomer["email"].ToString() == itemParking["customer"].ToString())
                    {
                        tableEmail = itemCustomer["email"].ToString();
                    }
                }
                foreach (DataRow itemSector in dataSector.Rows)
                {
                    if (itemSector["sector"].ToString() == itemParking["sector"].ToString())
                    {
                        tableSector = itemSector["sector"].ToString();
                    }
                }
                foreach (DataRow itemBuilding in dataBuilding.Rows)
                {
                    if (itemBuilding["building"].ToString() == itemParking["building"].ToString())
                    {
                        tableBuilding = itemBuilding["building"].ToString();
                    }
                }
                listManagerTable.Add(new Manager()
                {
                    Id = itemParking["id"].ToString(),
                    DateStart = itemParking["datestart"].ToString(),
                    DataEnd = itemParking["dataend"].ToString(),
                    Customer = tableEmail,
                    Sector = tableSector,
                    Building = tableBuilding,
                    Price = itemParking["price"].ToString(),
                });
            }
            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = listManagerTable;

            CBClient.Items.Clear();
            CBMesto.Items.Clear();
            CBBuilding.Items.Clear();

            CBMesto.Items.Add("Нет");

            ListOwner = SelectTable.listSelectTable(CBClient, "SELECT * FROM `customer`", 0);
            ListPlace = SelectTable.listSelectTable(CBMesto, "SELECT * FROM `sector`", 0);
            ListBuilding = SelectTable.listSelectTable(CBBuilding, "SELECT * FROM `building`", 1);
        }

        string nameWindow = "";
        public ManagerWindow(string name)
        {
            InitializeComponent();
            Load();
            nameWindow = name;
        }
        private void DataGridMouseDoubleClickOsn(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic selectedItem = DataGridOsn.SelectedItem;
                TBParkingID.Text = selectedItem.id;
                DPDateStart.Text = selectedItem.datestart;
                DPDataEnd.Text = selectedItem.dataend;
                CBMesto.Text = null;
                CBMesto.Text = selectedItem.sector;
                CBClient.Text = selectedItem.customer;
                CBBuilding.Text = selectedItem.building;
                TBPrice.Text = selectedItem.price;
            }
            catch { }
        }

        private void ButtonClickBuilding(object sender, RoutedEventArgs e)
        {
            BuildingWindow fm = new BuildingWindow();
            fm.Show();
            this.Close();
        }

        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (DPDateStart.ToString() != " " && DPDataEnd.ToString() != " " && CBClient.Text != " " && CBBuilding.Text != " " && TBPrice.Text != " ")
            {
                if (PriceArray == true)
                {
                    if (Msg.ShowQuestion("Вы действительно хотите добавить?"))
                    {
                        if (CBMesto.Text == "" || CBMesto.Text == "Нет")
                        {
                            Catalog.mysqlCommand("INSERT INTO `parking` (`id`, `datestart`, `dataend`,`customer`, `sector`, `building`, `price`) VALUES (NULL, '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + CBClient.Text + "', NULL, '" + CBBuilding.Text + "', '" + TBPrice.Text + "');");
                            Load();
                        }
                        else
                        {
                            Catalog.mysqlCommand("INSERT INTO `parking` (`id`, `datestart`, `dataend`, `customer`, `sector`, `building`, `price`) VALUES (NULL, '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + CBClient.Text + "', '" + CBMesto.Text + "', '" + CBBuilding.Text + "', '" + TBPrice.Text + "');");
                            Load();
                        }
                    }
                }
                else
                {
                    Msg.ShowInfo("Не правильно указана дата");
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            if (DPDateStart.ToString() != " " && DPDataEnd.ToString() != " " && CBClient.Text != " " && CBMesto.Text != " " && CBBuilding.Text != " " && TBPrice.Text != " ")
            {
                if (TBParkingID.Text != " ")
                {
                    if (PriceArray == true)
                    {
                        if (Msg.ShowQuestion("Вы действительно хотите обновить?"))
                        {

                            Catalog.mysqlCommand("UPDATE `parking` SET `datestart` = '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', `dataend` = '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "',`sector`= '" + CBClient.Text + "', `sector`= '" + CBMesto.Text + "', `building`= '" + CBBuilding.Text + "', `price` = '" + TBPrice.Text + "' WHERE `parking`.`id` = '" + TBParkingID.Text + "';");
                            Load();
                        }
                    }
                    else
                    {
                        Msg.ShowInfo("Не правильно указана дата");
                    }
                }
                else
                {
                    Msg.ShowError("Не выбран элемент");
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickDelete(object sender, RoutedEventArgs e)
        {
            if (TBParkingID.Text != "")
            {
                if (Msg.ShowQuestion("Вы действительно хотите удалить?"))
                {
                    Catalog.mysqlCommand("DELETE FROM `parking` WHERE `id` = '" + TBParkingID.Text + "';");
                    Load();
                }
            }
            else
            {
                Msg.ShowError("Выберите элемент");
            }
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            MainWindow fm = new MainWindow();
            fm.Show();
            this.Close();
        }


        private void Dataend_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                int dayStart = 0;
                int monthStart = 0;
                int yearStart = 0;

                int dayEnd = 0;
                int monthEnd = 0;
                int yearEnd = 0;

                int priceDate = 0;
                TBPrice.Text = null;
                dayStart = DPDateStart.SelectedDate.Value.Day;
                monthStart = DPDateStart.SelectedDate.Value.Month;
                yearStart = DPDateStart.SelectedDate.Value.Year;

                dayEnd = DPDataEnd.SelectedDate.Value.Day;
                monthEnd = DPDataEnd.SelectedDate.Value.Month;
                yearEnd = DPDataEnd.SelectedDate.Value.Year;

                priceDate = (dayEnd - dayStart) + ((monthEnd - monthStart) * 30) + ((yearEnd - yearStart) * 365);
                priceDate = priceDate * 360;
                TBPrice.Text = priceDate.ToString();
            }
            catch { }*/
        }

        private void ButtonClickCustomer(object sender, RoutedEventArgs e)
        {
            CustomerWindow fm = new CustomerWindow();
            fm.Show();
            this.Close();
        }

        private void ButtonClickStatistika(object sender, RoutedEventArgs e)
        {
            StatistWindow fm = new StatistWindow(nameWindow);
            fm.Show();
            this.Close();
        }
        bool PriceArray = false;
        int hours; int minutes;
        private void DPDataEnd_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                TBPrice.Text = null;
                DateTime dt1 = (DateTime)DPDateStart.Value;
                DateTime dt2 = (DateTime)DPDataEnd.Value;
                TimeSpan x = dt2 - dt1;
                hours = (int)x.TotalHours * 15;
                minutes = (((int)((x.TotalHours - hours) * 100)) / 100) * 60;
                if (hours > 0)
                {
                    PriceArray = true;
                    TBPrice.Text = hours.ToString(); //+ ":" + minutes.ToString();
                }
            }
            catch { }
        }
    }
}
