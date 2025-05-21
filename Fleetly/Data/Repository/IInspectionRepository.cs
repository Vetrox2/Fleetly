using Fleetly.Models.Entities;

namespace Fleetly.Data.Repository
{
    public interface IInspectionRepository
    {
        Task<List<Inspection>> GetByVehicleIdAsync(string vehicleId);
        Task<bool> HasValidInspectionAsync(string vehicleId);
        Task<DateTime?> ValidUntilAsync(string vehicleId);
        Task AddInspectionAsync(string vehicleId, Inspection inspection);
        Task UpdateInspectionAsync(string vehicleId, string inspectionId, Inspection inspection);
        Task DeleteInspectionAsync(string vehicleId, string inspectionId);
        Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment);
    }
}