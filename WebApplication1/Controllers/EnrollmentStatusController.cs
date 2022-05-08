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
    public class EnrollmentStatusController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET api/<controller>
        public IEnumerable<EnrollmentIDStatus> Get()
        {
            return unitOfWork.EnrollmentIDStatusRepository.GetValues();
        }

        // Post api/<controller>/EnrollmentCourseFKey    
        public IEnumerable<EnrollmentIDStatus> Post(EnrollmentCourseFKey _key)
        {
            var enrollmentIDStatusValues = unitOfWork.EnrollmentIDStatusRepository.GetValues(x => x.StudentID == _key.StudentID).FirstOrDefault();
            var enrollmentIDStatus = unitOfWork.EnrollmentIDStatusRepository.Get();     
            // check if it Course already exists , if not - Add new Enrollment Status.   
            if (enrollmentIDStatus == null || enrollmentIDStatus.Count() == 0)
            {                                         
                EnrollmentIDStatus _enrollmentIDStatus = new EnrollmentIDStatus();
                _enrollmentIDStatus.Status = EnrollmentStatus.InProgress;
                _enrollmentIDStatus.EnrollmentID = (enrollmentIDStatus == null || enrollmentIDStatus.Count() == 0) ? 1 : enrollmentIDStatus.Count() + 1;
                _enrollmentIDStatus.StudentID = _key.StudentID;

                enrollmentIDStatus.Add(_enrollmentIDStatus.EnrollmentID, _enrollmentIDStatus);                
            }
            
            var EnrollmentValueIEnum = unitOfWork.EnrollmentIDStatusRepository.GetValues(x => x.StudentID == _key.StudentID);
            return EnrollmentValueIEnum;

        }
    }
}