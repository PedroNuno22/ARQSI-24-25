using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services
{
    public class OperationRequestService : IOperationRequestService
    {
        private readonly IOperationRequestRepository _operationRequestRepository;
        private readonly OperationRequestMapper _mapper;

        public OperationRequestService(IOperationRequestRepository operationRequestRepository)
        {
            _operationRequestRepository = operationRequestRepository;
        }

        public async Task<OperationRequest> CreateRequestAsync(OperationRequest operationRequest)
        {
            // Add operationRequest directly, assuming it was already mapped
            await _operationRequestRepository.AddAsync(operationRequest);
            await _operationRequestRepository.SaveChangesAsync();
            return operationRequest;
        }

        public async Task<OperationRequest> UpdateRequestAsync(int id, OperationRequest operationRequest)
        {
            var existingRequest = await _operationRequestRepository.GetByIdAsync(id);
            if (existingRequest == null || existingRequest.DoctorId != operationRequest.DoctorId)
                return null;

            // Update fields based on input operationRequest
            existingRequest.Priority = operationRequest.Priority;
            existingRequest.Deadline = operationRequest.Deadline;
            await _operationRequestRepository.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<bool> DeleteRequestAsync(int id)
        {
            var operationRequest = await _operationRequestRepository.GetByIdAsync(id);
            if (operationRequest == null || operationRequest.Status == "Scheduled")
                return false;

            await _operationRequestRepository.DeleteAsync(operationRequest);
            await _operationRequestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OperationRequest>> GetRequestsAsync(string status, string priority)
        {
            return await _operationRequestRepository.GetByStatusAndPriorityAsync(status, priority);
        }
    }
}