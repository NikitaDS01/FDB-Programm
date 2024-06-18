using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Data.Projects
{
    public class ListPathDatabase
    {
        private List<PathDatabase> _databases;
        public ListPathDatabase()
        {
            _databases = new List<PathDatabase>();
            
        }
        public ListPathDatabase(IEnumerable<PathDatabase> databasesIn)
        {
            _databases = new List<PathDatabase>(databasesIn);
            for (int index = 0; index < _databases.Count; index++)
            {
                Database.Init(_databases[index].Database);
            }
        }
        public IReadOnlyList<PathDatabase> Databases => _databases;
        public void Add(PathDatabase database)
        {
            _databases.Add(database);
        }
        public void Remove(PathDatabase database)
        {
            _databases.Remove(database);
        }
    }
}
