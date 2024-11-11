using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Domain.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/operation-requests")]
    public class OperationRequestController : ControllerBase
    {
        private readonly OperationRequestAppService _operationRequestAppService;

        public OperationRequestController(OperationRequestAppService operationRequestService)
        {
            _operationRequestAppService = operationRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperationRequest(OperationRequestDto requestDto)
        {
            var result = await _operationRequestAppService.CreateRequestAsync(requestDto);
            if (result == null)
                return BadRequest("Error creating operation request.");

            return CreatedAtAction(nameof(GetOperationRequestById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperationRequest(Guid id, OperationRequestDto requestDto)
        {
            var updatedRequest = await _operationRequestAppService.UpdateRequestAsync(id, requestDto);
            if (updatedRequest == null)
                return NotFound("Operation request not found.");

            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationRequest(Guid id)
        {
            var deleted = await _operationRequestAppService.DeleteRequestAsync(id);
            if (!deleted)
                return NotFound("Operation request not found or already scheduled.");

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationRequests([FromQuery] string status, [FromQuery] int priority)
        {
            var requests = await _operationRequestAppService.GetRequestsAsync(status, priority);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationRequestById(Guid id)
        {
            var result = await _operationRequestAppService.GetRequestByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }


}
