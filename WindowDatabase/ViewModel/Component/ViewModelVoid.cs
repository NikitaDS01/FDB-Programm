using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelVoid : IViewModel
    {
        public string Name => NameDatabase();
        private string NameDatabase()
        {
            if (Database.IsInit)
                return Database.CurrentDatabase.Name;
            else
                return "Пусто";
        }
    }
}
