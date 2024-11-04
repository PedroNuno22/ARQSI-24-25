using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOperationRequestRepository
    {
        // Get an OperationRequest by its unique Id
        Task<OperationRequest> GetByIdAsync(int id);

        // Add a new OperationRequest
        Task AddAsync(OperationRequest operationRequest);

        // Update an existing OperationRequest
        Task UpdateAsync(OperationRequest operationRequest);

        // Delete an OperationRequest by its Id
        Task DeleteAsync(int id);

        // Get all OperationRequests by a specific doctor
        Task<IEnumerable<OperationRequest>> GetByDoctorIdAsync(int doctorId);

        // Get all OperationRequests for a specific patient
        Task<IEnumerable<OperationRequest>> GetByPatientIdAsync(int patientId);

        // Get all OperationRequests within a specific date range
        Task<IEnumerable<OperationRequest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Get all pending OperationRequests (not yet scheduled)
        Task<IEnumerable<OperationRequest>> GetPendingRequestsAsync();

        // Save any changes to the database
        Task SaveChangesAsync();
    }
}