using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Parking
{
    public class ClassSpravka
    {
        public static MySqlConnection Cn = new MySqlConnection("server = 127.0.0.1; user id = root; port=3310;persistsecurityinfo=True;database=dp;password=;convert zero datetime=True");
        //public static MySqlConnection Cn = new MySqlConnection("server = 10.0.7.240; user = user5; port=3306;persistsecurityinfo=True;database=user5;password=wsruser5;convert zero datetime=True");
        //public static MySqlConnection Cn = new MySqlConnection("server =185.92.149.80; user id = admin_leha; port=3306;persistsecurityinfo=True;database=admin_db;charset=utf8;password=hZIKqXWowf;convert zero datetime=True");
        public class Spravka
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public static List<Spravka> SelectTB(string sql, string ls2, int Column)
        {
            List<Spravka> ListAll = new List<Spravka>();
            try
            {
                ListAll.Clear();
                Cn.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand(sql, Cn);
                MySqlDataReader DR = cmd.ExecuteReader();
                if (DR.HasRows)
                {
                    while (DR.Read())
                        ListAll.Add(new Spravka { Name = DR[Column].ToString() });
                }
            }
            catch (Exception)
            {
            }
            finally { Cn.Close(); }
            return ListAll;
        }
        public static string NameF;
        public static DataTable LoadVV(string Cm)
        {
            DataTable DT = new DataTable();
            try
            {
                Cn.Open();
                MySqlCommand Cmd = new MySqlCommand(Cm, Cn);
                DT.Load(Cmd.ExecuteReader());
                Cn.Close();
            }
            catch (Exception) { Cn.Close(); }
            return DT;
        }
        public static int Search(List<ClassSpravka.Spravka> gg, string nn)
        {
            for (int i = 0; i < gg.Count; i++)
            {
                if (gg[i].Name == nn)
                {
                    return gg[i].Id;
                }
            }
            return 0;
        }
        public static void MysqlLi(string Cm)
        {
            try
            {
                Cn.Open();
                MySqlCommand Cmd = new MySqlCommand(Cm, Cn);
                Cmd.ExecuteReader();
                MessageBox.Show("Данные успешно обновлены");
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка подключения к бд:{ex}"); }
            finally { Cn.Close(); }
        }
        public static DataTable LoadWindow(string Sql)
        {
            DataTable DT = new DataTable();
            try
            {
                Cn.Open();
                MySqlCommand cmd = new MySqlCommand(Sql, Cn);
                DT.Load(cmd.ExecuteReader());
                Cn.Close();
            }
            catch (Exception ex)
            {
                Msg.ShowError($"Ошибка подключения к базе данных:{ex}");
            }
            finally { Cn.Close(); }
            return DT;
        }
        public static void Prover(string NN,string Cm)
        {
            Cn.Open();
            MySqlCommand NameS = new MySqlCommand(NN, Cn);
            var Im = NameS.ExecuteScalar();
            Cn.Close();
            if ((long)Im == 0)
            {
                try
                {
                    Cn.Open();
                    MySqlCommand Cmd = new MySqlCommand(Cm, Cn);
                    Cmd.ExecuteNonQuery();
                    Cn.Close();
                    MessageBox.Show("Данные успешно обновлены");
                }
                catch (Exception ex) { Msg.ShowError($"Ошибка подключения к бд{ex}"); }
                finally { Cn.Close(); }
            }
            else { Msg.ShowError("Невозможно выполнить повторные данные повторные данные"); }
        }
    }
}
