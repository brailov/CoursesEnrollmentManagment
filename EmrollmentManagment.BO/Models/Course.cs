using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityProject.Models
{
    public class Course
    {       
        public int CourseID { get; set; }      
        public string Title { get; set; }
        public string LectorName { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
        public bool Mandatory { get; set; }        
        public int Credits { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}