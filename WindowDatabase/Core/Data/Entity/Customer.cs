using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FileDB.Core.Attribute;
using WindowDatabase.Core.Data.Entity;

namespace ConsoleTest.Data
{
    public class Customer : INotifyPropertyChanged, IDataDatabase
    {
        [SerializeIndex]
        public string Name {get; set;}
        [SerializeNameRecord(Name ="Юр.адрес")]
        public string JurAddress {get; set;}
        [SerializeNameRecord(Name="Физ.адрес")]
        public string Address {get; set;}
        public string INN {get; set;}
        public string KPK {get; set;}
        public string PC {get; set;}
        public string KOPP_C {get; set;}
        [SerializeNameRecord(Name="Представитель")]
        public string Delegate {get; set;}
        public string Phone {get; set;}
        public string AddressSite {get; set;}
        public DateTime CreateRecord {get; set;}
        public DateTime UpdateRecord {get; set;}

        public Customer(string nameIn, string address1In, string address2In,
            string inn, string kpk, string pc, string kopp_c, string delegateIn,
            string phone, string addressSiteIn, DateTime createIn, DateTime updateIn)
        {
            Name = nameIn;
            JurAddress = address1In;
            Address = address2In;
            INN = inn;
            KPK = kpk;
            PC = pc;
            KOPP_C = kopp_c;
            Delegate = delegateIn;
            Phone = phone;
            AddressSite = addressSiteIn;
            CreateRecord = createIn;
            UpdateRecord = updateIn;
        }
        public Customer()
        {
            Name = string.Empty;
            JurAddress = string.Empty;
            Address = string.Empty;
            INN = string.Empty;
            KPK = string.Empty;
            PC = string.Empty;
            KOPP_C = string.Empty;
            Delegate = string.Empty;
            Phone = string.Empty;
            AddressSite = string.Empty;
            CreateRecord = DateTime.Now;
            UpdateRecord = DateTime.Now;
        }
        public override string ToString()
        {
            return Name + ' ' + Delegate;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}