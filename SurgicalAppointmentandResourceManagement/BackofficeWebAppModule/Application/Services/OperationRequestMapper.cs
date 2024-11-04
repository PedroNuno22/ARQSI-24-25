using Application.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class OperationRequestMapper
    {
        public OperationRequest ToEntity(OperationRequestDto dto)
        {
            return new OperationRequest
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                OperationTypeId = dto.OperationTypeId,
                Priority = dto.Priority,
                Deadline = dto.Deadline,
                Status = dto.Status
            };
        }

        public OperationRequestDto ToDto(OperationRequest entity)
        {
            return new OperationRequestDto
            {
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                OperationTypeId = entity.OperationTypeId,
                Priority = entity.Priority,
                Deadline = entity.Deadline,
                Status = entity.Status
            };
        }
    }
}