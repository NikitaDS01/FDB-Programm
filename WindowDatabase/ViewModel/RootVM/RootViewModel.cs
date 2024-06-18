using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WindowDatabase.Core;

namespace WindowDatabase.ViewModel.RootVM
{
    public class RootViewModel : INotifyPropertyChanged, IViewModel
    {
        private IViewModel _currentViewModel;
        public RootViewModel(IViewModel viewModelIn)
        {
            _currentViewModel = viewModelIn;
        }

        public IViewModel CurrentContentVM
        {
            get { return _currentViewModel; }
            set 
            { 
                _currentViewModel = value; 
                OnPropertyChanged();
            }
        }
        public void ChangeVM(IViewModel viewModelIn)
        {
            CurrentContentVM = viewModelIn;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
