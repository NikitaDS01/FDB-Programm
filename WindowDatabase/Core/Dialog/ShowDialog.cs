using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowDatabase.Core.Dialog
{
    public static class ShowDialog
    {
        public static void Error(string messageIn)
        {
            MessageBox.Show(messageIn, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Warning(string messageIn)
        {
            MessageBox.Show(messageIn, "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static void Info(string messageIn)
        {
            MessageBox.Show(messageIn, "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
