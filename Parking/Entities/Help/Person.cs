using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Parking.Entities.Help
{
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
