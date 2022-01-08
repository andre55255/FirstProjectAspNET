using AppWebMVC.Data;
using System.Linq;
using System.Collections.Generic;
using AppWebMVC.Models;

namespace AppWebMVC.Services
{
    public class DepartmentService
    {
        private readonly AppWebMVCContext _context;

        public DepartmentService(AppWebMVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(dep => dep.Name).ToList();
        }
    }
}
