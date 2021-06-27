using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IShiftAssignmentService
    {
        Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignments(string username,
            ShiftAssignmentParams @params);
        Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignments(int storeId,
            ShiftAssignmentParams @params);
        Task<ShiftAssignment> GetShiftAssignment(int id);
        Task<ShiftAssignment> CreateShiftAssignment(ShiftAssignmentCreate create);
        Task UpdateShiftAssignment(int id, ShiftAssignmentUpdate update);
        Task DeleteShiftAssignment(int id);
    }
}
