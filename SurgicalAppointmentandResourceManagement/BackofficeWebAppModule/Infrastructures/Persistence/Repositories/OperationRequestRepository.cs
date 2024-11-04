using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence.Repositories
{
    public class OperationRequestRepository : IOperationRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationRequest> GetByIdAsync(int id)
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

        public async Task DeleteAsync(OperationRequest operationRequest)
        {
            _context.OperationRequests.Remove(operationRequest);
        }

        public async Task<IEnumerable<OperationRequest>> GetByStatusAndPriorityAsync(string status, string priority)
        {
            var query = _context.OperationRequests.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(r => r.Priority == priority);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}