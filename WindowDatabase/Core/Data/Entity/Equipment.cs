using FileDB.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowDatabase.Core.Data.Entity
{
    public class Equipment
    {
        [SerializeIndex]
        public int Index { get; set; }
        public string Name { get; set; }
        public DateTime DatePay { get; set; }
        public DateTime DateCheck { get; set; }
        public string Specifications { get; set; }
        public Equipment(int index, string name, DateTime datePay, DateTime dateCheck, string specifications)
        {
            Index = index;
            Name = name;
            DatePay = datePay;
            DateCheck = dateCheck;
            Specifications = specifications;
        }
        public Equipment()
        {
            Index = 0;
            Name = string.Empty;
            DatePay = DateTime.Now;
            DateCheck = DateTime.Now;
            Specifications = string.Empty;
        }
    }
}
