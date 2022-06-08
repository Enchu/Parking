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
    public partial class CustomerWindow : Window
    {
        List<ClassSpravka.Spravka> ListRole = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListStatus = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ListGender = new List<ClassSpravka.Spravka>();
        void Load()
        {
            nn.Clear();
            ClassSpravka.Cn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `customer`", ClassSpravka.Cn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM `gender`", ClassSpravka.Cn);
            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            ClassSpravka.Cn.Close();

            foreach (DataRow item in dt.Rows)
            {
                foreach (DataRow item1 in dt1.Rows)
                {
                    if (item1["Gender"].ToString() == item["Gender"].ToString())
                    {
                        it1 = item1["Gender"].ToString();
                    }
                }
                nn.Add(new TableSpravka.Person()
                {
                    First_name = item["First_name"].ToString(),
                    Last_name = item["Last_name"].ToString(),
                    Patronomyc = item["Patronomyc"].ToString(),
                    Phone = item["Phone"].ToString(),
                    Email = item["Email"].ToString(),
                    Password = item["Password"].ToString(),
                    Gender = it1,
                });
            }

            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = nn;

            CBGender.Items.Clear();
            ListGender = SelectTable.ff(CBGender, "SELECT * FROM `gender`", "gender", 0);
        }
        public class Spravka
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public string Patronomyc { get; set; }
            public string Phone { get; set; }
            public string Status { get; set; }
            public string Role { get; set; }
            public BitmapFrame Photo { get; set; }
            public string Gender { get; set; }
        }
        List<TableSpravka.Person> nn = new List<TableSpravka.Person>();
        string it1 = null;
        string it2 = null;
        string it3 = null;
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
                    ClassSpravka.Prover($"Select count(*) from customer where Email = '{TBEmail.Text}'", "INSERT INTO `customer` (`Email`,`Password`,`First_name`,`Last_name`,`Patronomyc`,`Phone`,`Gender`) VALUES " +
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
                    ClassSpravka.Prover($"Select count(*) from customer where Email = '{TBEmail.Text}' and Phone = '" + TBPhone.Text + "'", "UPDATE `customer` SET `Email`='" + TBEmail.Text + "', `Password` = '" + TBPassword.Text + "', `First_name` = '" + TBFirst_name.Text + "', `Last_name` = '" + TBLast_name.Text + "', `Patronomyc` = '" + TBPatronomyc.Text + "', `Phone` = '" + TBPhone.Text + "',`Gender`='" + CBGender.Text + "' WHERE `Email` = '" + TBIdEmail.Text + "';");
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
                    ClassSpravka.MysqlLi("DELETE FROM `customer` WHERE `customer`.`Email` = '"+TBIdEmail.Text+"'");
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
