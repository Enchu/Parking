using System.Windows.Media.Imaging;

namespace Parking.Entities
{
    public class TableSpravka
    {
        public string First_name { get; internal set; }

        public class Manager
        {
            public string id { get; set; }
            public string datestart { get; set; }
            public string dataend { get; set; }
            public string customer { get; set; }
            public string sector { get; set; }
            public string building { get; set; }
            public string price { get; set; }
        }
        
        public class Building
        {
            public string id { get; set; }
            public string building { get; set; }
            public string address { get; set; }
            public string number_quant { get; set; }
            public BitmapFrame chema { get; set; }
        }
        public class Person
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
    }
}
