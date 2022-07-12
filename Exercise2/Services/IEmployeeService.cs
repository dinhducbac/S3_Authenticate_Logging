using EmployeeManagerment.Models;
using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercise2.Services
{
    public interface IEmployeeService
    {
        public Task<APIResult<List<EmployeeViewModel>>> GetAllAsync(int pageIndex, int pageSize);
        public Task<APIResult<EmployeeViewModel>> GetEmployeeAsync(int id);
        public Task<APIResult<EmployeeViewModel>> CreateAsync(EmployeeCreateRequest request);
        public Task<APIResult<EmployeeViewModel>> UpdateAsync(int id, EmployeeUpdateRequest request);
        public Task<APIResult<string>> DeleteAsync(int id);
    }
}
