using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entities.Help
{
    public class Manager
    {
        public string Id { get; set; }
        public string DateStart { get; set; }
        public string DataEnd { get; set; }
        public string Customer { get; set; }
        public string Sector { get; set; }
        public string Building { get; set; }
        public string Price { get; set; }
    }
}
