using System;
using System.Collections.Generic;
using System.Text;
using static UniversityProject.BO.Enums.Enums;

namespace EmrollmentManagment.BO.Models
{
    public class EnrollmentIDStatus
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public EnrollmentStatus? Status { get; set; }
    }
}
