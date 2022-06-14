using MySql.Data.MySqlClient;
using Parking.Entities.NewFolder1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Parking
{
    public class Catalog
    {
        public static MySqlConnection connection = new MySqlConnection("server = 127.0.0.1; user id = root; port=3310;persistsecurityinfo=True;database=dp;password=;convert zero datetime=True");
        //public static MySqlConnection Cn = new MySqlConnection("server = 10.0.7.240; user = user5; port=3306;persistsecurityinfo=True;database=user5;password=wsruser5;convert zero datetime=True");
        //public static MySqlConnection Cn = new MySqlConnection("server =185.92.149.80; user id = admin_leha; port=3306;persistsecurityinfo=True;database=admin_db;charset=utf8;password=hZIKqXWowf;convert zero datetime=True");
        public static string lastName;
        public static List<Directory> SelectTable(string sql, int column)
        {
            var listTable = new List<Directory>();
            try
            {
                listTable.Clear();

                connection.Open();
                var commandTable = new MySqlCommand(sql, connection);
                MySqlDataReader dataReader = commandTable.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                        listTable.Add(new Directory { Name = dataReader[column].ToString() });
                }
            }
            catch (Exception error)
            { 
                Msg.ShowError($"Ошибка подключения к базе данных:{error}"); 
            }
            finally 
            { 
                connection.Close(); 
            }

            return listTable;
        }

        public static DataTable LoadWindow(string sql)
        {
            var dataTable = new DataTable();
            try
            {
                connection.Open();

                var commandTable = new MySqlCommand($"SELECT * FROM {sql} ", connection);//var commandTable = new MySqlCommand(sql, connection);
                dataTable.Load(commandTable.ExecuteReader());

                connection.Close();
            }
            catch (Exception error)
            {
                Msg.ShowError($"Ошибка подключения к базе данных:{error}");
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        public static void mysqlCommand(string sql)
        {
            try
            {
                connection.Open();
                var commandTable = new MySqlCommand(sql, connection);
                commandTable.ExecuteReader();
                MessageBox.Show("Данные успешно обновлены");
            }
            catch (Exception error) 
            { 
                MessageBox.Show($"Ошибка подключения к бд:{error}"); 
            }
            finally 
            { 
                connection.Close(); 
            }
        }

        public static void mysqlTableCommand(string sqlSelectCommand,string sqlCommand)
        {
            connection.Open();

            MySqlCommand nameSearchTable = new MySqlCommand(sqlSelectCommand, connection);
            var nameTable = nameSearchTable.ExecuteScalar();

            connection.Close();

            if ((long)nameTable == 0)
            {
                mysqlCommand(sqlCommand);
            }
            else 
            { 
                Msg.ShowError("Невозможно выполнить повторные данные повторные данные"); 
            }
        }
    }
}
