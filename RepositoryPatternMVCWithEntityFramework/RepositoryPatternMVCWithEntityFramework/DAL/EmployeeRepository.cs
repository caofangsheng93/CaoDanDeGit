using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternMVCWithEntityFramework.DAL
{
    public class EmployeeRepository:IEmployeeRepository<Employee>,IDisposable
    {
        private EmployeeManagementDBEntities context;

        public EmployeeRepository(EmployeeManagementDBEntities context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployeeById(object id)
        {
            return context.Employees.Find(id);
        }

        public void InsertEmployee(Employee obj)
        {
             context.Employees.Add(obj);
        }

        public void UpdateEmployee(Employee obj)
        {
            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteEmployee(object id)
        {
           Employee em=  context.Employees.Find(id);
           context.Employees.Remove(em);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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