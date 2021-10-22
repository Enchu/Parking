using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Parking
{
    public class ClassMysql
    {
        public static MySqlConnection Cn = new MySqlConnection("server = 127.0.0.1; user id = root; port=3310;persistsecurityinfo=True;database=newcry;password=;convert zero datetime=True");
    }
}
