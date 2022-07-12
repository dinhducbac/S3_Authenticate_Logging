using EmployeeManagerment.Models;
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
        public readonly EmployeeDBContext Db;
        public PositionService(EmployeeDBContext dBContext)
        {
            Db = dBContext;
        }

        public async Task<APIResult<Position>> CreateAsync(PositionCreateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<Position>();
            try
            {
                var positions = new Position();
                positions.Name = request.Name;
                await Db.Positions.AddAsync(positions);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeViewModel = await GetPositionByIdAsync(positions.Id);
                apiResult.Success = true;
                apiResult.Message = "Create success!";
                apiResult.ResultObject = employeeViewModel.ResultObject;
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Create failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }

        public async Task<APIResult<Position>> GetPositionByIdAsync(int id)
        {
            var apiResult = new APIResult<Position>();
            var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
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
            var positions = await Db.Positions.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new APIResult<List<Position>>() { Success = true, Message = "Successful!", ResultObject = positions };
        }

        public async Task<APIResult<Position>> UpdateAsync(int id, PositionUpdateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<Position>();
            try
            {
                var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                position.Name = request.Name;
                Db.Positions.Update(position);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeModel = await GetPositionByIdAsync(id);
                apiResult.Success = true;
                apiResult.Message = "Update success";
                apiResult.ResultObject = employeeModel.ResultObject;

            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Update failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }

        public async Task<APIResult<string>> DeleteAsync(int id)
        {
            var apiResult = new APIResult<string>();
            using var transaction = Db.Database.BeginTransaction();
            try
            {
                var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                Db.Positions.Remove(position);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                apiResult.Success = true;
                apiResult.Message = "Delete success!";
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Delete failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }
    }
}
