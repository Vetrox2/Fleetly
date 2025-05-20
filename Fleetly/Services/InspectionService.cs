using Fleetly.Data.Repository;
using Fleetly.Models.Dtos;
using Fleetly.Models.Entities;
using Fleetly.Services.Interfaces;
using System;
using System.Linq;

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

         public async Task UpdateInspectionAsync(string vehicleId, string inspectionId, UpdateInspectionDto dto)
        {
            //coś tu nie tak jest daty ustawia na jedna do tylu ale czy przeszła inspekcja działa
            var inspections = await _inspectionRepo.GetByVehicleIdAsync(vehicleId);
            var inspection = inspections.FirstOrDefault(i => i.Id == inspectionId);
            if (inspection == null) throw new KeyNotFoundException("Inspection not found");

            inspection.DatePerformed = dto.DatePerformed;
            inspection.IsPassed = dto.IsPassed;
            inspection.ValidUntil = dto.ValidUntil;

            await _inspectionRepo.UpdateInspectionAsync(vehicleId, inspectionId, inspection);
        }

        public async Task AddCommentAsync(string vehicleId, string inspectionId, Comment comment)
        {
            await _inspectionRepo.AddCommentAsync(vehicleId, inspectionId, comment);
        }

        public async Task DeleteCommentAsync(string vehicleId, string inspectionId, string commentId)
        {
            var inspections = await _inspectionRepo.GetByVehicleIdAsync(vehicleId);
            var inspection = inspections.FirstOrDefault(i => i.Id == inspectionId);
            if (inspection == null) throw new KeyNotFoundException("Inspection not found");

            var comment = inspection.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) throw new KeyNotFoundException("Comment not found");

            inspection.Comments.Remove(comment);
            await _inspectionRepo.UpdateInspectionAsync(vehicleId, inspectionId, inspection);
        }

        public async Task UpdateCommentAsync(string vehicleId, string inspectionId, string commentId, UpdateCommentDto dto)
        {
            var inspections = await _inspectionRepo.GetByVehicleIdAsync(vehicleId);
            var inspection = inspections.FirstOrDefault(i => i.Id == inspectionId);
            if (inspection == null) throw new KeyNotFoundException("Inspection not found");

            var comment = inspection.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) throw new KeyNotFoundException("Comment not found");

            comment.Text = dto.Text;
            comment.Timestamp = DateTime.UtcNow;

            await _inspectionRepo.UpdateInspectionAsync(vehicleId, inspectionId, inspection);
        }
    }
}
