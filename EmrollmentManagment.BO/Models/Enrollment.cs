using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static UniversityProject.BO.Enums.Enums;

namespace UniversityProject.Models
{
    
    public class Enrollment
    {
        public int NumID { get; set; }
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
      
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}