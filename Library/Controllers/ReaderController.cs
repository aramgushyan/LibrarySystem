using Microsoft.AspNetCore.Mvc;
using Applications.Dto.Reader;
using Domain.Models;
using Services.Interfaces;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/readerController/")]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpPost("addReader")]
        public async Task<IActionResult> AddReaderAsync([FromBody] AddReaderDto addReaderDto, CancellationToken token)
        {
            var readerCardNumber = await _readerService.AddReaderAsync(addReaderDto, token);
            return Ok(readerCardNumber);
        }

        [HttpGet("{readerCardNumber}")]
        public async Task<IActionResult> GetReaderAsync(string readerCardNumber, CancellationToken token)
        {
            var reader = await _readerService.GetReaderAsync(readerCardNumber, token);
            return Ok(reader);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetReadersAsync(CancellationToken token)
        {
            var readers = await _readerService.GetReadersAsync(token);
            return Ok(readers);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateReaderAsync([FromBody] UpdateReaderDto updateReaderDto, CancellationToken token)
        {
            var result = await _readerService.UpdateReaderAsync(updateReaderDto, token);
            if (!result)
                return NotFound("Читатель не найден для обновления");

            return Ok("Данные читателя успешно обновлены");
        }

        [HttpDelete("{readerCardNumber}")]
        public async Task<IActionResult> RemoveReaderAsync(string readerCardNumber, CancellationToken token)
        {
            var result = await _readerService.RemoveReaderAsync(readerCardNumber, token);
            if (!result)
                return NotFound("Читатель не найден для удаления");

            return Ok("Читатель успешно удалён");
        }
    }
}
