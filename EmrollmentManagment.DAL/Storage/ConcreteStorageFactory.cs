using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using UniversityProject.DAL.Storage;

namespace UniversityProject.DAL
{
    /// <summary>
    /// Factory class to get the appropriate ILogger based on what is specified in 
    /// the App.Config file
    /// </summary>
    public class ConcreteStorageFactory : StorageFactory
    {
        #region Member Variables
        // reference to the IStorage object.  Get a reference the first time then keep it
        private static IStorage storage;
        // This variable is used as a lock for thread safety
        private static object lockObject = new object();
        #endregion

        public override IStorage GetStorage()
        {           
            lock (lockObject)
            {
                if (storage == null)
                {                   
                    string class_name = ConfigurationManager.AppSettings["Storage.ClassName"];

                    if ( String.IsNullOrEmpty(class_name))
                        throw new ApplicationException("Missing config data for Storage");
                    // return persistent framework or any other data container.
                    switch (class_name)
                    {
                        case "Dictionary":
                            storage = new DictionaryStorage() as IStorage; return storage;
                        default:
                            throw new ApplicationException(string.Format("class_name '{0}' cannot be created", class_name));
                    }                                   
                }
                return storage;
            }
        }
    }
}
