using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Parking.Windows;

namespace Parking.Windows
{
    public partial class BuildingWindow : Window
    {
        List<ClassSpravka.Spravka> ListAddress = new List<ClassSpravka.Spravka>();
        List<ClassSpravka.Spravka> ff(ComboBox fs, string sql, string ls2, int Column)
        {
            List<ClassSpravka.Spravka> fg = ClassSpravka.SelectTB(sql, ls2, Column);
            for (int i = 0; i < fg.Count; i++)
            {
                fs.Items.Add(fg[i].Name);
            }
            return fg;
        }
        public class Spravka
        {
            public string id { get; set; }
            public string building { get; set; }
            public string address { get; set; }
            public string number_quant { get; set; }
            public BitmapFrame chema { get; set; }

        }
        List<Spravka> nn = new List<Spravka>();
        string it1 = null;
        void Load()
        {
            nn.Clear();
            ClassSpravka.Cn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `building`", ClassSpravka.Cn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM `address`", ClassSpravka.Cn);
            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());
            ClassSpravka.Cn.Close();

            foreach (DataRow item in dt.Rows)
            {
                foreach (DataRow item1 in dt1.Rows)
                {
                    if (item1["address"].ToString() == item["address"].ToString())
                    {
                        it1 = item1["address"].ToString();
                    }
                }
                if (item["chema"].ToString().Length > 4)
                {
                    byte[] imgData = (byte[])item["chema"];
                    MemoryStream ms = new MemoryStream(imgData);
                    nn.Add(new Spravka()
                    {
                        chema = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad),
                        id = item["id"].ToString(),
                        building = item["building"].ToString(),
                        number_quant = item["number_quant"].ToString(),
                        address = it1,
                    });
                }else
                {
                    nn.Add(new Spravka()
                    {
                        id = item["id"].ToString(),
                        building = item["building"].ToString(),
                        number_quant = item["number_quant"].ToString(),
                        address = it1,
                    });
                }
            }
            DataGridOsn.ItemsSource = null;
            DataGridOsn.Items.Clear();
            DataGridOsn.ItemsSource = nn;

            CBAddress.Items.Clear();

            ListAddress = ff(CBAddress, "SELECT * FROM `address`", "address", 0);
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
                    ClassSpravka.MysqlLi("INSERT INTO `building` (`id`,`building`, `address`,`number_quant`,`chema`) VALUES (NULL,'" + TBBuilding.Text + "', '" + CBAddress.Text.Trim() + "','" + TBNumberQuant.Text + "',NULL);");
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
                        ClassSpravka.MysqlLi("UPDATE `building` SET `building` = '" + TBBuilding.Text + "',`address`='" + CBAddress.Text + "',`number_quant`='" + TBNumberQuant.Text + "' WHERE `id` = '" + TBIDBuilding.Text + "';");
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
                    ClassSpravka.MysqlLi("DELETE FROM `building` WHERE `building`.`id` = '"+TBIDBuilding.Text+"'");
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
                    ClassSpravka.Cn.Open();
                    MySqlCommand Cmd2 = new MySqlCommand("UPDATE `building` SET `chema` = @img WHERE `id` = '" + TBIDBuilding.Text + "';", ClassSpravka.Cn);
                    Cmd2.Parameters.Add(new MySqlParameter("@img", BitImage));
                    Cmd2.ExecuteReader();
                    ClassSpravka.Cn.Close();
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
