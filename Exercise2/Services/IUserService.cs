using EmployeeManagerment.Models;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public interface IUserService
    {
        public Task<APIResult<List<UserModel>>> GetAll();
        public Task<APIResult<UserModel>> Login(LoginRequest request);
        public Task<APIResult<string>> Register(RegisterRequest request);
    }
}
