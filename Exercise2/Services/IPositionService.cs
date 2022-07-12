using EmployeeManagerment.Models;
using Exercise2.Entity;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public interface IPositionService
    {
        public Task<APIResult<List<Position>>> GetAllAsync(int pageInde, int pageSize);
        public Task<APIResult<Position>> CreateAsync(PositionCreateRequest request);
        public Task<APIResult<Position>> GetPositionByIdAsync(int id);
        public Task<APIResult<Position>> UpdateAsync(int id, PositionUpdateRequest request);
        public Task<APIResult<string>> DeleteAsync(int id);
    }
}
