using Exercise2.EF;
using Exercise2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerment.Respository.PositionRepository
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        private readonly EmployeeDBContext _dbContext;
        public PositionRepository(EmployeeDBContext context) : base(context)
        {
            _dbContext = context;
        }

        public Task<List<Position>> GetAllAsync(int pageIndex, int pageSize)
        {
            return _dbContext.Positions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        
    }
}
