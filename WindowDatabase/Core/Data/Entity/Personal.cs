using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Attribute;

namespace ConsoleTest.Data
{
    public class Personal
    {
        [SerializeIndex, SerializeNameRecord(Name="ФИО")]
        public string Name {get; set;}
        [SerializeNameRecord(Name="Квалификация")]
        public string Skill {get; set;}
        [SerializeNameRecord(Name="Опыт работы в коллективе")]
        public int WorkExpCompany {get; set;}
        [SerializeNameRecord(Name="Опыт работы по специальности")]
        public int WorkExpAll {get; set;}
        [SerializeNameRecord(Name="Дата медосмотра")]
        public DateTime DateMedic {get; set;}

        public Personal(string nameIn, string skillIn, 
            int yearsCompany, int yearsWork, DateTime medicIn)
        {
            Name = nameIn;
            Skill = skillIn;
            WorkExpCompany = yearsCompany;
            WorkExpAll = yearsWork;
            DateMedic = medicIn;
        }
        public Personal()
        {
            Name = string.Empty;
            Skill = string.Empty;
            WorkExpCompany = 0;
            WorkExpAll = 0;
            DateMedic = DateTime.Now;
        }
    }
}