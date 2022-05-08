using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniversityProject.DAL.Repository;
using UniversityProject.Models;

namespace WebApplication1.Controllers
{
    public class EnrollmentStateRulesController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET api/<controller>
        public IEnumerable<EnrollmentStateRules> Get()
        {
            return unitOfWork.EnrollmentStatusRulesRepository.GetValues();
        }
    }
}