using EmployeeManagerment.Models;
using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise2.Services
{
    public interface IEmployeeService
    {
        public Task<APIResult<List<EmployeeModel>>> GetAllAsync(int pageIndex, int pageSize);
        public Task<APIResult<EmployeeModel>> GetEmployeeAsync(int id);
        public Task<APIResult<EmployeeModel>> CreateAsync(EmployeeCreateRequest request);
        public Task<APIResult<EmployeeModel>> UpdateAsync(int id, EmployeeUpdateRequest request);
        public Task<APIResult<string>> DeleteAsync(int id);
    }
}
