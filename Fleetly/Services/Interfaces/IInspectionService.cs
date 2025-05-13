using Fleetly.Models.Entities;

namespace Fleetly.Services.Interfaces
{
    public interface IInspectionService
    {
        Task<List<Inspection>> GetByVehicleIdAsync(string vehicleId);
        Task AddInspectionAsync(string vehicleId, Inspection inspection);
        Task DeleteInspectionAsync(string vehicleId, string inspectionId);
        Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment);
    }
}
