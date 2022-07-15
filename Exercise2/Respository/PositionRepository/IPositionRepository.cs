using Exercise2.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Respository.PositionRepository
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        public Task<List<Position>> GetAllAsync(int pageIndex, int pageSize);
    }
}
