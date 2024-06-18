using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.Projects;

namespace WindowDatabase
{
    public static class Settings
    {
        private const string FILE_NAME = "PathDatabase.txt";
        private static int _countRecord = 0;
        public static PathDatabase[] LoadPath()
        {
            if (!File.Exists(FILE_NAME))
                return new PathDatabase[0];

            var lines = System.IO.File.ReadAllLines(FILE_NAME);
            var paths = new List<PathDatabase>();
            for (int index = 0; index < lines.Length; index++)
            {
                if (!Directory.Exists(lines[index]))
                    continue;
                paths.Add(new PathDatabase(lines[index]));
            }
            _countRecord = paths.Count;
            return paths.ToArray();
        }
        public static void SavePath(IEnumerable<PathDatabase> pathsIn)
        {
            if (_countRecord == pathsIn.Count()) 
                return;

            System.IO.File.WriteAllLines(FILE_NAME,
                pathsIn.Select(fdb => (fdb.Database.Path)));
        }

        public static string TableContract => "Договор";
        public static string TableCustomer => "Заказчик";

        public static string TableProject => "Проект";

        public static string TableGroup => "Полевой отряд";
        public static string TableChief => "Начальник отряда";
        public static string TableDriver => "Водители";
        public static string TableEngineer => "Инженерно-технические рабоники";
        public static string TableWorker => "Рабочие";
        public static string TableSupervisor => "Супервайзер";
        public static string TableMeasuring => "Измерительное оборудование";
        public static string TableGenerator => "Генераторное оборудование";
        public static string TableTelemetry => "Телеметрическое оборудование";
        public static string TableEquipments => "Набор оборудования";

        public static string TableMethodology => "Методика";
    }
}
