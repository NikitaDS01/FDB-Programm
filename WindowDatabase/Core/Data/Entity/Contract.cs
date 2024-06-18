using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Attribute;

namespace ConsoleTest.Data
{
    public class Contract
    {
        public string Name {get; set;}
        [SerializeIndex]
        public int Id {get; set;}
        public DateTime Begin {get; set;}
        public DateTime End {get; set;}
        public float Cost {get; set;}
        public DateTime CreateRecord {get; set;}
        public DateTime UpdateRecord {get; set;}
        public Contract(string nameIn, int id, DateTime begin,
            DateTime end, float cost, DateTime createIn, DateTime updateIn)
        {
            Name = nameIn;
            Id = id;
            Begin = begin;
            End = end;
            Cost = cost;
            CreateRecord = createIn;
            UpdateRecord = updateIn;
        }
        public Contract()
        {
            Name = string.Empty;
            Id = 0;
            Begin = DateTime.Now;
            End = DateTime.Now;
            Cost = 0;
            CreateRecord = DateTime.Now;
            UpdateRecord = DateTime.Now;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}