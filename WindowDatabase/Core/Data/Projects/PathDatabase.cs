using FileDB;
using FileDB.Core.Data;
using FileDB.Core.File;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;
using FileDB.Serialization;
using System;
using System.IO;
using static FileDB.FileDatabase;

namespace WindowDatabase.Core.Data.Projects
{
    public class PathDatabase
    {
        private FileDatabase _database;

        public PathDatabase(string pathIn)
        {
            _database = new FileDatabase(new DirectoryInfo(pathIn)!);
        }
        public string Name => _database.Name;
        public DateTime CreateDateTime => _database.CreateDateTime;
        public string DirectoryPath => _database.Path;
        public FileDatabase Database => _database;
        public override string ToString()
        {
            return DirectoryPath;
        }
    }
}