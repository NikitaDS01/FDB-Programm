using FileDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.Projects;

namespace WindowDatabase.Core
{
    public static class Database
    {
        private static FileDatabase? _database = null;
        private static bool _isInit = false;

        public static bool IsInit => _isInit;
        public static FileDatabase CurrentDatabase
        {
            get
            {
                if( _database == null )
                    throw new Exception("База данных не была загружена");
                return _database;
            }
        }
        public static void Create(FileDatabase databaseIn, FileDatabase.PropertyDatabase property)
        {
            databaseIn.CreateDatabase(property);
        }
        public static void Create(FileDatabase databaseIn, string nameIn)
        {
            databaseIn.CreateDatabase(new FileDatabase.PropertyDatabase() { NameDatabase = nameIn,                                                         CreateDatabase = DateTime.Now});
        }
        public static void Init(FileDatabase databaseIn)
        {
            databaseIn.Initialize();
        }
        public static void Open(FileDatabase databaseIn)
        {
            _database = databaseIn;
            _isInit = true;
        }
        public static void Close()
        {
            _database = null;
            _isInit = false;
        }
    }
}
