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
        Task<OperationRequest> UpdateRequestAsync(int id, OperationRequest operationRequest);

        // Method to delete an OperationRequest by its Id
        Task<bool> DeleteRequestAsync(int id);

        // Method to retrieve OperationRequests with optional filters
        Task<IEnumerable<OperationRequest>> GetRequestsAsync(string status, string priority);
    }
}