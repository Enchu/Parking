using Parking.Entities.NewFolder1;
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
        public static List<Directory> listSelectTable(ComboBox comboBoxList, string sql, int column)
        {
            List<Directory> listDirectoryTable = Catalog.SelectTable(sql, column);
            for (int i = 0; i < listDirectoryTable.Count; i++)
            {
                comboBoxList.Items.Add(listDirectoryTable[i].Name);
            }
            return listDirectoryTable;
        }

    }
}
