using EmrollmentManagment.BO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityProject.Models;

namespace UniversityProject.DAL.Repository
{
    public class UnitOfWork : IDisposable
    {        
        private GenericRepository<Student> studentRepository;
        private GenericRepository<Course> courseRepository;
        private GenericRepository<Enrollment> enrollmentRepository;
        private GenericRepository<EnrollmentStateRules> enrollmentStatusRulesRepository;
        private GenericRepository<EnrollmentIDStatus> enrollmentIDStatusRepository;

        public GenericRepository<Student> StudentRepository
        {
            get
            {

                if (this.studentRepository == null)
                {
                    this.studentRepository = new GenericRepository<Student>();
                }
                return studentRepository;
            }
        }

        public GenericRepository<Course> CourseRepository
        {
            get
            {

                if (this.courseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>();
                }
                return courseRepository;
            }
        }

        public GenericRepository<Enrollment> EnrollmentRepository
        {
            get
            {

                if (this.enrollmentRepository == null)
                {
                    this.enrollmentRepository = new GenericRepository<Enrollment>();
                }
                return enrollmentRepository;
            }
        }

        public GenericRepository<EnrollmentStateRules> EnrollmentStatusRulesRepository
        {
            get
            {

                if (this.enrollmentStatusRulesRepository == null)
                {
                    this.enrollmentStatusRulesRepository = new GenericRepository<EnrollmentStateRules>();
                }
                return enrollmentStatusRulesRepository;
            }
        }

        public GenericRepository<EnrollmentIDStatus> EnrollmentIDStatusRepository
        {
            get
            {

                if (this.enrollmentIDStatusRepository == null)
                {
                    this.enrollmentIDStatusRepository = new GenericRepository<EnrollmentIDStatus>();
                }
                return enrollmentIDStatusRepository;
            }
        }

        public void Save()
        {
            //context.SaveChanges(); ?
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
