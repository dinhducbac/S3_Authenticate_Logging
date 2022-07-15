using Exercise2.EF;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Exercise2.Entity;
using EmployeeManagerment.Models;
using System.Runtime.ExceptionServices;
using System;
using EmployeeManagerment.Respository.EmployeeRepository;

namespace Exercise2.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<APIResult<EmployeeModel>> CreateAsync(EmployeeCreateRequest request)
        {
            var apiResult = new APIResult<EmployeeModel>();
            try
            {
                var employee = new Employee();
                employee.Name = request.Name;
                employee.PositionID = request.PositionId;
                employee = await _employeeRepository.CreateAsync(employee);
                var employeeViewModel = await GetEmployeeAsync(employee.Id);
                apiResult.Success = true;
                apiResult.Message = "Create success!";
                apiResult.ResultObject = employeeViewModel.ResultObject;
            }
            catch (Exception ex)
            { 
                apiResult.Success = false;
                apiResult.Message = $"Create failed, Exeption: {ex.Message}, line {ex.StackTrace}";
            }
            return apiResult;
        }

        public async Task<APIResult<string>> DeleteAsync(int id)
        {
            var apiResult = new APIResult<string>();
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                await _employeeRepository.DeleteAsync(employee);
                apiResult.Success = true;
                apiResult.Message = "Delete success!";
            }
            catch(Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Delete failed, Exeption: {ex.Message}, line {ex.StackTrace}";
            }
            return apiResult;
        }

        public async Task<APIResult<List<EmployeeModel>>> GetAllAsync(int pageIndex, int pageSize)
        {
            var employees = await _employeeRepository.GetAllAsync(pageIndex,pageSize);
            return new APIResult<List<EmployeeModel>>() { Success = true, Message = "Success", ResultObject = employees };
        }

        public async Task<APIResult<EmployeeModel>> GetEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return new APIResult<EmployeeModel>() { Success = false, Message = "Can not find employee"};
            }
            var employees = await _employeeRepository.GetModelByIdAsync(id);
            return new APIResult<EmployeeModel>() { Success = true, Message = "Success", ResultObject = employees };
        }

        public async Task<APIResult<EmployeeModel>> UpdateAsync(int id, EmployeeUpdateRequest request)
        {
            var apiResult = new APIResult<EmployeeModel>();
            try
            { 
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                employee.Name = request.Name;
                employee.PositionID = request.PositionId;
                await _employeeRepository.SavechangeAsync();
                var employeeModel = await GetEmployeeAsync(id);
                apiResult.Success = true;
                apiResult.Message = "Update success";
                apiResult.ResultObject = employeeModel.ResultObject;
                
            }
            catch(Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Update failed, Exeption: {ex.Message}, line {ex.StackTrace}";
            }
            return apiResult;
        }
    }
}
