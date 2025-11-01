using Microsoft.AspNetCore.Mvc;
using Applications.Dto;
using Domain.Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/guideController/")]
    public class GuideController : ControllerBase
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }

        [HttpPost("issuesByPeriod/")]
        public async Task<IActionResult> ShowIssuesByPeriodAsync([FromBody] PeriodDto periodDto, CancellationToken token)
        {
            var issues = await _guideService.ShowIssuesByPeriod(periodDto, token);
            return Ok(issues);
        }

        [HttpPost("returnsByPeriod/")]
        public async Task<IActionResult> ShowReturnsByPeriod([FromBody] PeriodDto periodDto, CancellationToken token)
        {
            var returns = await _guideService.ShowReturnsByPeriod(periodDto, token);
            return Ok(returns);
        }

        [HttpGet("booksReader/{readerCardNumber}/")]
        public async Task<IActionResult> ShowBooksReader(string readerCardNumber, CancellationToken token)
        {
            var books = await _guideService.ShowBooksReader(readerCardNumber, token);
            return Ok(books);
        }

        [HttpGet("isStock/{libraryCode}/")]
        public async Task<IActionResult> IsStock(string libraryCode, CancellationToken token)
        {
            var inStock = await _guideService.IsStock(libraryCode, token);
            return Ok(inStock);
        }
    }
}