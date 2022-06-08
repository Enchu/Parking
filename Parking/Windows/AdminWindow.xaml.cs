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
using Parking.Entities;


namespace Parking.Windows
{
    public partial class AdminWindow : Window
    {
        List<ClassSpravka.Spravka> ListRole = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListStatus = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListGender = new List<ClassSpravka.Spravka>();

        void Load()
        {
            nn.Clear();

            ClassSpravka.Cn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `staff`", ClassSpravka.Cn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM `role`", ClassSpravka.Cn);
            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM `status`", ClassSpravka.Cn);
            DataTable dt2 = new DataTable();
            dt2.Load(cmd2.ExecuteReader());
            MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM `gender`", ClassSpravka.Cn);
            DataTable dt3 = new DataTable();
            dt3.Load(cmd3.ExecuteReader());
            ClassSpravka.Cn.Close();

            foreach (DataRow item in dt.Rows)
            {
                foreach (DataRow item1 in dt1.Rows)
                {
                    if (item1["Role"].ToString() == item["Role"].ToString())
                    {
                        it1 = item1["Role"].ToString();
                    }
                }
                foreach (DataRow item2 in dt2.Rows)
                {
                    if (item2["status"].ToString() == item["status"].ToString())
                    {
                        it2 = item2["status"].ToString();
                    }
                }
                foreach (DataRow item3 in dt3.Rows)
                {
                    if (item3["Gender"].ToString() == item["Gender"].ToString())
                    {
                        it3 = item3["Gender"].ToString();
                    }
                }
                nn.Add(new TableSpravka.Person()
                {
                    First_name = item["First_name"].ToString(),
                    Last_name = item["Last_name"].ToString(),
                    Patronomyc = item["Patronomyc"].ToString(),
                    Phone = item["Phone"].ToString(),
                    Role = it1,
                    Email = item["Email"].ToString(),
                    Password = item["Password"].ToString(),
                    Status = it2,
                    Gender = it3,
                });
            }

            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = nn;

            CBStatus.Items.Clear();
            CBRole.Items.Clear();
            CBGender.Items.Clear();

            ListRole = SelectTable.ff(CBRole, "SELECT * FROM `role`", "Role", 0);
            ListStatus = SelectTable.ff(CBStatus, "SELECT * FROM `status`", "Status", 0);
            ListGender = SelectTable.ff(CBGender, "SELECT * FROM `gender`", "gender", 0);
        }
        List<TableSpravka.Person> nn = new List<TableSpravka.Person>();
        string it1 = null;
        string it2 = null;
        string it3 = null;
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
                        ClassSpravka.Prover($"Select count(*) from staff where Email = '{TBEmail.Text}'", "INSERT INTO `staff` (`Email`,`Password`,`First_name`,`Last_name`,`Patronomyc`,`Phone`,`Status`,`Role`,`Gender`) VALUES " +
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
                    ClassSpravka.Prover($"Select count(*) from staff where Email = '{TBEmail.Text}' and Phone = '" + TBPhone.Text + "'", "UPDATE `staff` SET `Email`='" + TBEmail.Text + "', `Password` = '" + TBPassword.Text + "', `First_name` = '" + TBFirst_name.Text + "', `Last_name` = '" + TBLast_name.Text + "', `Patronomyc` = '" + TBPatronomyc.Text + "', `Phone` = '" + TBPhone.Text + "', `Role` = '" + CBRole.Text + "',  `Status` = '" + CBStatus.Text + "',`Gender`='" + CBGender.Text + "' WHERE `staff`.`Email` = '" + TBIdEmail.Text + "';");
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
                    ClassSpravka.Cn.Open();
                    //Select count(*) from staff WHERE `Status` = "Работает" AND `Role` = "Администратор"
                    MySqlCommand NameS = new MySqlCommand($"Select count(*) from staff WHERE `Status` = 'Работает' AND `Role` = 'Администратор'", ClassSpravka.Cn);
                    var Im = NameS.ExecuteScalar();
                    ClassSpravka.Cn.Close();
                    if ((long)Im != 1)
                    {
                        ClassSpravka.MysqlLi("UPDATE `staff` SET `Status` = 'Удален' WHERE (`Email` = '" + TBEmail.Text + "')");
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
