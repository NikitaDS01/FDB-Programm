using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core;

namespace WindowDatabase.ViewModel.Component
{
    public interface IEntityTable<Type> where Type : class
    {
        ICommand AddCommand { get; }
        ICommand ChangeCommand { get; }
        ICommand DeleteCommand { get; }
        Type SelectedItem { get; set; }
        ObservableCollection<Type> Items { get; }
        string Name => Database.CurrentDatabase.Name;
    }
}
