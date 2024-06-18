using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FileDB.Core.Attribute;
using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using WindowDatabase;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.Component;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;

namespace ConsoleTest.Data
{
    public class Project  
    {

        [SerializeIndex]
        public string Name {get; set;}
        public RecordLink Customer {get; set;}
        public RecordLink Contract {get; set;}
        public DateTime CreateRecord { get; set; }
        public DateTime UpdateRecord { get; set; }
        public Project(string nameIn, RecordLink customerIn, RecordLink contractIn, DateTime createIn, DateTime updateIn)
        {
            Name = nameIn;
            Customer = customerIn;
            Contract = contractIn;
            CreateRecord = createIn;
            UpdateRecord = updateIn;
        }
        public Project()
        {
            Name = string.Empty;
            Customer = RecordLink.Empty;
            Contract = RecordLink.Empty;
            CreateRecord = DateTime.Now;
            UpdateRecord = DateTime.Now; 
        }
        public override string ToString()
        {
            return Name;
        }
    }
}