using Parking.Entities.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Entities
{
    internal class SearchTable
    {
        public static int Search(List<Directory> gg, string nn)
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
    }
}
