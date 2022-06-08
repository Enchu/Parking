using MySql.Data.MySqlClient;
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
using Parking.Windows;
using System.Windows.Threading;
using Parking.Entities;

namespace Parking.Windows
{
    public partial class ManagerWindow : Window
    {
        List<ClassSpravka.Spravka> ListOwner = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListPlace = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListBuilding = new List<ClassSpravka.Spravka>();

        List<TableSpravka.Manager> nn = new List<TableSpravka.Manager>();
        string it1 = null;
        string it2 = null;
        string it3 = null;
        void Load()
        {
            nn.Clear();

            DataTable dt = ClassSpravka.LoadVV("SELECT * FROM `parking`");
            DataTable dt1 = ClassSpravka.LoadVV("SELECT * FROM `customer`");
            DataTable dt2 = ClassSpravka.LoadVV("SELECT * FROM `sector`");
            DataTable dt3 = ClassSpravka.LoadVV("SELECT * FROM `building`");

            foreach (DataRow item in dt.Rows)
            {
                foreach (DataRow item1 in dt1.Rows)
                {
                    if (item1["email"].ToString() == item["customer"].ToString())
                    {
                        it1 = item1["email"].ToString();
                    }
                }
                foreach (DataRow item2 in dt2.Rows)
                {
                    if (item2["sector"].ToString() == item["sector"].ToString())
                    {
                        it2 = item2["sector"].ToString();
                    }
                }
                foreach (DataRow item3 in dt3.Rows)
                {
                    if (item3["building"].ToString() == item["building"].ToString())
                    {
                        it3 = item3["building"].ToString();
                    }
                }
                nn.Add(new TableSpravka.Manager()
                {
                    id = item["id"].ToString(),
                    datestart = item["datestart"].ToString(),
                    dataend = item["dataend"].ToString(),
                    customer = it1,
                    sector = it2,
                    building = it3,
                    price = item["price"].ToString(),
                });
            }
            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = nn;

            CBClient.Items.Clear();
            CBMesto.Items.Clear();
            CBBuilding.Items.Clear();

            CBMesto.Items.Add("Нет");
            ListOwner = SelectTable.ff(CBClient, "SELECT * FROM `customer`", "email", 0);
            ListPlace = SelectTable.ff(CBMesto, "SELECT * FROM `sector`", "sector", 0);
            ListBuilding = SelectTable.ff(CBBuilding, "SELECT * FROM `building`", "building", 1);
        }

        string name1 = "";
        public ManagerWindow(string name)
        {
            InitializeComponent();
            Load();
            name1 = name;
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
                            ClassSpravka.MysqlLi("INSERT INTO `parking` (`id`, `datestart`, `dataend`,`customer`, `sector`, `building`, `price`) VALUES (NULL, '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + CBClient.Text + "', NULL, '" + CBBuilding.Text + "', '" + TBPrice.Text + "');");
                            Load();
                        }
                        else
                        {
                            ClassSpravka.MysqlLi("INSERT INTO `parking` (`id`, `datestart`, `dataend`, `customer`, `sector`, `building`, `price`) VALUES (NULL, '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', '" + CBClient.Text + "', '" + CBMesto.Text + "', '" + CBBuilding.Text + "', '" + TBPrice.Text + "');");
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
                        
                            ClassSpravka.MysqlLi("UPDATE `parking` SET `datestart` = '" + DPDateStart.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "', `dataend` = '" + DPDataEnd.Value.Value.Date.ToString("yyyy-MM-dd") + " " + DPDateStart.Value.Value.Hour.ToString() + ":" + DPDateStart.Value.Value.Minute.ToString() + ":" + DPDateStart.Value.Value.Second.ToString() + "',`sector`= '" + CBClient.Text + "', `sector`= '" + CBMesto.Text + "', `building`= '" + CBBuilding.Text + "', `price` = '" + TBPrice.Text + "' WHERE `parking`.`id` = '" + TBParkingID.Text + "';");
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
                    ClassSpravka.MysqlLi("DELETE FROM `parking` WHERE `id` = '" + TBParkingID.Text + "';");
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
            StatistWindow fm = new StatistWindow(name1);
            fm.Show();
            this.Close();
        }
        bool PriceArray = false;
        int hours;int minutes;
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
