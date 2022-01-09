using AppWebMVC.Data;
using System.Linq;
using System.Collections.Generic;
using AppWebMVC.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class DepartmentService
    {
        private readonly AppWebMVCContext _context;

        public DepartmentService(AppWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(dep => dep.Name).ToListAsync();
        }
    }
}
