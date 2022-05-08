using EmrollmentManagment.BO.Models;
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
    public class EnrollmentController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/<controller>
        public IEnumerable<Enrollment> Get()
        {
            return unitOfWork.EnrollmentRepository.GetValues();
        }


        // GET api/<controller>/QueryStudent/5
        [Route("api/Enrollment/QueryStudent/{id}")]
        public IEnumerable<Enrollment> QueryStudent(int id)
        {           
            return unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == id);
        }


        // POST api/<controller>/EnrollmentCourseFKey    
        public IEnumerable<Enrollment> Post(EnrollmentCourseFKey _key)
        {
            var enrollmentIDStatusValues = unitOfWork.EnrollmentIDStatusRepository.GetValues(x => x.StudentID == _key.StudentID).FirstOrDefault();           
            var EnrollmentStateRules = unitOfWork.EnrollmentStatusRulesRepository.Get().Values;

            if (_key != null && _key.EnrollmentID > 0 && _key.IsDelete)     // Delete enrollment
            {
                if (enrollmentIDStatusValues != null)
                {                                          
                    // Enrollment can be deleted only when Enrollment - Status either  InProgress or Completed (but no payed).
                    if (enrollmentIDStatusValues.Status != EnrollmentStatus.Payed && (enrollmentIDStatusValues.Status == EnrollmentStatus.InProgress || enrollmentIDStatusValues.Status == EnrollmentStatus.Completed))
                        enrollmentIDStatusValues.Status = EnrollmentStatus.Cancelled;                                          
                }
            }
            else if (_key != null && _key.EnrollmentID > 0 && _key.StudentID > 0 && _key.IsEndRegistration)     //  End Registration
            {                
                // check if it already exists
                if (enrollmentIDStatusValues != null)
                {                   
                    // loop rows in state table to check if the "rule" is valid.                        
                    var getStateCheck = EnrollmentStateRules.FirstOrDefault(x => x.StageA == enrollmentIDStatusValues.Status && x.StageB == EnrollmentStatus.Completed);
                    if (getStateCheck != null && (bool)getStateCheck.Active)
                        enrollmentIDStatusValues.Status = EnrollmentStatus.Completed;                    
                }               
            }
            else if (_key != null && _key.StudentID > 0 && _key.IsPay)     // change status to - payed. this can't be reversed. 
            {
                // check if it already exists
                if (enrollmentIDStatusValues != null)
                {                    
                    var getStateCheck = EnrollmentStateRules.FirstOrDefault(x => x.StageA == enrollmentIDStatusValues.Status && x.StageB == EnrollmentStatus.Payed);
                    if (getStateCheck != null && (bool)getStateCheck.Active)
                        enrollmentIDStatusValues.Status = EnrollmentStatus.Payed;                    
                }               
            }
            else // Add new enrollment
            {                               
                // student can't register himself more then once.
                if (enrollmentIDStatusValues == null)
                {
                    var enrollmentIDStatus = unitOfWork.EnrollmentIDStatusRepository.Get();

                    // Create new Enrollment object
                    EnrollmentIDStatus _enrollmentIDStatus = new EnrollmentIDStatus();
                    _enrollmentIDStatus.StudentID = _key.StudentID;
                    _enrollmentIDStatus.EnrollmentID = (enrollmentIDStatus == null || enrollmentIDStatus.Count() == 0) ? 1 : enrollmentIDStatus.Count() + 1;
                    _enrollmentIDStatus.Status = EnrollmentStatus.InProgress;
                    enrollmentIDStatus.Add(_enrollmentIDStatus.EnrollmentID, _enrollmentIDStatus);                  
                }
            }
            var enrollment = unitOfWork.EnrollmentRepository.GetValues(x => x.StudentID == _key.StudentID);
            return enrollment;                           
        }

        // DELETE api/<controller>/EnrollmentCourseFKey    
        public void Delete(EnrollmentCourseFKey _key)
        {
            if (_key != null && _key.EnrollmentID >0 && _key.IsDelete)
            {
                var enrollmentDict = unitOfWork.EnrollmentRepository.Get();
                var res = enrollmentDict.Remove(_key.EnrollmentID);
            }
        }

    }
}