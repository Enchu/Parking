using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Parking.Windows;
using Parking.Entities;

namespace Parking
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string textEmpty = string.Empty;
        public static Bitmap SetBitmap(int Width, int Height)//Капча
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Width, Height);
            int Xpos = rnd.Next(0, Width - 100);
            int Ypos = rnd.Next(15, Height - 15);
            Brush[] colors = { Brushes.Black, Brushes.Red, Brushes.RoyalBlue, Brushes.Green };
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.Clear(Color.Gray);
            textEmpty = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                textEmpty += ALF[rnd.Next(ALF.Length)];
            g.DrawString(textEmpty,
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
            var memoryStream = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);
            image.StreamSource = memoryStream;
            image.EndInit();
            return image;
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
        string pass = "", role = "", name = "";

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            if (Msg.ShowQuestion("Вы действительно хотите выйти?"))
            {
                Environment.Exit(0);
            }
        }

        void inputTable()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM staff where Email='" + LoginBox.Text + "' and Password='" + PasswordBox.Password + "' and Status='Работает';", Catalog.connection);
            try
            {
                Catalog.connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                
                if (read.Read())
                {
                    pass = read[1].ToString();
                    role = read[7].ToString();
                    name = read[0].ToString();
                    Catalog.lastName = read[0].ToString();
                    
                    switch (read[7].ToString())
                    {
                        case "Администратор":
                            {
                                Catalog.connection.Close();
                                AdminWindow fm = new AdminWindow();
                                fm.Show();
                                this.Close();
                                break;
                            }
                        case "Менеджер":
                            {
                                Catalog.connection.Close();
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
            catch (Exception error)
            {
                MessageBox.Show("Ошибка подключения к БД!!!" + error.ToString());
            }
            finally 
            { 
                Catalog.connection.Close(); 
            }
        }
        private void ButtonAvtorizClick(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Password != "")
            {
                int attemptsCount = 0;
                if (attemptsCount <= 2)
                {
                    inputTable();
                    attemptsCount = attemptsCount + 1;
                    ImageCaptcha.Source = Convert2(SetBitmap(Convert.ToInt32(ImageCaptcha.Width), Convert.ToInt32(ImageCaptcha.Height)));
                    if (attemptsCount == 2)
                    {
                        ImageCaptcha.Visibility = Visibility.Visible;
                        Cap.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (Cap.Text == textEmpty)
                    {
                        inputTable();
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
