using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;

namespace Fleetly.Services.Interfaces
{
    public interface IInspectionService
    {
        Task<List<Inspection>> GetByVehicleIdAsync(string vehicleId);
        Task AddInspectionAsync(string vehicleId, Inspection inspection);
        Task DeleteInspectionAsync(string vehicleId, string inspectionId);
        Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment);
        Task UpdateInspectionAsync(string vehicleId, string inspectionId, UpdateInspectionDto dto);
        Task DeleteCommentAsync(string vehicleId, string inspectionId, string commentId);
        Task UpdateCommentAsync(string vehicleId, string inspectionId, string commentId, UpdateCommentDto dto);

    }
}
