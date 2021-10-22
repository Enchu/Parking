using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Parking.Windows;

namespace Parking
{
    public partial class MainWindow : Window
    {
        MySqlConnection Cn = new MySqlConnection("server = 127.0.0.1; user id = root; port=3310;persistsecurityinfo=True;database=dp;password=;convert zero datetime=True");
        //public MySqlConnection Cn = new MySqlConnection("server =185.92.149.80; user id = admin_leha; port=3306;persistsecurityinfo=True;database=admin_db;charset=utf8;password=hZIKqXWowf;convert zero datetime=True");
        //public static MySqlConnection Cn = new MySqlConnection("server=185.92.149.80;port=3306;database=admin_db;user=admin_leha;password=hZIKqXWowf;charset=utf8");
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string text = string.Empty;
        public static Bitmap SetBitmap(int Width, int Height)//Капча
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Width, Height);
            int Xpos = rnd.Next(0, Width - 100);
            int Ypos = rnd.Next(15, Height - 15);
            Brush[] colors = { Brushes.Black, Brushes.Red, Brushes.RoyalBlue, Brushes.Green };
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.Clear(Color.Gray);
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];
            g.DrawString(text,
            new Font("Arial", 20),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));
            g.DrawLine(Pens.Black,
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
            new System.Drawing.Point(0, Height - 1),
            new System.Drawing.Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);
            return result;
        }
        public static BitmapImage Convert2(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
        int vhodcount = 0;

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /*if (PasswordBox.Password == "Password")
			{
				statusText.Text = "'Password' is not allowed as a password.";
			}
			else
			{
				statusText.Text = string.Empty;
			}*/
        }
        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            /*if (revealModeCheckBox.IsChecked == true)
			{
				PasswordBox.PasswordRevealMode = PasswordBox.Visible;
			}
			else
			{
				PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
			}*/
        }

        private void revealModeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                pwdTextBox.Text = PasswordBox.Password; // скопируем в TextBox из PasswordBox
                pwdTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
                PasswordBox.Visibility = Visibility.Hidden; // PasswordBox - скрыть
            }
            else
            {
                PasswordBox.Password = pwdTextBox.Text; // скопируем в PasswordBox из TextBox 
                pwdTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                PasswordBox.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }
        string Pass = "", Role = "", name = "";

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            if (Msg.ShowQuestion("Вы действительно хотите выйти?"))
            {
                Environment.Exit(0);
            }
        }

        void VH()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM staff where Email='" + LoginBox.Text + "' and Password='" + PasswordBox.Password + "' and Status='Работает';", Cn);
            try
            {
                Cn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    Pass = read[1].ToString();
                    Role = read[7].ToString();
                    name = read[0].ToString();
                    ClassSpravka.NameF = read[0].ToString();
                    switch (read[7].ToString())
                    {
                        case "Администратор":
                            {
                                AdminWindow fm = new AdminWindow();
                                fm.Show();
                                this.Close();
                                break;
                            }
                        case "Менеджер":
                            {
                                ManagerWindow fm = new ManagerWindow(name);
                                fm.Show();
                                this.Close();
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("Логин или пароль неверен!!!");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка подключения к БД!!!" + Ex.ToString());
            }
            finally { Cn.Close(); }
        }
        private void ButtonAvtorizClick(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Password != "")
            {
                if (vhodcount <= 2)
                {
                    VH();
                    vhodcount = vhodcount + 1;
                    ImageCaptcha.Source = Convert2(SetBitmap(Convert.ToInt32(ImageCaptcha.Width), Convert.ToInt32(ImageCaptcha.Height)));
                    if (vhodcount == 2)
                    {
                        ImageCaptcha.Visibility = Visibility.Visible;
                        Cap.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (Cap.Text == text)
                    {
                        VH();
                    }
                    else
                    {
                        MessageBox.Show("Вводимый код не верен!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поле Логина и Пароля");
            }
        }
    }
}
