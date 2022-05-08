using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityProject.DAL.Storage
{
    // <summary>
    /// Driver class to adapts calls from ILogger to work with a log4net backend
    /// </summary>
    internal class DictionaryStorage : IStorage
    {
        public DictionaryStorage() { }            

        public DBSet GetData()
        {                     
            return new DBSet();

        }
    }
}
