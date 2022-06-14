using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Parking.Entities.Help
{
    public class Building
    {
        public string id { get; set; }
        public string building { get; set; }
        public string address { get; set; }
        public string number_quant { get; set; }
        public BitmapFrame chema { get; set; }
    }
}
