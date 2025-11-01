using Microsoft.AspNetCore.Mvc;
using Applications.Dto.Issue;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/issueController/")]
    public class IssuesController : Controller
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddIssueAsync([FromBody] AddIssueDto addIssueDto, CancellationToken token)
        {
            var issueId = await _issueService.AddIssueAsync(addIssueDto, token);
            return Ok(issueId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueAsync(int id, CancellationToken token)
        {
            var issue = await _issueService.ShowIssueAsync(id, token);
            return Ok(issue);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIssuesAsync(CancellationToken token)
        {
            var issues = await _issueService.ShowAllIssuesAsync(token);
            return Ok(issues);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateIssueAsync(UpdateIssueDto issue, CancellationToken token)
        {
            var result = await _issueService.UpdateIssueAsync(issue, token);
            if (!result)
                return NotFound("Выдача не найдена для обновления");

            return Ok("Выдача успешно обновлена");
        }
    }
}