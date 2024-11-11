using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Services;

namespace Application.Services
{
    public class OperationRequestAppService
    {
        private readonly IOperationRequestService _operationRequestService;
        private readonly OperationRequestMapper _mapper;

        public OperationRequestAppService(IOperationRequestService operationRequestService, OperationRequestMapper mapper)
        {
            _operationRequestService = operationRequestService;
            _mapper = mapper;
        }

        // Creates an operation request by mapping from DTO to domain entity
        public async Task<OperationRequestDto> CreateRequestAsync(OperationRequestDto requestDto)
        {
            // Map DTO to domain entity using the mapper
            var operationRequest = _mapper.ToEntity(requestDto);

            // Call Domain service to create request
            var createdRequest = await _operationRequestService.CreateRequestAsync(operationRequest);

            // Map the created entity back to DTO for the response
            return _mapper.ToDto(createdRequest);
        }

        // Updates an existing operation request
        public async Task<OperationRequestDto> UpdateRequestAsync(Guid id, OperationRequestDto requestDto)
        {
            // Map DTO to domain entity using the mapper
            var operationRequest = _mapper.ToEntity(requestDto);

            // Call Domain service to update request
            var updatedRequest = await _operationRequestService.UpdateRequestAsync(id, operationRequest);
            if (updatedRequest == null)
                return null;

            // Map the updated entity back to DTO for the response
            return _mapper.ToDto(updatedRequest);
        }

        // Deletes an operation request by ID
        public async Task<bool> DeleteRequestAsync(Guid id)
        {
            return await _operationRequestService.DeleteRequestAsync(id);
        }

        // Retrieves a list of operation requests with optional filters
        public async Task<IEnumerable<OperationRequestDto>> GetRequestsAsync(string status, int priority)
        {
            var operationRequests = await _operationRequestService.GetRequestsAsync(status, priority);

            // Map the list of domain entities to DTOs using the mapper
            var requestDtos = new List<OperationRequestDto>();
            foreach (var request in operationRequests)
            {
                requestDtos.Add(_mapper.ToDto(request));
            }
            return requestDtos;
        }

        // Retrieves the operation request with the paremeter id
        public async Task<OperationRequestDto> GetRequestByIdAsync(Guid id)
        {
            // Retrieve the OperationRequest from the Domain service by ID
            var operationRequest = await _operationRequestService.GetByIdAsync(id);
            if (operationRequest == null)
                return null;

            // Map the entity to DTO before returning
            return _mapper.ToDto(operationRequest);
        }
    }
}