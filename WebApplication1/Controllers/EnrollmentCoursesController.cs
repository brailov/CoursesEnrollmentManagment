using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using UniversityProject.BO.Models;
using UniversityProject.DAL.Repository;
using UniversityProject.Models;
using static UniversityProject.BO.Enums.Enums;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EnrollmentCoursesController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/<controller>/QueryStudent/5      
        public IEnumerable<Course> Get(EnrollmentCourseFKey _key)
        {
            var EnrollmentValue = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID);
            var studentCourse = EnrollmentValue.Select(c =>c.Course );         
            return studentCourse;
        }

        // Post api/<controller>/EnrollmentCourseFKey    
        public IEnumerable<Course> Post(EnrollmentCourseFKey _key)
        {
            var enrollmentIDStatusValues = unitOfWork.EnrollmentIDStatusRepository.GetValues(x => x.StudentID == _key.StudentID).FirstOrDefault();
            if (_key != null && _key.EnrollmentID > 0 && _key.StudentID > 0 && _key.IsDelete)       // Delete course
            {              
                var cEnrollmentValue = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID);
                var coursesInEnrollment = unitOfWork.EnrollmentRepository.Get();
                // check if it already exists
                if (cEnrollmentValue != null)
                {                    
                    foreach (Enrollment itemEnrol in cEnrollmentValue)
                    {
                        // course can be deleted in only when Enrollment Status either  Payed or Cancelled.
                        if (enrollmentIDStatusValues.Status != EnrollmentStatus.Payed || enrollmentIDStatusValues.Status != EnrollmentStatus.Cancelled)
                            coursesInEnrollment.Remove(itemEnrol.NumID);
                    }                    
                }
                cEnrollmentValue = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID);
                IEnumerable<Course> enumerable = cEnrollmentValue.Select(c => c.Course);
                return (IEnumerable<Course>)enumerable;
            }            
            else // Add new course
            {
                var enrollment = unitOfWork.EnrollmentRepository.Get();
                var EnrollmentValue = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID && x.CourseID == _key.CourseID ).FirstOrDefault();
                
                // check if it Course already exists
                if (_key.CourseID > 0 && EnrollmentValue == null )
                {
                    var courseEnrollment = unitOfWork.CourseRepository.GetValues(x => x.CourseID == _key.CourseID);
                    // course can be added in only when Enrollment Status either  Payed or Cancelled.
                    if (enrollmentIDStatusValues.Status != EnrollmentStatus.Payed || enrollmentIDStatusValues.Status != EnrollmentStatus.Cancelled)
                    {
                        var studentEnrollment = unitOfWork.StudentRepository.GetValues(x => x.StudentID == _key.StudentID);

                        Enrollment _enrollment = new Enrollment();
                        _enrollment.CourseID = _key.CourseID; 
                        _enrollment.Course = courseEnrollment.FirstOrDefault();
                        _enrollment.NumID = (enrollment == null || enrollment.Count() == 0) ? 1 : enrollment.Count() + 1;
                        _enrollment.EnrollmentID = enrollmentIDStatusValues.EnrollmentID;
                        _enrollment.Student = studentEnrollment.FirstOrDefault();
                        _enrollment.StudentID = _key.StudentID;                      

                        enrollment.Add(_enrollment.NumID, _enrollment);            
                    }                    
                }
            }
            var EnrollmentValueIEnum = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID && x.EnrollmentID == _key.EnrollmentID);
            var newCourseList = EnrollmentValueIEnum.Select(c => c.Course);
            var studentTotalScores = (from x in newCourseList select x.Credits).Sum();
            if (studentTotalScores > 10) // some condition to change status of enrollment to - completed.
            {
                var _enrollmentIDStatusValues = unitOfWork.EnrollmentIDStatusRepository.GetValues(x => x.StudentID == _key.StudentID).FirstOrDefault();
                var EnrollmentStateRules = unitOfWork.EnrollmentStatusRulesRepository.Get().Values;

                var getStateCheck = EnrollmentStateRules.FirstOrDefault(x => x.StageA == _enrollmentIDStatusValues.Status && x.StageB == EnrollmentStatus.Completed);
                if (getStateCheck != null && (bool)getStateCheck.Active)
                    _enrollmentIDStatusValues.Status = EnrollmentStatus.Completed;
            }

            return newCourseList;
        }
      
    
    }
}