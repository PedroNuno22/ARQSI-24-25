using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories
{
    public class OperationRequestRepository : IOperationRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationRequest> GetByIdAsync(Guid id)
        {
            return await _context.OperationRequests.FindAsync(id);
        }

        public async Task AddAsync(OperationRequest operationRequest)
        {
            await _context.OperationRequests.AddAsync(operationRequest);
        }

        public async Task UpdateAsync(OperationRequest operationRequest)
        {
            _context.OperationRequests.Update(operationRequest);
        }

        public async Task DeleteAsync(Guid id)
        {
            var operationRequest = await _context.OperationRequests.FindAsync(id);
            if (operationRequest != null)
            {
                _context.OperationRequests.Remove(operationRequest);
            }
        }

        public async Task<IEnumerable<OperationRequest>> GetByStatusAndPriorityAsync(string status, int priority)
        {
            var query = _context.OperationRequests.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status == status);

            if (priority > 0)
                query = query.Where(r => r.Priority == priority);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // New method: Get operation requests by Doctor ID
        public async Task<IEnumerable<OperationRequest>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.OperationRequests
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();
        }

        // New method: Get operation requests by Patient ID
        public async Task<IEnumerable<OperationRequest>> GetByPatientIdAsync(Guid patientId)
        {
            return await _context.OperationRequests
                .Where(r => r.PatientId == patientId)
                .ToListAsync();
        }

        // New method: Get operation requests by date range
        public async Task<IEnumerable<OperationRequest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.OperationRequests
                .Where(r => r.Deadline >= startDate && r.Deadline <= endDate)
                .ToListAsync();
        }

        // New method: Get pending operation requests
        public async Task<IEnumerable<OperationRequest>> GetPendingRequestsAsync()
        {
            return await _context.OperationRequests
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }
    }
}