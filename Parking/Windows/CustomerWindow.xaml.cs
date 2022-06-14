using MySql.Data.MySqlClient;
using Parking.Entities;
using Parking.Entities.Help;
using Parking.Entities.NewFolder1;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Parking.Windows
{
    public partial class CustomerWindow : Window
    {
        List<Directory> ListRole = new List<Directory>();
        List<Directory> ListStatus = new List<Directory>();
        List<Directory> ListGender = new List<Directory>();
        void Load()
        {
            listPerson.Clear();

            var dataCustomer = Catalog.LoadWindow("`customer`");
            var dataGender = Catalog.LoadWindow("`gender`");

            foreach (DataRow itemCustomer in dataCustomer.Rows)
            {
                foreach (DataRow itemGender in dataGender.Rows)
                {
                    if (itemGender["Gender"].ToString() == itemCustomer["Gender"].ToString())
                    {
                        this.tableGender = itemGender["Gender"].ToString();
                    }
                }
                listPerson.Add(new Person()
                {
                    First_name = itemCustomer["First_name"].ToString(),
                    Last_name = itemCustomer["Last_name"].ToString(),
                    Patronomyc = itemCustomer["Patronomyc"].ToString(),
                    Phone = itemCustomer["Phone"].ToString(),
                    Email = itemCustomer["Email"].ToString(),
                    Password = itemCustomer["Password"].ToString(),
                    Gender = this.tableGender,
                });
            }

            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = listPerson;

            CBGender.Items.Clear();
            ListGender = SelectTable.listSelectTable(CBGender, "SELECT * FROM `gender`", 0);
        }

        List<Person> listPerson = new List<Person>();
        string tableGender = null;
        public CustomerWindow()
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
            if (TBFirst_name.Text != "" && TBLast_name.Text != "" && TBPatronomyc.Text != "" && TBPhone.Text != "" && TBEmail.Text != "" && TBPassword.Text != "")
            {
                if (Msg.ShowQuestion("Вы действительно хотите добавить?"))
                {
                    Catalog.mysqlTableCommand($"Select count(*) from customer where Email = '{TBEmail.Text}'", "INSERT INTO `customer` (`Email`,`Password`,`First_name`,`Last_name`,`Patronomyc`,`Phone`,`Gender`) VALUES " +
                            "( '" + TBEmail.Text + "','" + TBPassword.Text + "','" + TBFirst_name.Text + "', '" + TBLast_name.Text + "', '" + TBPatronomyc.Text + "', '" + TBPhone.Text + "','" + CBGender.Text + "' );");
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
            if (TBFirst_name.Text != "" && TBLast_name.Text != "" && TBPatronomyc.Text != "" && TBPhone.Text != "" && TBEmail.Text != "" && TBPassword.Text != "")
            {
                if (Msg.ShowQuestion("Вы действительно хотите обновить?"))
                {
                    Catalog.mysqlTableCommand($"Select count(*) from customer where Email = '{TBEmail.Text}' and Phone = '" + TBPhone.Text + "'", "UPDATE `customer` SET `Email`='" + TBEmail.Text + "', `Password` = '" + TBPassword.Text + "', `First_name` = '" + TBFirst_name.Text + "', `Last_name` = '" + TBLast_name.Text + "', `Patronomyc` = '" + TBPatronomyc.Text + "', `Phone` = '" + TBPhone.Text + "',`Gender`='" + CBGender.Text + "' WHERE `Email` = '" + TBIdEmail.Text + "';");
                    Load();
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickDelete(object sender, RoutedEventArgs e)
        {
            if (TBEmail.Text != "")
            {
                if (Msg.ShowQuestion("Вы действительно хотите удалить?"))
                {
                    Catalog.mysqlCommand("DELETE FROM `customer` WHERE `customer`.`Email` = '" + TBIdEmail.Text + "'");
                    Load();
                }
            }
            else
            {
                MessageBox.Show("Выберите значение из таблицы");
            }
        }

        private void DataGridMouseDoubleClickOsn(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dynamic selectedItem = DataGridOsn.SelectedItem;
                TBFirst_name.Text = selectedItem.First_name;
                TBLast_name.Text = selectedItem.Last_name;
                TBPatronomyc.Text = selectedItem.Patronomyc;
                TBPhone.Text = selectedItem.Phone;
                TBEmail.Text = selectedItem.Email;
                TBPassword.Text = selectedItem.Password;
                TBIdEmail.Text = selectedItem.Email;
                CBGender.Text = selectedItem.Gender;
            }
            catch { }
        }

        private void KDPhone(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.OemComma)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void KDName(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Oem1 || e.Key == Key.Oem6 || e.Key == Key.OemOpenBrackets)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
