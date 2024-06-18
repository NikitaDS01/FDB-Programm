using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Attribute;
using FileDB.Core.Data;

namespace ConsoleTest.Data
{
    public class PersonalGroup
    {
        [SerializeIndex]
        public int Index {get; set;}
        
        public RecordLink Chief {get; set;}
        public RecordLink Driver {get; set;}
        public RecordLink Worker {get; set;}
        public RecordLink Engineer {get; set;}

        public PersonalGroup(int indexIn, RecordLink chiefIn,
            RecordLink driverIn, RecordLink workerIn, RecordLink engineerIn)
        {
            Chief = chiefIn;
            Driver = driverIn;
            Worker = workerIn;
            Engineer = engineerIn;
            Index = indexIn;
        }
        public PersonalGroup()
        {
            Chief = RecordLink.Empty;
            Driver = RecordLink.Empty;
            Worker = RecordLink.Empty;
            Engineer = RecordLink.Empty;
            Index = 0;
        }
    }
}