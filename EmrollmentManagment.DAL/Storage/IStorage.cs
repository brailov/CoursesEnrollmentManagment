using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityProject.DAL.Storage
{   
    /// <summary>
    /// Defines the common logging interface specification
    /// </summary>
    public interface IStorage
    {
        DBSet GetData();
    }
}
