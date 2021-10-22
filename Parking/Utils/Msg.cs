using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parking
{
    public class Msg
    {
        public static void ShowInfo(string msg)
        {
            MessageBox.Show(msg,"Информация",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static bool ShowQuestion(string msg)
        {
            return MessageBox.Show(msg, "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
