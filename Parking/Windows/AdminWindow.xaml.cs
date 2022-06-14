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
    public partial class AdminWindow : Window
    {
        List<Directory> ListRole = new List<Directory>();
        List<Directory> ListStatus = new List<Directory>();
        List<Directory> ListGender = new List<Directory>();

        public void Load()
        {
            listPersonTable.Clear();

            var dataStaff = Catalog.LoadWindow("`staff`");
            var dataRole = Catalog.LoadWindow("`role`");
            var dataStatus = Catalog.LoadWindow("`status`");
            var dataGender = Catalog.LoadWindow("`gender`");

            foreach (DataRow itemStaff in dataStaff.Rows)
            {
                foreach (DataRow itemRole in dataRole.Rows)
                {
                    if (itemRole["Role"].ToString() == itemStaff["Role"].ToString())
                    {
                        tableRole = itemRole["Role"].ToString();
                    }
                }
                foreach (DataRow itemStatus in dataStatus.Rows)
                {
                    if (itemStatus["status"].ToString() == itemStaff["status"].ToString())
                    {
                        TableStatus = itemStatus["status"].ToString();
                    }
                }
                foreach (DataRow itemGender in dataGender.Rows)
                {
                    if (itemGender["Gender"].ToString() == itemStaff["Gender"].ToString())
                    {
                        tableGender = itemGender["Gender"].ToString();
                    }
                }
                listPersonTable.Add(new Person()
                {
                    First_name = itemStaff["First_name"].ToString(),
                    Last_name = itemStaff["Last_name"].ToString(),
                    Patronomyc = itemStaff["Patronomyc"].ToString(),
                    Phone = itemStaff["Phone"].ToString(),
                    Role = tableRole,
                    Email = itemStaff["Email"].ToString(),
                    Password = itemStaff["Password"].ToString(),
                    Status = TableStatus,
                    Gender = tableGender,
                });
            }

            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = listPersonTable;

            CBStatus.Items.Clear();
            CBRole.Items.Clear();
            CBGender.Items.Clear();

            ListRole = SelectTable.listSelectTable(CBRole, "SELECT * FROM `role`", 0);
            ListStatus = SelectTable.listSelectTable(CBStatus, "SELECT * FROM `status`", 0);
            ListGender = SelectTable.listSelectTable(CBGender, "SELECT * FROM `gender`", 0);
        }
        List<Person> listPersonTable = new List<Person>();
        string tableRole = null;
        string TableStatus = null;
        string tableGender = null;
        public AdminWindow()
        {
            InitializeComponent();
            Load();
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
                CBRole.Text = selectedItem.Role;
                TBEmail.Text = selectedItem.Email;
                TBPassword.Text = selectedItem.Password;
                CBStatus.Text = selectedItem.Status;
                TBIdEmail.Text = selectedItem.Email;
                CBGender.Text = selectedItem.Gender;
            }
            catch { }
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            MainWindow fm = new MainWindow();
            fm.Show();
            this.Close();
        }

        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (TBFirst_name.Text != "" && TBLast_name.Text != "" && TBPatronomyc.Text != "" && TBPhone.Text != "" && CBRole.Text != "" && TBEmail.Text != "" && TBPassword.Text != "")
            {
                if (TBFirst_name.Text.Length > 2 && TBLast_name.Text.Length > 2 && TBPatronomyc.Text.Length > 2 && CBRole.Text.Length > 2 && TBEmail.Text.Length > 9 && TBPassword.Text.Length > 6)
                {
                    if (Msg.ShowQuestion("Вы действительно хотите добавить?"))
                    {
                        Catalog.mysqlTableCommand($"Select count(*) from staff where Email = '{TBEmail.Text}'", "INSERT INTO `staff` (`Email`,`Password`,`First_name`,`Last_name`,`Patronomyc`,`Phone`,`Status`,`Role`,`Gender`) VALUES " +
                                "( '" + TBEmail.Text + "','" + TBPassword.Text + "','" + TBFirst_name.Text + "', '" + TBLast_name.Text + "', '" + TBPatronomyc.Text + "', '" + TBPhone.Text + "', " +
                                "'" + CBStatus.Text + "','" + CBRole.Text + "','" + CBGender.Text + "' );");
                        Load();
                    }
                }
                else
                {
                    Msg.ShowError("Поля не могут быть меньше 3 символов");
                }
            }
            else
            {
                Msg.ShowError("Не все поля заполнены");
            }
        }

        private void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            if (TBFirst_name.Text != "" && TBLast_name.Text != "" && TBPatronomyc.Text != "" && TBPhone.Text != "" && CBRole.Text != "" && TBEmail.Text != "" && TBPassword.Text != "")
            {
                if (Msg.ShowQuestion("Вы действительно хотите обновить?"))
                {
                    Catalog.mysqlTableCommand($"Select count(*) from staff where Email = '{TBEmail.Text}' and Phone = '" + TBPhone.Text + "'", "UPDATE `staff` SET `Email`='" + TBEmail.Text + "', `Password` = '" + TBPassword.Text + "', `First_name` = '" + TBFirst_name.Text + "', `Last_name` = '" + TBLast_name.Text + "', `Patronomyc` = '" + TBPatronomyc.Text + "', `Phone` = '" + TBPhone.Text + "', `Role` = '" + CBRole.Text + "',  `Status` = '" + CBStatus.Text + "',`Gender`='" + CBGender.Text + "' WHERE `staff`.`Email` = '" + TBIdEmail.Text + "';");
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
                    Catalog.connection.Open();
                    //Select count(*) from staff WHERE `Status` = "Работает" AND `Role` = "Администратор"
                    MySqlCommand NameS = new MySqlCommand($"Select count(*) from staff WHERE `Status` = 'Работает' AND `Role` = 'Администратор'", Catalog.connection);
                    var Im = NameS.ExecuteScalar();
                    Catalog.connection.Close();
                    if ((long)Im != 1)
                    {
                        Catalog.mysqlCommand("UPDATE `staff` SET `Status` = 'Удален' WHERE (`Email` = '" + TBEmail.Text + "')");
                        Load();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите значение из таблицы");
            }
        }

        private void ButtonClickSpravka(object sender, RoutedEventArgs e)
        {
            SpravochnikWindow fm = new SpravochnikWindow();
            fm.Show();
            this.Close();
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

        private void KDFName(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.OemQuotes)
                TBFirst_name.Text += "'";
            else
                TBFirst_name.Text += e.Key.ToString();*/
            if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Oem1 || e.Key == Key.Oem6 || e.Key == Key.OemOpenBrackets)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void KDFEmail(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.OemQuotes)
                TBFirst_name.Text += "'";
            else
                TBFirst_name.Text += e.Key.ToString();
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/
        }
    }
}
