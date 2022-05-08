using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityProject.BO.Models
{
    public class EnrollmentCourseFKey
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int EnrollmentID { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEndRegistration { get; set; }
        public bool IsPay { get; set; }

    }
}
