using EmrollmentManagment.BO.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Models;
using static UniversityProject.BO.Enums.Enums;

namespace UniversityProject.DAL
{
    public class DBSet
    {
       
        private static readonly Dictionary<int, Student> StudentContainer = null;
        private static readonly Dictionary<int, Course> CoursesContainer = null;
        private static readonly Dictionary<int, Enrollment> EnrollmentContainer = null;
        private static readonly Dictionary<int, EnrollmentStateRules> EnrollmentStateContainer = null;
        private static readonly Dictionary<int, EnrollmentIDStatus> EnrollmentIDStatusContainer = null;

        public Dictionary<int, Student> Students { get { return StudentContainer; } }
        public Dictionary<int, Course> Courses { get { return CoursesContainer; } }
        public Dictionary<int, Enrollment> Enrollments { get { return EnrollmentContainer; } } 
        public Dictionary<int, EnrollmentStateRules> EnrollmentStateRuless { get { return EnrollmentStateContainer; } }
        public Dictionary<int, EnrollmentIDStatus> EnrollmentIDStatuss { get { return EnrollmentIDStatusContainer; } }

        static DBSet()
        {
            StudentContainer = new Dictionary<int, Student>();
            StudentContainer.Add(1, new Student { StudentID = 1, FirstName = "David", LastName = "Bazel", EnrollmentDate = DateTime.Parse("2010-09-01") });
            StudentContainer.Add(2, new Student { StudentID = 2, FirstName = "Yogev", LastName = "Levi", EnrollmentDate = DateTime.Parse("2012-09-01") });

            CoursesContainer = new Dictionary<int, Course>();
            CoursesContainer.Add(1050, new Course { CourseID = 1050, Title = "Tirgul1", LectorName = "LectA", Year = 2021, Semester = "A", Mandatory = false, Duration = new TimeSpan(1, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 1, });
            CoursesContainer.Add(4022, new Course { CourseID = 4022, Title = "Economics", LectorName = "LectB", Year = 2021, Semester = "B", Mandatory = false, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 3, });
            CoursesContainer.Add(3141, new Course { CourseID = 3141, Title = "Algorithems", LectorName = "LectC", Year = 2021, Semester = "B", Mandatory = true, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 4, });
            CoursesContainer.Add(4041, new Course { CourseID = 4041, Title = "Macroeconomics", LectorName = "LectC", Year = 2022, Semester = "A", Mandatory = false, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 3, });
            CoursesContainer.Add(1045, new Course { CourseID = 1045, Title = "Algebra", LectorName = "LectB", Year = 2022, Semester = "B", Mandatory = false, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 5, });
            CoursesContainer.Add(1060, new Course { CourseID = 1060, Title = "Tirgul2", LectorName = "LectA", Year = 2022, Semester = "A", Mandatory = true, Duration = new TimeSpan(1, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 1, });
            CoursesContainer.Add(2021, new Course { CourseID = 2021, Title = "Java", LectorName = "LectC", Year = 2020, Semester = "A", Mandatory = true, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(8, 0, 0), Credits = 5, });
            CoursesContainer.Add(2042, new Course { CourseID = 2042, Title = "OperationSystems", LectorName = "LectA", Year = 2020, Semester = "B", Mandatory = true, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(10, 0, 0), Credits = 4, });
            CoursesContainer.Add(2047, new Course { CourseID = 2047, Title = "Hedva", LectorName = "LectB", Year = 2020, Semester = "B", Mandatory = true, Duration = new TimeSpan(2, 0, 0), StartTime = new TimeSpan(9, 0, 0), Credits = 4, });

            EnrollmentIDStatusContainer = new Dictionary<int, EnrollmentIDStatus>();
            EnrollmentContainer = new Dictionary<int, Enrollment>();
            /*
            EnrollmentContainer.Add(1, new Enrollment { EnrollmentID = 1, CourseID = 1050, StudentID = 1, Course = CoursesContainer[1050], Student = StudentContainer[1] });
            EnrollmentContainer.Add(2, new Enrollment { EnrollmentID = 2, CourseID = 2021, StudentID = 1, Course = CoursesContainer[2021], Student = StudentContainer[1] });
            EnrollmentContainer.Add(3, new Enrollment { EnrollmentID = 3, CourseID = 2042, StudentID = 1, Course = CoursesContainer[2042], Student = StudentContainer[1] });
            EnrollmentContainer.Add(4, new Enrollment { EnrollmentID = 4, CourseID = 3141, StudentID = 1, Course = CoursesContainer[3141], Student = StudentContainer[1] });
            EnrollmentContainer.Add(5, new Enrollment { EnrollmentID = 5, CourseID = 1050, StudentID = 2, Course = CoursesContainer[1050], Student = StudentContainer[2] });
            EnrollmentContainer.Add(6, new Enrollment { EnrollmentID = 6, CourseID = 2021, StudentID = 2, Course = CoursesContainer[2021], Student = StudentContainer[2] });
            EnrollmentContainer.Add(7, new Enrollment { EnrollmentID = 7, CourseID = 1060, StudentID = 2, Course = CoursesContainer[1060], Student = StudentContainer[2] });
            EnrollmentContainer.Add(8, new Enrollment { EnrollmentID = 8, CourseID = 4022, StudentID = 2, Course = CoursesContainer[4022], Student = StudentContainer[2] });
            */
            EnrollmentStateContainer = new Dictionary<int, EnrollmentStateRules>();
            EnrollmentStateContainer.Add(1, new EnrollmentStateRules { StageA = EnrollmentStatus.InProgress, StageB = EnrollmentStatus.Completed, Active = true });
            EnrollmentStateContainer.Add(2, new EnrollmentStateRules { StageA = EnrollmentStatus.InProgress, StageB = EnrollmentStatus.Cancelled, Active = true });
            EnrollmentStateContainer.Add(3, new EnrollmentStateRules { StageA = EnrollmentStatus.Completed, StageB = EnrollmentStatus.InProgress, Active = true });
            EnrollmentStateContainer.Add(4, new EnrollmentStateRules { StageA = EnrollmentStatus.Completed, StageB = EnrollmentStatus.Cancelled, Active = true });
            EnrollmentStateContainer.Add(5, new EnrollmentStateRules { StageA = EnrollmentStatus.Completed, StageB = EnrollmentStatus.Payed, Active = true });         
        }

    }
}
