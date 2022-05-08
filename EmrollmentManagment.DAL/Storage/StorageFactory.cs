using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityProject.DAL.Storage
{
    public abstract class StorageFactory
    {
        public abstract IStorage GetStorage();

    }
}
