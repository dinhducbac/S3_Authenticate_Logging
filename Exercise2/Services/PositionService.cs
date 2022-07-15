using EmployeeManagerment.Models;
using EmployeeManagerment.Respository.PositionRepository;
using Exercise2.EF;
using Exercise2.Entity;
using Exercise2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<APIResult<Position>> CreateAsync(PositionCreateRequest request)
        {
            var apiResult = new APIResult<Position>();
            try
            {
                var positions = new Position();
                positions.Name = request.Name;
                positions = await _positionRepository.CreateAsync(positions);
                var employeeViewModel = await GetPositionByIdAsync(positions.Id);
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

        public async Task<APIResult<Position>> GetPositionByIdAsync(int id)
        {
            var apiResult = new APIResult<Position>();
            var position = await _positionRepository.GetByIdAsync(id);

            if (position == null)
            {
                apiResult.Success = false;
                apiResult.Message = "Cannot find position!";
            }
            else
            {
                apiResult.Success = true;
                apiResult.Message = "Succesful!";
                apiResult.ResultObject = position;
            }
            return apiResult;
        }

        public async Task<APIResult<List<Position>>> GetAllAsync(int pageIndex, int pageSize)
        {
            var positions = await _positionRepository.GetAllAsync(pageIndex,pageSize);
            return new APIResult<List<Position>>() { Success = true, Message = "Successful!", ResultObject = positions };
        }

        public async Task<APIResult<Position>> UpdateAsync(int id, PositionUpdateRequest request)
        {
            var apiResult = new APIResult<Position>();
            try
            {
                var position = await _positionRepository.GetByIdAsync(id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                position.Name = request.Name;
                await _positionRepository.SavechangeAsync();
                var employeeModel = await GetPositionByIdAsync(id);
                apiResult.Success = true;
                apiResult.Message = "Update success";
                apiResult.ResultObject = employeeModel.ResultObject;

            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Update failed, Exeption: {ex.Message}, line {ex.StackTrace}";
            }
            return apiResult;
        }

        public async Task<APIResult<string>> DeleteAsync(int id)
        {
            var apiResult = new APIResult<string>();
            try
            {
                var position = await _positionRepository.GetByIdAsync(id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                await _positionRepository.DeleteAsync(position);
                apiResult.Success = true;
                apiResult.Message = "Delete success!";
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Delete failed, Exeption: {ex.Message}, line {ex.StackTrace}";
            }
            return apiResult;
        }
    }
}
