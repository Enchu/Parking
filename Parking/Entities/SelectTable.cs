using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace Parking.Entities
{
    public class SelectTable
    {
        public static List<ClassSpravka.Spravka> ff(ComboBox fs, string sql, string ls2, int Column)
        {
            List<ClassSpravka.Spravka> fg = ClassSpravka.SelectTB(sql, ls2, Column);
            for (int i = 0; i < fg.Count; i++)
            {
                fs.Items.Add(fg[i].Name);
            }
            return fg;
        }

    }
}
