using EmployeeManagerment.Models;
using Exercise2.Models;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public interface IUserService
    {
        public Task<APIResult<UserViewModel>> Login(LoginRequest request);
        public Task<APIResult<string>> Register(RegisterRequest request);
    }
}
