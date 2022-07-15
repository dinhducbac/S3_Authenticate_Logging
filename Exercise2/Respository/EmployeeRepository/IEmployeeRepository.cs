using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Respository.EmployeeRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<List<EmployeeModel>> GetAllAsync(int pageIndex, int pageSize);
        public Task<EmployeeModel> GetModelByIdAsync(int id);
    }
}
