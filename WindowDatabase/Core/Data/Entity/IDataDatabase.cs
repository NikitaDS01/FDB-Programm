using System;
using System.IO;

namespace WindowDatabase.Core.Data.Entity
{
    public interface IDataDatabase
    {
        DateTime CreateRecord { get; }
        DateTime UpdateRecord { get; }
    }
}
