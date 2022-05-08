using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using UniversityProject.DAL.Repository;
using UniversityProject.Models;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CoursesController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET api/<controller>
        public IEnumerable<Course> Get()
        {          
            var coursesList = unitOfWork.CourseRepository.GetValues();
            var reorderedCoursesList = from row in coursesList
                                       orderby row.Year ascending,row.Semester ascending, row.Mandatory descending
                                       select row;
            return reorderedCoursesList;                                                           
        }
      
    }
}