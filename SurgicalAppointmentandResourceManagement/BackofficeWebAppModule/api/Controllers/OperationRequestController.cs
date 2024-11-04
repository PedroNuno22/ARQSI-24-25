using Microsoft.AspNetCore.Mvc;
using Application.DTOs.OperationRequestDto;

namespace api.Controllers
{
    [ApiController]
    [Route("api/operation-requests")]
    public class OperationRequestController : ControllerBase
    {
        private readonly IOperationRequestService _operationRequestService;

        public OperationRequestController(IOperationRequestService operationRequestService)
        {
            _operationRequestService = operationRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperationRequest(OperationRequestDto requestDto)
        {
            var result = await _operationRequestService.CreateRequestAsync(requestDto);
            if (result == null)
                return BadRequest("Error creating operation request.");

            return CreatedAtAction(nameof(GetOperationRequestById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperationRequest(int id, OperationRequestDto requestDto)
        {
            var updatedRequest = await _operationRequestService.UpdateRequestAsync(id, requestDto);
            if (updatedRequest == null)
                return NotFound("Operation request not found.");

            return Ok(updatedRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationRequest(int id)
        {
            var deleted = await _operationRequestService.DeleteRequestAsync(id);
            if (!deleted)
                return NotFound("Operation request not found or already scheduled.");

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationRequests([FromQuery] string status, [FromQuery] string priority)
        {
            var requests = await _operationRequestService.GetRequestsAsync(status, priority);
            return Ok(requests);
        }
    }


}
