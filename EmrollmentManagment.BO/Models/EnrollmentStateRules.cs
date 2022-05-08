using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static UniversityProject.BO.Enums.Enums;

namespace UniversityProject.Models
{
    public class EnrollmentStateRules
    {
     
        public int StagID { get; set; }
        public EnrollmentStatus StageA { get; set; }
        public EnrollmentStatus StageB { get; set; }
        public bool? Active { get; set; }
    }
}