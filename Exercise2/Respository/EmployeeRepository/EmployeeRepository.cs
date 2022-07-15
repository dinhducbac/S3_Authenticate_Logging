using Exercise2.EF;
using Exercise2.Entity;
using Exercise2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerment.Respository.EmployeeRepository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeDBContext _dbContext;
        public EmployeeRepository(EmployeeDBContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<EmployeeModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _dbContext.Employees
                .Include(p => p.Position)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new EmployeeModel(p.Id, p.Name, p.Position.Name))
                .ToListAsync();
        }

        public async Task<EmployeeModel> GetModelByIdAsync(int id)
        {
            return await _dbContext.Employees.Where(emp => emp.Id == id)
                .Include(p => p.Position)
                 .Select(p => new EmployeeModel(p.Id, p.Name, p.Position.Name))
                .FirstOrDefaultAsync();
        }
    }
}
