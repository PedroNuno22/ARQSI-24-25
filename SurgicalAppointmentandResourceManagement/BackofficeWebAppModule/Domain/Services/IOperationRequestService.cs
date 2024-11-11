using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services
{
    public interface IOperationRequestService
    {
        // Method to create a new OperationRequest from domain values
        Task<OperationRequest> CreateRequestAsync(OperationRequest operationRequest);

        // Method to update an existing OperationRequest
        Task<OperationRequest> UpdateRequestAsync(Guid id, OperationRequest operationRequest);

        // Method to delete an OperationRequest by its Id
        Task<bool> DeleteRequestAsync(Guid id);

        // Method to retrieve OperationRequests with optional filters
        Task<IEnumerable<OperationRequest>> GetRequestsAsync(string status, int priority);

        // Method to retrieve a OperationRequest with a specific id
        Task<OperationRequest> GetByIdAsync(Guid id);

    }
}