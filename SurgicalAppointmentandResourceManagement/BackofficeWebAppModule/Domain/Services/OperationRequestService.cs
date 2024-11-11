using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Domain.Services
{
    public class OperationRequestService : IOperationRequestService
    {
        private readonly IOperationRequestRepository _operationRequestRepository;

        public OperationRequestService(IOperationRequestRepository operationRequestRepository)
        {
            _operationRequestRepository = operationRequestRepository;
        }

        public async Task<OperationRequest> CreateRequestAsync(OperationRequest operationRequest)
        {
            if (operationRequest == null)
                throw new ArgumentNullException(nameof(operationRequest));

            if (operationRequest.Deadline <= DateTime.Now)
                throw new InvalidOperationException("The deadline must be in the future.");

            operationRequest.Status = "Requested";
            await _operationRequestRepository.AddAsync(operationRequest);
            await _operationRequestRepository.SaveChangesAsync();
            return operationRequest;
        }

        public async Task<OperationRequest> UpdateRequestAsync(Guid id, OperationRequest updatedRequest)
        {
            var existingRequest = await _operationRequestRepository.GetByIdAsync(id);
            if (existingRequest == null)
                throw new KeyNotFoundException("Operation request not found.");

            if (existingRequest.Status != "Requested")
                throw new InvalidOperationException("Only 'Requested' operation requests can be updated.");

            existingRequest.Priority = updatedRequest.Priority;
            existingRequest.Deadline = updatedRequest.Deadline;
            existingRequest.DoctorId = updatedRequest.DoctorId;
            existingRequest.OperationTypeId = updatedRequest.OperationTypeId;
            existingRequest.PatientId = updatedRequest.PatientId;

            await _operationRequestRepository.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<bool> DeleteRequestAsync(Guid id)
        {
            var operationRequest = await _operationRequestRepository.GetByIdAsync(id);
            if (operationRequest == null)
                return false;

            if (operationRequest.Status == "Scheduled")
                throw new InvalidOperationException("Cannot delete an operation request that is already scheduled.");

            await _operationRequestRepository.DeleteAsync(id);
            await _operationRequestRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OperationRequest>> GetRequestsAsync(string status, int priority)
        {
            return await _operationRequestRepository.GetByStatusAndPriorityAsync(status, priority);
        }

        public async Task<OperationRequest> GetByIdAsync(Guid id)
        {
            return await _operationRequestRepository.GetByIdAsync(id);
        }
    }
}