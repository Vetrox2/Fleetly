using Fleetly.Data.Repository;
using Fleetly.Models.Entities;
using Fleetly.Services.Interfaces;

namespace Fleetly.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly IInspectionRepository _inspectionRepo;

        public InspectionService(IInspectionRepository inspectionRepo)
        {
            _inspectionRepo = inspectionRepo;
        }

        public async Task<List<Inspection>> GetByVehicleIdAsync(string vehicleId)
        {
            return await _inspectionRepo.GetByVehicleIdAsync(vehicleId);
        }

        public async Task AddInspectionAsync(string vehicleId, Inspection inspection)
        {
            await _inspectionRepo.AddInspectionAsync(vehicleId, inspection);
        }

        public async Task DeleteInspectionAsync(string vehicleId, string inspectionId)
        {
            await _inspectionRepo.DeleteInspectionAsync(vehicleId, inspectionId);
        }

        public async Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment)
        {
            await _inspectionRepo.AddCommentAsync(vehicleId, inspectionId, comment);
        }
    }
}
