using System;
using System.Collections.Generic;
using System.Windows;
using WindowDatabase.ViewModel;
using WindowDatabase.ViewModel.RootVM;

namespace WindowDatabase.Core
{
    public static class WindowManager
    {
        private class PropertyDictionary
        {
            public Window Window { get; set; }
            public IViewModel VM { get; set; }
            public PropertyDictionary(Window window, IViewModel vM)
            {
                Window = window;
                VM = vM;
            }
        }

        public static int MaxCountHistory { get; set; } = 10;

        private static List<IViewModel> _historyWindow = new List<IViewModel>();
        private static Dictionary<System.Type, PropertyDictionary> _windows = new Dictionary<Type, PropertyDictionary>();

        public static int CountHistory => _historyWindow.Count;
        public static void AddHistory(IViewModel viewModel)
        {
            _historyWindow.Add(viewModel);
            if (_historyWindow.Count > MaxCountHistory)
            {
                _historyWindow.RemoveAt(0);
            }
        }
        public static bool PopHistory(out IViewModel? viewModel)
        {
            viewModel = null;
            if (_historyWindow.Count == 0)
                return false;

            viewModel = _historyWindow[_historyWindow.Count - 1];
            _historyWindow.Remove(viewModel);
            return true;
        }


        public static bool Contains(System.Type typeWindowIn)
        {
            return _windows.ContainsKey(typeWindowIn);
        }
        public static bool Contains<In>() where In : Window
        {
            var type = typeof(In);
            return _windows.ContainsKey(type);
        }

        public static Window GetWindow<In>() where In : Window
        {
            var type = typeof(In);
            if (!Contains<In>())
                throw new ArgumentNullException(nameof(type));
            return _windows[type].Window;
        }
        public static IViewModel GetViewModel<In>() where In : Window
        {
            var type = typeof(In);
            if (!Contains<In>())
                throw new ArgumentNullException(nameof(type));
            return _windows[type].VM;
        }

        public static void OpenDialog(Window window, IViewModel vm)
        {
            Add(window, vm);
            window.DataContext = vm;
            window.ShowDialog();
            _windows.Remove(window.GetType());
        }
        public static void Open(Window window, IViewModel vm)
        {
            Add(window, vm);
            window.DataContext = vm;
            window.Show();
        }
        
        public static void Close(Window windowIn)
        {
            var type = windowIn.GetType();
            if (!Contains(type))
                throw new ArgumentNullException(nameof(windowIn));
            var property = _windows[type];
            property.Window.Close();
        }
        public static void Close<In>() where In : Window
        {
            var type = typeof(In);
            if (!Contains<In>())
                throw new ArgumentNullException(nameof(type));
            var property = _windows[type];
            property.Window.Close();
        }
        private static void Add(Window window, IViewModel vm)
        {
            var type = window.GetType();
            if (_windows.ContainsKey(type))
                throw new ArgumentException(nameof(window));
            _windows.Add(type, new PropertyDictionary(window, vm));
        }
    }
}
